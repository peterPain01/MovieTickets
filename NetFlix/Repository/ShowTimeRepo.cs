using NetFlix.EnityModel;
using NetFlix.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetFlix.Repository
{
    public class ShowTimeRepo : RepositoryBase
    {
        // get after day 
        public ObservableCollection<Showtime> GetShowTimeByMovieId(int id)
        {
            using (var context = new BookingMovieAppContext())
            {
                var showtimes = new ObservableCollection<Showtime>(
                    context.Showtimes
                           .Where(st => st.MovieId == id)
                           .Select(st => new Showtime
                           {
                               ShowtimeId = st.ShowtimeId,
                               ShowtimeDatetime = st.ShowtimeDatetime,
                               Cinema = new Cinema
                               {
                                   // Can have bug here 
                                   CinemaId = st.Cinema.CinemaId,
                                   Name = st.Cinema.Name,
                               }
                           })
                           .ToList());
                return showtimes;
            }
        }

        public ObservableCollection<Seat> GetSeatByShowtimeId(int showtimeId)
        {
            using (var context = new BookingMovieAppContext())
            {
                ObservableCollection<Seat> seats = new ObservableCollection<Seat>(context.Seats
                    .Where(s => s.ShowtimeId == showtimeId)
                    .ToList());
                return seats;
            }
        }
    }
}
