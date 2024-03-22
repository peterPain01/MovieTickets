using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Model
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int CinemaId { get; set; }
        public int ShowtimeId { get; set; }
        public char row { get; set;  }
        public int number { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
    }
}

