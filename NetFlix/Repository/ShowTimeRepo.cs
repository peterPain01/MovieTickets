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
        public ObservableCollection<ShowTime> GetShowTimeByMovieId(int id)
        {
            ObservableCollection<ShowTime> showtimes = new ObservableCollection<ShowTime>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *, FORMAT(showtime_datetime, 'dd/MM/yyyy HH:mm:ss') as formatedDateTime" +
                    "                       from showtimes st, Cinema cn " +
                    "                       where st.cinema_id = cn.cinema_id " +
                    "                       and st.movie_id = @movie_id";
                command.Parameters.AddWithValue("@movie_id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShowTime st = ProcessShowTime(reader);
                        showtimes.Add(st);
                    }

                }
            }
            return showtimes;
        }

        private ShowTime ProcessShowTime(SqlDataReader reader)
        {
            ShowTime st = new ShowTime();
            st.ShowTimeId= reader.GetInt32(reader.GetOrdinal("showtime_id"));
            st.CinemaId= reader.GetInt32(reader.GetOrdinal("cinema_id"));
            st.MovieId= reader.GetInt32(reader.GetOrdinal("movie_id"));
            st.ShowTimeDateTime = reader.GetString(reader.GetOrdinal("formatedDateTime"));
            st.CinemaName = reader.GetString(reader.GetOrdinal("name"));

            DateTime dateTime = DateTime.ParseExact(st.ShowTimeDateTime.ToString(), "dd/MM/yyyy HH:mm:ss", null);
            st.Day = dateTime.Day.ToString();
            st.Month = dateTime.Month.ToString();
            st.Hour = dateTime.Hour.ToString();
            st.Minute = dateTime.Hour < 12 ? dateTime.Minute.ToString("00 AM") : dateTime.Minute.ToString("00 PM");
            st.DayOfWeek = dateTime.DayOfWeek;
            return st; 
        }
    }
}
