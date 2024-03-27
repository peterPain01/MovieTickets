using Netflix.Repository;
using Netflix.Utils;
using NetFlix.EnityModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetFlix.ViewModel
{
    public class AdvanceSearch : ViewModelBase
    {
        private ObservableCollection<Movie> _searchingMovie;

        public ObservableCollection<Movie> SearchingMovies
        {
            get { return _searchingMovie; }
            set
            {
                _searchingMovie = value;
                OnPropertyChanged(nameof(SearchingMovies));
            }
        }

        public ICommand SearchingCommand { get; set; }
        public ICommand BacktoHomePage { get; set; }

        private MovieRepository movieRepository;
        public AdvanceSearch()
        {
            SearchingCommand = new ViewModelCommand(ExeSearchingCommand);
            BacktoHomePage = new ViewModelCommand(ExeBacktoHomePage);
            movieRepository = new MovieRepository();
        }

        public void ExeSearchingCommand(object p)
        {
            string pattern = (String)p;
            int temp;
            (SearchingMovies) = movieRepository.GetMovieforAdvanceSearch(pattern);
        }

        public void ExeBacktoHomePage(object p)
        {
            NavigationStore._navigationStore.CurrentViewModel = NavigationStore._navigationStore.PrevViewModel; 
        }
    }
}
