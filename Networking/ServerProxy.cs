using Networking;
using Service;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Transport.Model;

namespace networking
{
    public class ServerProxy : ITransportServer
    {
        private readonly string _host;
        private readonly int _port;

        private ITransportObserver _client;

        private NetworkStream _stream;

        private IFormatter _formatter;
        private TcpClient _connection;

        private readonly Queue<IResponse> _responses;
        private volatile bool _finished;
        private EventWaitHandle _waitHandle;

        public ServerProxy(string host, int port)
        {
            _host = host;
            _port = port;
            _responses = new Queue<IResponse>();
        }

        public bool LogIn(Employee employee, ITransportObserver client)
        {
            InitializeConnection();
            var udto = DTOUtils.GetEmployeeDto(employee);
            SendRequest(new LoginRequest(udto));
            var response = ReadResponse();
            if (response is OkResponse)
            {
                _client = client;
                return true;
            }
            CloseConnection();
            return false;
        }


        public List<Ride> GetAllRides()
        {
            var rideDto = new RideDTO();
            SendRequest(new GetAllRidesRequest(rideDto));
            var response = ReadResponse();
            if (response is ErrorResponse)
                throw new Exception("Error Get All Rides");
            return (response as GetRidesResponse)?.Rides.Select(DTOUtils.GetFromDto).ToList();
        }

        public List<Booking> GetAllBookings()
        {
            var bookingDto = new BookingDTO();
            SendRequest(new GetAllBookingsRequest(bookingDto));
            var response = ReadResponse();
            if (response is ErrorResponse)
                throw new Exception("Error Get All Bookings");
            return (response as GetBookingsResponse)?.Bookings.Select(DTOUtils.GetFromDto).ToList();
        }

        public List<Ride> GetCustomRides(string destination, string date, string hour)
        {
            var customRideDto = new CustomRideDTO
            {
                Destination = destination,
                Date = DateTime.Parse(date),
                Hour = DateTime.Parse(hour)
            };
            SendRequest(new GetCustomRidesRequest(customRideDto));
            var response = ReadResponse();
            if (response is ErrorResponse)
                throw new Exception("Error Get Custom");

            return (response as GetRidesResponse)?.Rides.Select(DTOUtils.GetFromDto).ToList();
        }


        public Client GetCustomClients(string firstName, string lastName)
        {
            var customClient = new ClientDTO
            {
                FirstName = firstName,
                LastName = lastName
            };
            SendRequest(new GetCustomClientsRequest(customClient));
            var response = ReadResponse();
            if (response is ErrorResponse)
                throw new Exception("Error Get Custom Client");
            return (response as GetClientResponse)?.Clients.Select(DTOUtils.GetFromDto).ToList().First();
        }

        public List<Ride> AddBooking(Ride ride, Booking booking, Employee employee)
        {
            var bookingDto = DTOUtils.GetBookingDto(booking);
            var rideDto = DTOUtils.GetRideDto(ride);
            var employeeDto = DTOUtils.GetEmployeeDto(employee);
            SendRequest(new BookingRequest(bookingDto, rideDto, employeeDto));
            var response = ReadResponse();
            if (response is ErrorResponse)
                throw new Exception("Error add reservation");

            return ((GetRidesResponse)response).Rides.Select(DTOUtils.GetFromDto).ToList();
        }

        public void LogOut(Employee employee)
        {
            var employeeDto = DTOUtils.GetEmployeeDto(employee);
            SendRequest(new LogOutRequest(employeeDto));
            var response = ReadResponse();
            if (response is ErrorResponse)
                throw new Exception("Error Log Out");
        }

        public void Refresh()
        {
            SendRequest(new ReservationRefreshRequest());
            var response = ReadResponse();

        }

        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _stream.Close();
                _connection.Close();
                _waitHandle.Close();
                _client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private IResponse ReadResponse()
        {
            IResponse response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (_responses)
                {
                    response = _responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void SendRequest(IRequest request)
        {
            try
            {
                _formatter.Serialize(_stream, request);
                _stream.Flush();
            }
            catch (Exception e)
            {
                throw new Exception("Error sending object " + e);
            }
        }

        private void InitializeConnection()
        {
            try
            {
                _connection = new TcpClient(_host, _port);
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
                _finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void StartReader()
        {
            var thread = new Thread(Run);
            thread.Start();
        }

        public virtual void Run()
        {
            while (!_finished)
            {
                try
                {
                    var response = _formatter.Deserialize(_stream);
                    Console.WriteLine("response received " + response);
                    if (response is IUpdateResponse updateResponse)
                    {
                        HandleUpdate(updateResponse);
                    }
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue((IResponse)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    if (_finished)
                        break;
                    Console.WriteLine("Reading error " + e);
                }
            }
        }

        private void HandleUpdate(IUpdateResponse response)
        {
            if (response is AddedBookingResponse reservationResponse)
            {
                _client.AddBooking(reservationResponse.Rides.Select(DTOUtils.GetFromDto).ToList());
            }
        }
    }
}