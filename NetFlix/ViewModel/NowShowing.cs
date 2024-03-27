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
    public class NowShowing : ViewModelBase
    {
        private ObservableCollection<Movie> _allMovies;

        public ObservableCollection<Movie> AllMovies
        {
            get { return _allMovies; }
            set
            {
                _allMovies = value;
                OnPropertyChanged(nameof(AllMovies));   
            }
        }

        public ICommand NavigateToMoviePage { get; set; }

        public NowShowing(MovieRepository movieRepo)
        {
            AllMovies = movieRepo.GetAllMovies();
            NavigateToMoviePage = new ViewModelCommand(ExecuteNavigatetoMoviePage);
        }

        private void ExecuteNavigatetoMoviePage(object parameter)
        {
            int id = (int)parameter;
            NavigationStore._navigationStore.CurrentViewModel = new MovieViewModel(id);
        }
    }
}
