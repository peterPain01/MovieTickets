using Netflix.Repository;
using NetFlix.EnityModel;
using NetFlix.View.Admin;
using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XAct.Messages;

namespace NetFlix.ViewModel
{
    public class AdminDashBoardVM : ViewModelBase
    {
        // Properties
        private int _totalShowtime;
        private int _totalNowShowing;
        private string _selectedShowtimeFilter = "today";
        private UserControl _currentMainSection; 

        public int TotalShowTime
        {
            get { return _totalShowtime; }
            set
            {
                _totalShowtime = value;
                OnPropertyChanged("TotalShowTime");
            }
        }

        public UserControl CurrentMainSection
        {
            get { return _currentMainSection; }
            set
            {
                _currentMainSection = value;
                OnPropertyChanged("CurrentMainSection");
            }
        }

        public int TotalNowShowing
        {
            get { return _totalNowShowing; }
            set
            {
                _totalNowShowing = value;
                OnPropertyChanged("TotalShowTime");
            }
        }

        public string SelectedShowtimeFilter
        {
            get { return _selectedShowtimeFilter; }
            set
            {
                _selectedShowtimeFilter = value;
                OnPropertyChanged("SelectedShowtimeFilter");
            }
        }

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

        public ICommand GetTotalShowTime { get; set; }
        public ICommand NavigateToFeature { get; set; }
        public ICommand OpenAddMovieForm { get; set; }

        private MovieRepository movieRepo;

        public AdminDashBoardVM()
        {
            // Initital ICommand 
            GetTotalShowTime = new ViewModelCommand(ExecutetGetTotalShowTime);
            NavigateToFeature = new ViewModelCommand(ExecuteNavigateToFeature);
            OpenAddMovieForm = new ViewModelCommand(ExecuteOpenAddMovieForm); 
            // Initital Variable 
            movieRepo = new MovieRepository();
            CurrentMainSection = new View.Admin.AdminDashBoard(); 

            TrendingMovies = movieRepo.GetTrendingMovie(); 
            InitTotalShowtime();
            InitTotalNowShowing(); 
        }

        private void ExecuteNavigateToFeature(object parameter)
        {
            string section = (string)parameter;
            switch (section)
            {
                case "Movie":
                    CurrentMainSection = new View.Admin.AdminMovie();
                    break;
                case "Dashboard":
                    CurrentMainSection = new View.Admin.AdminDashBoard();
                    break;
            } 
        }

        private void ExecuteOpenAddMovieForm(object parameter)
        {
            var form = new AddMovieForm(); 
            form.Show();
        }


        private void InitTotalNowShowing() 
        {
            TotalNowShowing = movieRepo.GetTotalNowShowing();
        }

        private void InitTotalShowtime()
        {
            TotalShowTime = movieRepo.GetTotalShowTime("today");
        }

        private void ExecutetGetTotalShowTime(object parameter)
        {
            string filter = (string)parameter;
            SelectedShowtimeFilter = filter;
            TotalShowTime = movieRepo.GetTotalShowTime(filter);
        }
        
    }
}
