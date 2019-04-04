using System;
using System.Collections.Generic;
using System.Linq;
using Transport.Model;
using Transport.Repository;

namespace Transport.Service
{
    public class RideService : AbstractService<int, Ride>
    {
        public static readonly int MaxAvailablePlaces = 18;

        private readonly RideDBRepository _rideDBRepository;

        public RideService(IRepository<int, Ride> repository) : base(repository)
        {
            _rideDBRepository = (RideDBRepository) repository;
        }


        public List<Ride> FilterDestinationDateHour(string destination, string date, string hour)
        {
            return FilterAndSorter(GetAll().ToList(), ride => ride.Destination.ToLower().Contains(destination.ToLower()) &&
                                                     ride.Date.ToString("yyyy-MM-dd").Contains(date) &&
                                                     ride.Hour.ToString(@"HH\:mm").Contains(hour),
                ride => ride.Id);
        }

        public int AvailableSeatsRide(Ride ride)
        {
           int availableSeats = MaxAvailablePlaces - _rideDBRepository.FindRide(ride.Destination, ride.Date.ToString("yyyy-MM-dd"), ride.Hour.ToString(@"HH\:mm")).First().Value;
           if (availableSeats <= 0) return 0;
           else return availableSeats;
        }
    }
}
