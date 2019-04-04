using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;

namespace Transport.Repository
{
    public class BookingRepository : AbstractRepository<int, Booking>
    {
        public BookingRepository(IValidator<Booking> validator) : base(validator)
        {
        }
    }
}
