using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Model
{
    public class Seat
    {
        int SeatId { get; set; }
        int CinemaId { get; set; }
        int ShowtimeId { get; set; }
        char row { get; set;  }
        int number { get; set; }
        string Status { get; set; }
    }
}

