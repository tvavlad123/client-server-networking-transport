
using Networking;
using Transport.Model;

namespace networking
{
    public class DTOUtils
    {
        public static Employee GetFromDto(EmployeeDTO employeeDto)
        {
            return new Employee { UserName = employeeDto.UserName, Password = employeeDto.Password };
        }

        public static EmployeeDTO GetEmployeeDto(Employee employee)
        {
            return new EmployeeDTO(employee.UserName, employee.Password);
        }

        public static Ride GetFromDto(RideDTO rideDto)
        {
            return new Ride
            {
                Id = rideDto.Id,
                Destination = rideDto.Destination,
                Date = rideDto.Date,
                Hour = rideDto.Hour
            };
        }

        public static Client GetFromDto(ClientDTO clientDto)
        {
            return new Client
            {
                Id = clientDto.Id,
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName
            };
        }

        public static RideDTO GetRideDto(Ride ride)
        {
            return new RideDTO
            {
                Id = ride.Id,
                Destination = ride.Destination,
                Date = ride.Date,
                Hour = ride.Hour
            };
        }

        public static Booking GetFromDto(BookingDTO bookingDto)
        {
            return new Booking
            {
                Id = bookingDto.Id,
                ClientId = bookingDto.ClientId,
                RideId = bookingDto.RideId,
                SeatNo = bookingDto.SeatNo
            };
        }

        public static BookingDTO GetBookingDto(Booking booking)
        {
            return new BookingDTO
            {
                Id = booking.Id,
                ClientId = booking.ClientId,
                RideId = booking.RideId,
                SeatNo = booking.SeatNo
            };
        }

        public static ClientDTO GetClientDto(Client client)
        {
            return new ClientDTO
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName
            };
        }
    }
}