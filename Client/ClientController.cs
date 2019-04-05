using Service;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;
using Transport.Repository;
using Transport.Service;

namespace Client
{
    public class ClientController : ITransportObserver
    {
        public static readonly int MaxAvailablePlaces = 18;
        public event EventHandler<EmployeeEventArgs> UpdateEvent;
        private readonly ITransportServer _server;
        private Employee _currentEmployee;

        public ClientController(ITransportServer server)
        {
            _server = server;
            _currentEmployee = null;
        }

        public bool LogIn(string username, string password)
        {
            var employee = new Employee { UserName = username, Password = password };
            var result = _server.LogIn(employee, this);
            if (!result) return false;
            Console.WriteLine(@"Login succeeded ....");
            _currentEmployee = employee;
            Console.WriteLine(@"Current user {0}", employee.UserName);
            return true;
        }

        public List<Ride> GetAllRides()
        {
            var result = _server.GetAllRides();
            Console.WriteLine(@"Get all trips succeeded ....");
            return result;
        }

        public List<Booking> GetAllBookings()
        {
            var result = _server.GetAllBookings();
            Console.WriteLine(@"Get all bookings succeeded ....");
            return result;
        }

        public List<Transport.Model.Client> GetAllClients()
        {
            var result = _server.GetAllClients();
            Console.WriteLine(@"Get all clients succeeded ....");
            return result;
        }
        public List<Ride> GetCustomRides(string destination, string date, string hour)
        {
            var result = _server.GetCustomRides(destination, date, hour);
            foreach (Ride ride in result)
            {
                Console.WriteLine(ride.Destination);
            }
            Console.WriteLine(@"Get custom trips succeeded ....");
            return result;
        }

        public Transport.Model.Client GetCustomClients(string firstName, string lastName)
        {
            var result = _server.GetCustomClients(firstName, lastName);
            
                Console.WriteLine($"{result.FirstName} {result.LastName}");
            
            Console.WriteLine(@"Get custom clients succeeded...");
            return result;
        }
        protected virtual void OnUpdateEvent(EmployeeEventArgs e)
        {
            UpdateEvent?.Invoke(this, e);
            Console.WriteLine(@"Update Event called");
        }

        public void AddBooking(Ride ride, Booking booking, Transport.Model.Client client)
        {
            var allRides = _server.AddBooking(ride, booking, _currentEmployee, client);
            var employeeArgs = new EmployeeEventArgs(allRides, EmployeeEvent.BookingAdded);
            OnUpdateEvent(employeeArgs);
            Console.WriteLine(@"Make booking succeeded ....");
        }

        public void AddClient(Transport.Model.Client client)
        {
            ClientDBRepository clientDBRepository = new ClientDBRepository(DBUtils.GetProperties());
            clientDBRepository.Save(client);

        }

        public void AddBooking(List<Ride> allRides)
        {
            var employeeArgs = new EmployeeEventArgs(allRides, EmployeeEvent.BookingAdded);
            OnUpdateEvent(employeeArgs);
        }

        public void LogOut()
        {
            _server.LogOut(_currentEmployee);
            _currentEmployee = null;
            Console.WriteLine(@"Log out succeeded ....");
        }

        
    }
}
