using NetFlix.EnityModel;
using NetFlix.Repository;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Netflix.Repository
{
    public class MovieRepository : RepositoryBase
    {
        public ObservableCollection<Movie> GetNowShowingMovie()
        {
            using (var context = new BookingMovieAppContext())
            {
                var currentTime = DateTime.Now;
                var tempQuery = (from st in context.Showtimes
                                 join mv in context.Movies on st.MovieId equals mv.MovieId
                                 where st.ShowtimeDatetime > currentTime
                                 select mv.MovieId).Distinct();

                ObservableCollection<Movie> movies = new ObservableCollection<Movie>(from mv in context.Movies
                                                                                     join temp in tempQuery on mv.MovieId equals temp
                                                                                     select mv);
                return movies;
            }
        }
        public ObservableCollection<Movie> GetAllMovies()
        {
            using (var context = new BookingMovieAppContext())
            {
                var movies = new ObservableCollection<Movie>(
                        context.Movies.ToList()
                    );

                return movies;

            }
        }


        public ObservableCollection<Movie> GetTrendingMovie()
        {
            //using (var context = new BookingMovieAppContext())
            //{
            //    var currentTime = DateTime.Now;
            //    var tempQuery = (from bk in context.Bookings
            //                     join st in context.Showtimes on bk.ShowtimeId equals st.ShowtimeId
            //                     group st by st.MovieId into g
            //                     where g.Any(s => s.ShowtimeDatetime > currentTime)
            //                     orderby g.Count() descending
            //                     select new { Id = g.Key, Total = g.Count() }).Take(5);

            //    var moviesQuery = from mv in context.Movies
            //                      join temp in tempQuery on mv.MovieId equals temp.Id
            //                      select mv;
            //}
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

        public ObservableCollection<Movie> GetMovieforAdvanceSearch(string pattern)
        {
            using(var context = new BookingMovieAppContext())
            {
                var movies = context.Movies.Where(mv => mv.Title.Contains(pattern)).ToList();  
                return new ObservableCollection<Movie>(movies); 
            }
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
            using (var context = new BookingMovieAppContext())
            {
                var stars = context.Stars.Where(s => s.Movies.Any(m => m.MovieId == id)).ToList();
                var mv = context.Movies.Where(mv => mv.MovieId == id)
                            .Include("Stars") // Why include not work 
                            .FirstOrDefault();
                if(stars != null && mv != null)
                    mv.Stars = stars; 
                return mv;
            }
        }

        public ObservableCollection<Genre> GetAllGenre()
        {
            using (var context = new BookingMovieAppContext())
            {
                ObservableCollection<Genre> genres = new ObservableCollection<Genre>(
                        context.Genres.ToList()
                    );
                return genres;
            }
        }

        public void GetMovieByCategory(string categorie) { }

        public void GetMovieByRating(int rating) { }
        public void GetMovieByCertficate(int certificate) { }

        public void CreateMovie(Movie newMovie)
        {
            using (var context = new BookingMovieAppContext())
            {
                context.Add(newMovie);
                context.SaveChanges();
            }
        }

        public void DeleteMovie(Movie movieDelete)
        {
            using(var context = new BookingMovieAppContext())
            {
                var movie = context.Movies.FirstOrDefault(mv => mv.MovieId == movieDelete.MovieId); 
                if(movie != null)
                {
                    context.Remove(movie);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateMovie(Movie newMovie)
        {
            using(var context = new BookingMovieAppContext())
            {
                Movie movie = context.Movies.FirstOrDefault(mv => mv.MovieId == newMovie.MovieId);

                if (movie != null)
                {
                    movie.Title = newMovie.Title;
                    movie.DurationMinutes = newMovie.DurationMinutes;
                    movie.ReleaseDate = newMovie.ReleaseDate;
                    movie.Rating = newMovie.Rating;
                    movie.Certification = newMovie.Certification;
                    movie.PlotSummary = newMovie.PlotSummary;
                    movie.PosterUrl = newMovie.PosterUrl;
                    movie.PosterVerticalUrl = newMovie.PosterVerticalUrl;
                    context.SaveChanges();
                }
            }
        }

        // handle Null Value
        // handle Null Value
        // handle Null Value
        // handle Null Value
        // handle Null Value
        public Movie ProcessMovie(SqlDataReader reader)
        {
            Movie movie = new Movie();
            movie.MovieId = reader.GetInt32(reader.GetOrdinal("movie_id"));
            movie.Title = reader.GetString(reader.GetOrdinal("title"));
            movie.DurationMinutes = reader.GetInt32(reader.GetOrdinal("duration_minutes"));
            movie.Rating = Convert.ToDecimal(reader.GetDecimal(reader.GetOrdinal("rating")));
            movie.Certification = reader.GetInt32(reader.GetOrdinal("Certification"));
            movie.PlotSummary = reader.GetString(reader.GetOrdinal("plot_summary"));
            movie.PosterUrl = reader.GetString(reader.GetOrdinal("poster_url"));
            movie.PosterVerticalUrl = reader.GetString(reader.GetOrdinal("poster_vertical_url"));
            movie.TrailerUrl = reader.GetString(reader.GetOrdinal("trailer_url"));
            //movie.Release_date = reader.GetDateTime(reader.GetOrdinal("realease_date"));

            return movie;
        }
    }
}
