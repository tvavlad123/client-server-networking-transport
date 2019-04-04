using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    public interface IResponse
    {

    }

    public interface IUpdateResponse : IResponse
    {

    }

    [Serializable]
    public class OkResponse : IResponse
    {

    }

    [Serializable]
    public class ErrorResponse : IResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }

        public virtual string Message { get; }
    }
    [Serializable]
    public sealed class GetRidesResponse : IResponse
    {
        public GetRidesResponse(RideDTO[] rides)
        {
            Rides = rides;
        }

        public RideDTO[] Rides { get; set; }
    }

    [Serializable]
    public sealed class GetClientResponse : IResponse
    {
        public GetClientResponse(ClientDTO[] clients)
        {
            Clients = clients;
        }

        public ClientDTO[] Clients { get; set; }
    }

    [Serializable]
    public sealed class AddedBookingResponse : IUpdateResponse
    {
        public AddedBookingResponse(RideDTO[] rides)
        {
            Rides = rides;
        }

        public RideDTO[] Rides { get; set; }
    }
}
