using Netflix.Model;
using Netflix.Repository;
using Netflix.Utils;
using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<MovieModel> movies;
        private MovieRepository movieRepo;


        private object _selectedItem;
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
        public ObservableCollection<MovieModel> Items
        {
            get { return movies; }
            set
            {
                movies = value;
                OnPropertyChanged("Items");
            }
        }

        public SearchViewModel(string searchPattern)
        {
            movieRepo = new MovieRepository();
            Items = movieRepo.GetMovieByName(searchPattern);
        }

        private void SelectionChanged()
        {
            string selectOption = SelectedItem.ToString();
            string[] split = selectOption.Split('-');
            string field = split[0];
            string mode = split[1];
            SortMovies(field, mode);
        }

        private void SortMovies(string field, string mode)
        {
            if(field.Equals("rating") && mode.Equals("low"))
                Items = new ObservableCollection<MovieModel>(Items.OrderBy(o => o.Rating));
            else if (field.Equals("rating") && mode.Equals("high"))
                Items = new ObservableCollection<MovieModel>(Items.OrderByDescending(o => o.Rating));
            else if (field.Equals("run") && mode.Equals("low"))
                Items = new ObservableCollection<MovieModel>(Items.OrderBy(o => o.Duration_minutes));
            else if (field.Equals("run") && mode.Equals("high"))
                Items = new ObservableCollection<MovieModel>(Items.OrderByDescending(o => o.Duration_minutes));
            return;
        }

    }
}
