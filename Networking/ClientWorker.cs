using networking;
using Service;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transport.Model;
using Transport.Repository;
using Transport.Service;

namespace Networking
{
    public class ClientWorker : ITransportObserver
    {
        private readonly ITransportServer _server;
        private readonly TcpClient _connection;

        private readonly NetworkStream _stream;
        private readonly IFormatter _formatter;
        private volatile bool _connected;

        public ClientWorker(ITransportServer server, TcpClient tcpClient)
        {
            _server = server;
            _connection = tcpClient;
            try
            {
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void Run()
        {
            while (_connected)
            {
                try
                {
                    var request = (IRequest)_formatter.Deserialize(_stream);
                    var response = HandleRequest(request);
                    if (response != null)
                        SendResponse(response);

                    if (response is ErrorResponse && request is LoginRequest)
                        _connected = false;
                }
                catch (Exception e)
                {
                    if (!_connected)
                        break;
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(10);
                }
                catch (Exception e)
                {
                    if (!_connected)
                        break;
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private void SendResponse(IResponse response)
        {
            Console.WriteLine("sending response " + response);
            _formatter.Serialize(_stream, response);
            _stream.Flush();
        }

        private IResponse HandleRequest(IRequest request)
        {
            List<Ride> rides;
            switch (request)
            {
                case LoginRequest _:
                    Console.WriteLine("Login request ...");
                    var loginRequest = (LoginRequest)request;
                    var employeeDto = loginRequest.EmployeeDto;
                    var employee = DTOUtils.GetFromDto(employeeDto);
                    try
                    {
                        bool response;
                        lock (_server)
                        {
                            response = _server.LogIn(employee, this);
                        }
                        if (response)
                            return new OkResponse();
                        return new ErrorResponse("Wrong Username/Password");
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return new ErrorResponse(e.Message);
                    }

                case GetAllRidesRequest _:
                    Console.WriteLine("Get All Trips request ...");
                    try
                    {
                        RideDTO[] response;
                        lock (_server)
                        {
                            response = _server.GetAllRides().Select(DTOUtils.GetRideDto).ToArray();
                        }
                        return new GetRidesResponse(response);
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse(e.Message);
                    }
                case GetAllBookingsRequest _:
                    Console.WriteLine("Get All Bookings request ...");
                    try
                    {
                        BookingDTO[] response;
                        lock (_server)
                        {
                            response = _server.GetAllBookings().Select(DTOUtils.GetBookingDto).ToArray();
                        }
                        return new GetBookingsResponse(response);
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse(e.Message);
                    }
                case GetAllClientsRequest _:
                    Console.WriteLine("Get All Bookings request ...");
                    try
                    {
                        ClientDTO[] response;
                        lock (_server)
                        {
                            response = _server.GetAllClients().Select(DTOUtils.GetClientDto).ToArray();
                        }
                        return new GetClientsResponse(response);
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse(e.Message);
                    }
                case GetCustomRidesRequest _:
                    Console.WriteLine("Get custom rides...");
                    var customRequest = (GetCustomRidesRequest)request;
                    var customDto = customRequest.CustomRideDto;
                    try
                    {
                        RideDTO[] response;
                        lock (_server)
                        {
                            RideDBRepository db = new RideDBRepository(DBUtils.GetProperties());
                            var rideService = new RideService(db);
                            Console.WriteLine(rideService.FilterDestinationDateHour(customDto.Destination, customDto.Date.ToString("yyyy-MM-dd"),
                                customDto.Hour.ToString(@"HH\:mm")));
                            response = _server.GetCustomRides(customDto.Destination, customDto.Date.ToString("yyyy-MM-dd"),
                                customDto.Hour.ToString(@"HH\:mm")).Select(DTOUtils.GetRideDto).ToArray();
                        }
                        return new GetRidesResponse(response);
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse(e.Message);
                    }
                case BookingRequest _:
                    Console.WriteLine("Add booking...");
                    var bookingRequest = (BookingRequest)request;
                    var ride = DTOUtils.GetFromDto(bookingRequest.RideDto);
                    var booking = DTOUtils.GetFromDto(bookingRequest.BookingDto);
                    var client = DTOUtils.GetFromDto(bookingRequest.ClientDto);
                    employee = DTOUtils.GetFromDto(bookingRequest.EmployeeDto);
                    try
                    {
                        lock (_server)
                        {
                            rides = _server.AddBooking(ride, booking, employee, client);
                        }
                        return new GetRidesResponse(rides.Select(DTOUtils.GetRideDto).ToArray());
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse(e.Message);
                    }
                case LogOutRequest _:
                    Console.WriteLine("Log out ...");
                    var logOutRequest = (LogOutRequest)request;
                    employee = DTOUtils.GetFromDto(logOutRequest.EmployeeDto);
                    try
                    {
                        lock (_server)
                        {
                            _server.LogOut(employee);
                        }

                        return new OkResponse();
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse(e.Message);
                    }
            }
            return null;
        }

        public void AddBooking(List<Ride> allRides)
        {
            try
            {
                SendResponse(new AddedBookingResponse(allRides.Select(DTOUtils.GetRideDto).ToArray()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
