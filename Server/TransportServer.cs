using Service;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transport.Model;
using Transport.Repository;
using Transport.Service;

namespace Server
{
    public class TransportServer : ITransportServer
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IRepository<int, Ride> _rideRepository;
        private readonly IRepository<int, Booking> _bookingRepository;
        private readonly IRepository<int, Client> _clientRepository;
        private readonly IDictionary<string, ITransportObserver> _observers;
        public TransportServer(IRepository<int, Employee> employeeRepository, IRepository<int, Ride> rideRepository, IRepository<int, Booking> bookingRepository, IRepository<int, Client> clientRepository)
        {
            _employeeRepository = employeeRepository;
            _rideRepository = rideRepository;
            _bookingRepository = bookingRepository;
            _clientRepository = clientRepository;
            _observers = new Dictionary<string, ITransportObserver>();
        }

        public bool LogIn(Employee employee, ITransportObserver client)
        {
            var userOk = ((EmployeeDBRepository)_employeeRepository).FindByCredentials(employee.UserName, employee.Password);
            if (!userOk) return false;
            if (_observers.ContainsKey(employee.UserName))
                return false;
            _observers[employee.UserName] = client;
            return true;
        }

        public List<Ride> GetAllRides()
        {
            return ((RideDBRepository)_rideRepository).FindAll().ToList();
        }

        public List<Booking> GetAllBookings()
        {
            return ((BookingDBRepository)_bookingRepository).FindAll().ToList();
        }

        public List<Client> GetAllClients()
        {
            return ((ClientDBRepository)_clientRepository).FindAll().ToList();
        }

        public List<Ride> GetCustomRides(string destination, string date, string hour)
        {
            var tripService = new RideService(_rideRepository);
            return tripService.FilterDestinationDateHour(destination, date, hour);
        }

        public Client GetCustomClients(string firstName, string lastName)
        {
            var clientService = new ClientService(_clientRepository);
            return clientService.FilterClientByName(firstName, lastName);
        }

        public List<ClientRide> GetClientRide(string destination, string date, string hour)
        {
            var bookingService = new BookingService(_bookingRepository);
            var list = new List<ClientRide>();
            List<int> seats = new List<int>();
            foreach (var info in bookingService.FilterByClient(GetCustomRides(destination, date, hour)[0].Id))
            {
                var clientRide = new ClientRide
                {
                    FirstLastName = info.Item1,
                    SeatNo = info.Item2
                };
                list.Add(clientRide);
                seats.Add(clientRide.SeatNo);
            }
            for (int index = 1; index <= RideService.MaxAvailablePlaces; index++)
            {
                if (seats.IndexOf(index) == -1)
                {
                    var clientRide = new ClientRide
                    {
                        FirstLastName = "-",
                        SeatNo = index
                    };
                    list.Add(clientRide);
                }
            }
            return list;
        }

        public List<Ride> AddBooking(Ride ride, Booking booking, Employee employee, Client client)
        {
            
            ((BookingDBRepository)_bookingRepository).Save(booking);
            
            var allRides = ((RideDBRepository)_rideRepository).FindAll().ToList();

            foreach (var x in _observers.Keys)
                if (x != employee.UserName)
                    Task.Run(() => _observers[x].AddBooking(allRides));

            return allRides;
        }

        public void LogOut(Employee employee)
        {
            _observers.Remove(employee.UserName);
        }
    }
}