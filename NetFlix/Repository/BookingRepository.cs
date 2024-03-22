using NetFlix.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace NetFlix.Repository
{
    public class BookingRepository : RepositoryBase
    {
        public int CreateBooking(BookingModel newBooking)
        {
            int bookingId = -1;
            BookingModel bookingModel = new BookingModel();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = " INSERT INTO [Bookings] (user_id, showtime_id, original_price, total_price) " +
                    "                   VALUES (@user_id, @showtime_id, @original_price, @total_price);" +
                    "                   SELECT SCOPE_IDENTITY();";
                command.Parameters.AddWithValue("@user_id", newBooking.UserId);
                command.Parameters.AddWithValue("@showtime_id", newBooking.ShowtimeId);
                command.Parameters.AddWithValue("@original_price", newBooking.OriginalPrice);
                command.Parameters.AddWithValue("@total_price", newBooking.OriginalPrice);
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out bookingId))
                {
                    CreateBookingSeat(bookingId, newBooking.SelectedSeats);
                }
                connection.Close();
            }
            return bookingId;
        }
        public void CreateBookingSeat(int bookingId, ObservableCollection<Seat> seats)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [BookingSeats] (booking_id, seat_id) " +
                                      "VALUES (@booking_id, @seat_id)";
                command.Parameters.AddWithValue("@booking_id", bookingId);

                foreach (var seat in seats)
                {
                    command.Parameters.Clear(); 
                    command.Parameters.AddWithValue("@booking_id", bookingId);
                    command.Parameters.AddWithValue("@seat_id", seat.SeatId); 
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        ChangeSeatStatus(seats); 
                    }
                }
                connection.Close();
            }
        }

        public void ChangeSeatStatus(ObservableCollection<Seat> seats)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Update [Seats] SET status = 'Sold' WHERE seat_id = @seat_id"; 

                foreach (var seat in seats)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@seat_id", seat.SeatId);
                    int rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
