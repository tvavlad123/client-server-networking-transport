using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    public class CustomRideDTO
    {
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
    }
}
