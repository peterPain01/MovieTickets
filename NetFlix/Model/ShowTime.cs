using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Model
{
    public class ShowTime
    {
        public int ShowTimeId { get; set; }
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public string ShowTimeDateTime { get; set; }
        public string CinemaName { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }

        public ShowTime() { }
    }
}
