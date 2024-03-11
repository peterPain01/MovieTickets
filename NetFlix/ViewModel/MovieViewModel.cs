using Netflix.Model;
using Netflix.Repository;
using Netflix.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetFlix.ViewModel
{
    public class MovieViewModel : ViewModelBase
    {
        // Navigation 
        private NavigationStore _navigationStore;
        private int id;
        private MovieModel movie;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public MovieModel Movie { get => movie; set { movie = value; OnPropertyChanged(nameof(Movie)); } }

        private MovieRepository movieRepo;
        public MovieViewModel(NavigationStore navigationStore, int id)
        {
            this._navigationStore = navigationStore;
            Id = id;
            movieRepo = new MovieRepository();
            Movie = movieRepo.GetMovieById(id);
        }
    }
}
