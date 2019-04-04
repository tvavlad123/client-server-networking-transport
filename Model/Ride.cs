using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Model
{
    public class Ride : IHasId<int>
    {
        public int Id { get; set; }

        public string Destination { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hour { get; set; }
    }
}
