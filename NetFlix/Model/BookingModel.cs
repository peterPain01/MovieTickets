using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NetFlix.Model
{
    public class BookingModel
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int ShowtimeId { get; set; }
        public DateTime BookingDatetime { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ObservableCollection<Seat> SelectedSeats { get; set; }
    }
}
