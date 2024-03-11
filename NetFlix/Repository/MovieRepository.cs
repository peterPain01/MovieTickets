using Netflix.Model;
using NetFlix.Model;
using NetFlix.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using static NetFlix.ViewModel.LandingViewModel;

namespace Netflix.Repository
{
    public class MovieRepository : RepositoryBase
    {
        public ObservableCollection<MovieModel> GetTrendingMovie()
        {
            ObservableCollection<MovieModel> movies = new ObservableCollection<MovieModel>();
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
                        MovieModel movie = ProcessMovie(reader);
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }

        public void GetNowShowingMovie()
        {

        }

        public void GetMovieByName(string name)
        {

        }
        public MovieModel GetMovieById(int id) 
        {
            MovieModel movie = new MovieModel();

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

        public void GetMovieByCategory(string categorie) { }

        public void GetMovieByRating(int rating) { }
        public void GetMovieByCertficate(int certificate) { }

        public MovieModel ProcessMovie(SqlDataReader reader)
        {
            MovieModel movie = new MovieModel();
            movie.Id = reader.GetInt32(reader.GetOrdinal("movie_id"));
            movie.Title = reader.GetString(reader.GetOrdinal("title"));
            movie.Genre_id = reader.GetInt32(reader.GetOrdinal("genre_id"));
            movie.Duration_minutes = reader.GetInt32(reader.GetOrdinal("duration_minutes"));
            movie.Rating = Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("rating")));
            movie.Certification = reader.GetInt32(reader.GetOrdinal("Certification"));
            movie.Plot_summary = reader.GetString(reader.GetOrdinal("plot_summary"));
            movie.Poster_url = reader.GetString(reader.GetOrdinal("poster_url"));
            movie.Trailer_url = reader.GetString(reader.GetOrdinal("trailer_url"));
            movie.Director_id = reader.GetInt32(reader.GetOrdinal("director_id"));
            //movie.Release_date = reader.GetDateTime(reader.GetOrdinal("realease_date"));

            return movie;
        }
    }
}
