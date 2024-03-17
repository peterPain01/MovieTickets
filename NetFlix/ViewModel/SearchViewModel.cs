using Netflix.Model;
using Netflix.Repository;
using Netflix.Utils;
using NetFlix.View;
using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NetFlix.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        private NavigationStore _navigateStore;
        private ObservableCollection<Movie> movies;
        private ObservableCollection<Genre> genres;
        private MovieRepository movieRepo;
        private int _totalRecords;
        private int _page = 1;
        private string _searchPattern;
        private string _sortColumns;
        private string _sortMode;

        private object _selectedItem;
        private object _selectedGenreItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    SelectionChanged();
                }

            }
        }
        public object SelectedGenreItem
        {
            get { return _selectedGenreItem; }
            set
            {
                if (_selectedGenreItem != value)
                {
                    _selectedGenreItem = value;
                    OnPropertyChanged(nameof(SelectedGenreItem));
                    SelectionGenresChanged();
                }

            }
        }

        public int TotalRecords
        {
            get { return _totalRecords; }
            set
            {
                if (_totalRecords != value)
                {
                    _totalRecords = value;
                    OnPropertyChanged(nameof(TotalRecords));
                    SelectionChanged();
                }

            }
        }
        public int CurrentPage
        {
            get { return _page; }
            set
            {
                _page = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public ObservableCollection<Movie> Items
        {
            get { return movies; }
            set
            {
                movies = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ObservableCollection<Genre> Genres
        {
            get { return genres; }
            set
            {
                genres = value;
                OnPropertyChanged(nameof(Genres));
            }
        }
        public ICommand PaginateCommand;
        public ICommand GetAllGenre;

        public ICommand NavigateToMoviePage { get; set; }
        public SearchViewModel(string searchPattern)
        {
            movieRepo = new MovieRepository();
            PaginateCommand = new ViewModelCommand(ExecutePaginate);
            NavigateToMoviePage = new ViewModelCommand(ExecuteNavigateToMoviePage); 
            _searchPattern = searchPattern;
            _sortColumns = "";
            _sortMode = "";

            Genres = movieRepo.GetAllGenre();
            (Items, TotalRecords) = movieRepo.GetMovieByName(searchPattern);
        }

        private void SelectionChanged()
        {
            if (SelectedItem == null)
                return;
            string selectOption = SelectedItem.ToString();
            string[] split = selectOption.Split('-');
            string field = split[0];
            string mode = split[1];
            SortMovies(field, mode);
        }
        private void SelectionGenresChanged()
        {
            if (SelectedGenreItem == null)
                return;
            FilterMovie(SelectedGenreItem.ToString());
        }

        private string genre_id = "";
        private void FilterMovie(string id)
        {
            genre_id = id; 
            (Items, TotalRecords) = movieRepo.GetMovieByName(_searchPattern, _page, id, _sortColumns, _sortMode);
        }
        private void SortMovies(string field, string mode)
        {
            _sortColumns = field;
            _sortMode = "ASC";
            if (mode.Equals("high"))
                _sortMode = "DESC";
            if (field.Equals("rating") && mode.Equals("low"))
                (Items, TotalRecords) = movieRepo.GetMovieByName(_searchPattern, _page, genre_id, "rating", "ASC");
            else if (field.Equals("rating") && mode.Equals("high"))
                (Items, TotalRecords) = movieRepo.GetMovieByName(_searchPattern, _page, genre_id, "rating", "DESC");
            else if (field.Equals("duration_minutes") && mode.Equals("low"))
                (Items, TotalRecords) = movieRepo.GetMovieByName(_searchPattern, _page, genre_id, "duration_minutes", "ASC");
            else if (field.Equals("duration_minutes") && mode.Equals("high"))
                (Items, TotalRecords) = movieRepo.GetMovieByName(_searchPattern, _page, genre_id, "duration_minutes", "DESC");
            return;
        }

        private void ExecutePaginate(object parameter)
        {
            this._page = (int)parameter;
            (Items, TotalRecords) = movieRepo.GetMovieByName(_searchPattern, _page, genre_id, _sortColumns, _sortMode);
        }
        
        private void ExecuteNavigateToMoviePage(object parameter)
        {
            int id = (int)parameter;
            NavigationStore._navigationStore.CurrentViewModel = new MovieViewModel(id); 
        }
    }
}
