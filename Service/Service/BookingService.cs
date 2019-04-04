using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;
using Transport.Repository;

namespace Transport.Service
{
    public class BookingService : AbstractService<int, Booking>
    {
        private readonly BookingDBRepository _bookingDBRepository;

        private readonly ClientDBRepository _clientDBRepository;

        public BookingService(IRepository<int, Booking> repository) : base(repository)
        {
            _bookingDBRepository = (BookingDBRepository) repository;
            _clientDBRepository = new ClientDBRepository(DBUtils.GetProperties());
        }

        public IEnumerable<Booking> FilterByRide(int rideId)
        {
            return _bookingDBRepository.FindByRide(rideId);
        }

        public List<Tuple<string, int>> FilterByClient(int rideId)
        {
            var list = new List<Tuple<string, int>>();
            foreach (Booking booking in FilterByRide(rideId))
            {
                var client = _clientDBRepository.FindOne(booking.ClientId);
                list.Add(Tuple.Create($"{client.FirstName}, {client.LastName}", booking.SeatNo));
            }
            return list;
        }
    }
}
