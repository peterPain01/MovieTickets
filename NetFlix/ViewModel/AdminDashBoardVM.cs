using Netflix.Model;
using Netflix.Repository;
using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        
        public int TotalShowTime
        {
            get { return _totalShowtime; }
            set
            {
                _totalShowtime = value;
                OnPropertyChanged("TotalShowTime");
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

        public ICommand GetTrendingMovie { get; set; }
        public ICommand GetTotalShowTime { get; set; }
        private MovieRepository movieRepo;

        public AdminDashBoardVM()
        {
            // Initital ICommand 
            GetTrendingMovie = new ViewModelCommand(ExecuteGetTrendingMovie);
            GetTotalShowTime = new ViewModelCommand(ExecutetGetTotalShowTime);

            // Initital Variable 
            movieRepo = new MovieRepository();

            TrendingMovies = movieRepo.GetTrendingMovie(); 
            InitTotalShowtime();
            InitTotalNowShowing(); 
        }

        private void ExecuteGetTrendingMovie(object parameter)
        {

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
