﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    public class BookingDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RideId { get; set; }
        public int SeatNo { get; set; }
    }
}
