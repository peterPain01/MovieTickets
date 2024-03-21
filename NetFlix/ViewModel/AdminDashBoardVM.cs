using Netflix.Model;
using Netflix.Repository;
using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetFlix.ViewModel
{
    public class AdminDashBoardVM : ViewModelBase
    {
        private ObservableCollection<Movie> trending_movie;

        public ObservableCollection<Movie> TrendingMovies
        {
            get { return trending_movie; }
            set
            {
                trending_movie = value;
                OnPropertyChanged("TrendingMovies");
            }
        }

        public ICommand GetTrendingMovie { get; set; }
        private MovieRepository movieRepo;

        public AdminDashBoardVM()
        {
            // Initital ICommand 
            GetTrendingMovie = new ViewModelCommand(ExecureGetTrendingMovie);

            // Initital Variable 
            TrendingMovies = new ObservableCollection<Movie>
        {
            new Movie {
                Id = 1,
                Title = "The Matrix",
                GenreId = 1,
                DurationMinutes = 136,
                ReleaseDate = new DateTime(1999, 3, 31),
                Rating = 8.7,
                Certification = 13,
                PlotSummary = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                PosterUrl = "https://www.example.com/posters/the_matrix.jpg",
                PosterVerticalUrl = "https://www.example.com/posters_vertical/the_matrix_vertical.jpg",
                TrailerUrl = "https://www.youtube.com/watch?v=m8e-FF8MsqU",
                DirectorId = 1
            },
            new Movie {
                Id = 2,
                Title = "Inception",
                GenreId = 1,
                DurationMinutes = 148,
                ReleaseDate = new DateTime(2010, 7, 16),
                Rating = 8.8,
                Certification = 13,
                PlotSummary = "A thief who enters the dreams of others to steal secrets from their subconscious gets a shot at redemption when he is offered a task: plant an idea into the mind of a CEO.",
                PosterUrl = "https://www.example.com/posters/inception.jpg",
                PosterVerticalUrl = "https://www.example.com/posters_vertical/inception_vertical.jpg",
                TrailerUrl = "https://www.youtube.com/watch?v=YoHD9XEInc0",
                DirectorId = 2
            },
            new Movie {
                Id = 3,
                Title = "The Dark Knight",
                GenreId = 1,
                DurationMinutes = 152,
                ReleaseDate = new DateTime(2008, 7, 18),
                Rating = 9.0,
                Certification = 13,
                PlotSummary = "When the menace known as The Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                PosterUrl = "https://www.example.com/posters/the_dark_knight.jpg",
                PosterVerticalUrl = "https://www.example.com/posters_vertical/the_dark_knight_vertical.jpg",
                TrailerUrl = "https://www.youtube.com/watch?v=EXeTwQWrcwY",
                DirectorId = 3
            }
        };
            movieRepo = new MovieRepository();
        }

        private void ExecureGetTrendingMovie(object parameter)
        {

        }
    }
}
