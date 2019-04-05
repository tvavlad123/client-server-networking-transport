using Service;
using System.Collections.Generic;
using Transport.Model;

namespace services
{
    public interface ITransportServer
    {
        bool LogIn(Employee employee, ITransportObserver client);
        List<Ride> GetAllRides();
        List<Ride> GetCustomRides(string destination, string date, string hour);
        List<Client> GetAllClients();
        List<Ride> AddBooking(Ride ride, Booking booking, Employee employee, Client client);
        void LogOut(Employee employee);
        Client GetCustomClients(string firstName, string lastName);
        List<Booking> GetAllBookings();
    }
}