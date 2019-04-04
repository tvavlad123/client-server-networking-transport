using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace networking
{
    public abstract class AbstractServer
    {
        private TcpListener _server;
        private readonly string _host;
        private readonly int _port;

        protected AbstractServer(string host, int port)
        {
            _host = host;
            _port = port;
        }
        public void Start()
        {
            var adr = IPAddress.Parse(_host);
            var ep = new IPEndPoint(adr, _port);
            _server = new TcpListener(ep);
            _server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for clients ...");
                var client = _server.AcceptTcpClient();
                Console.WriteLine("Client connected ...");
                ProcessRequest(client);
            }
        }
        public abstract void ProcessRequest(TcpClient client);

    }

    public abstract class ConcurrentServer : AbstractServer
    {
        protected ConcurrentServer(string host, int port) : base(host, port)
        { }

        public override void ProcessRequest(TcpClient client)
        {
            var t = CreateWorker(client);
            t.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);

    }
}