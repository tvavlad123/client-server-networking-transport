using networking;
using Networking;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transport.Repository;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {

            var employeeRepo = new EmployeeDBRepository(DBUtils.GetProperties());
            var rideRepo = new RideDBRepository(DBUtils.GetProperties());
            var bookingRepo = new BookingDBRepository(DBUtils.GetProperties());
            var clientRepo = new ClientDBRepository(DBUtils.GetProperties());
            ITransportServer transportServer = new TransportServer(employeeRepo, rideRepo, bookingRepo, clientRepo);

            // IChatServer serviceImpl = new ChatServerImpl();
            var server = new ServerSerial("127.0.0.1", 8081, transportServer);
            server.Start();

            Console.WriteLine("Server started ...");
            //Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }

        class ServerSerial : ConcurrentServer
        {
            private readonly ITransportServer _server;
            private ClientWorker _clientWorker;
            public ServerSerial(string host, int port, ITransportServer server) : base(host, port)
            {
                _server = server;
                Console.WriteLine("Server ...");

            }

            protected override Thread CreateWorker(TcpClient client)
            {
                _clientWorker = new ClientWorker(_server, client);
                return new Thread(_clientWorker.Run);
            }
        }
    }
}
