using Netflix.Model;
using NetFlix.Model;
using NetFlix.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using XAct;
using static NetFlix.View.SearchPage;
using static NetFlix.ViewModel.LandingViewModel;

namespace Netflix.Repository
{
    public class MovieRepository : RepositoryBase
    {
        public ObservableCollection<Movie> GetNowShowingMovie()
        {
            ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT m.* " +
                    "FROM Movies m " +
                    "JOIN (SELECT DISTINCT mv.movie_id " +
                    "FROM Movies mv " +
                    "JOIN Showtimes st ON mv.movie_id = st.movie_id " +
                    "WHERE st.showtime_datetime > GETDATE()) " +
                    "AS sub ON m.movie_id = sub.movie_id;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Movie movie = ProcessMovie(reader);
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }

        public ObservableCollection<Movie> GetTrendingMovie()
        {
            ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "WITH TEMP as(" +
                    "                   select TOP(5) st.movie_id as id, COUNT(*) as total from Bookings bk, Showtimes st" +
                    "                   where bk.showtime_id = st.showtime_id" +
                    "                   GROUP BY st.movie_id" +
                    "                   HAVING st.movie_id IN (select distinct mv.movie_id from Movies mv, Showtimes st" +
                    "                   WHERE st.movie_id = mv.movie_id" +
                    "                   and st.showtime_datetime > GETDATE())) " +
                    "                   select * from Movies mv, TEMP t where mv.movie_id = t.id";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Movie movie = ProcessMovie(reader);
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }

        public int GetTotalShowTime(string filter)
        {
            int total = 0;
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    DateTime startDate;
                    DateTime endDate;

                    switch (filter.ToLower())
                    {
                        case "today":
                            startDate = DateTime.Today;
                            endDate = startDate.AddDays(1);
                            break;
                        case "month":
                            startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                            endDate = startDate.AddMonths(1);
                            break;
                        case "week":
                            startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                            endDate = startDate.AddDays(7);
                            break;
                        default:
                            throw new ArgumentException("Invalid filter value. Supported values are 'today', 'month', and 'week'.");
                    }

                    command.CommandText = "SELECT COUNT(DISTINCT showtime_id) AS total FROM Showtimes " +
                                  "WHERE showtime_datetime >= @StartDate AND showtime_datetime < @EndDate";

                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            total = reader.GetInt32(reader.GetOrdinal("total"));
                        }
                    }
                }
            }
            return total;
        }

        // Merge 2 funs
        public int GetTotalNowShowing()
        {
            int total = 0;
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(DISTINCT movie_id) AS total FROM Showtimes " +
                                          "WHERE showtime_datetime > GETDATE()";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            total = reader.GetInt32(reader.GetOrdinal("total"));
                        }
                    }
                }
            }
            return total;
        }

        public (ObservableCollection<Movie>, int) GetMovieByName(string title, int page = 1, string filter = "", string sort = "", string sort_type = "")
        {
            ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
            int PAGE_SIZE = 3;
            int totalRecords = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                            SELECT COUNT(*) FROM Movies
                            WHERE UPPER(title) LIKE UPPER('%' + @title + '%')
                            ";
                command.Parameters.AddWithValue("@title", title);
                totalRecords = (int)command.ExecuteScalar();

                // SORT 
                string sort_query = "movie_id";
                if (!string.IsNullOrEmpty(sort))
                {
                    sort_query = $"{sort} {sort_type}";
                }

                // FILTER 
                string filter_query = "";
                if (!string.IsNullOrEmpty(filter))
                {
                    filter_query = $" AND genre_id = {filter}";
                }
                command.CommandText = @"
                                        SELECT * FROM Movies
                                        WHERE UPPER(title) LIKE UPPER('%' + @title + '%')"
                                        + filter_query + @"
                                        ORDER BY " + sort_query + @"
                                        OFFSET @page_from ROWS 
                                        FETCH NEXT @PAGE_SIZE ROWS ONLY 
                                        ";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@page_from", (page - 1) * PAGE_SIZE);
                command.Parameters.AddWithValue("@PAGE_SIZE", PAGE_SIZE);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Movie movie = ProcessMovie(reader);
                        movies.Add(movie);
                    }
                }
            }
            return (movies, totalRecords);
        }
        public Movie GetMovieById(int id)
        {
            Movie movie = new Movie();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Movies WHERE movie_id = @id";
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        movie = ProcessMovie(reader);
                    }
                }
            }
            return movie;
        }

        public ObservableCollection<Genre> GetAllGenre()
        {
            ObservableCollection<Genre> genres = new ObservableCollection<Genre>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Genres";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Genre genre = new Genre
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("genre_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")) // Assuming "name" is the correct column for genre name
                        };
                        genres.Add(genre);
                    }
                }
            }
            return genres;
        }

        public void GetMovieByCategory(string categorie) { }

        public void GetMovieByRating(int rating) { }
        public void GetMovieByCertficate(int certificate) { }


        // handle Null Value
        // handle Null Value
        // handle Null Value
        // handle Null Value
        // handle Null Value
        public Movie ProcessMovie(SqlDataReader reader)
        {
            Movie movie = new Movie();
            movie.Id = reader.GetInt32(reader.GetOrdinal("movie_id"));
            movie.Title = reader.GetString(reader.GetOrdinal("title"));
            movie.GenreId = reader.GetInt32(reader.GetOrdinal("genre_id"));
            movie.DurationMinutes = reader.GetInt32(reader.GetOrdinal("duration_minutes"));
            movie.Rating = Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("rating")));
            movie.Certification = reader.GetInt32(reader.GetOrdinal("Certification"));
            movie.PlotSummary = reader.GetString(reader.GetOrdinal("plot_summary"));
            movie.PosterUrl = reader.GetString(reader.GetOrdinal("poster_url"));
            movie.PosterVerticalUrl = reader.GetString(reader.GetOrdinal("poster_vertical_url"));
            movie.TrailerUrl = reader.GetString(reader.GetOrdinal("trailer_url"));
            movie.DirectorId = reader.GetInt32(reader.GetOrdinal("director_id"));
            //movie.Release_date = reader.GetDateTime(reader.GetOrdinal("realease_date"));

            return movie;
        }
    }
}
