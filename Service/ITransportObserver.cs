using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;

namespace Service
{
    public interface ITransportObserver
    {
        void AddBooking(List<Ride> allBookings);
    }
}
