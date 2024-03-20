using Netflix.Model;
using Netflix.Repository;
using Netflix.Utils;
using NetFlix.Model;
using NetFlix.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using XAct;

// Having fews of Show time per Movie 
// so I fetched all Showtime Data and Filtering them on Client :) 

namespace NetFlix.ViewModel
{
    public class MovieViewModel : ViewModelBase
    {
        private int id;
        private string selectedDay; 
        private Movie movie;
        public Movie Movie { get => movie; set { movie = value; OnPropertyChanged(nameof(Movie)); } }
        private ObservableCollection<ShowTime> showtimes;
        private ObservableCollection<ShowTime> showtimes_selected; 
        private List<ShowTime> day_showtimes;


        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }
        public string SelectedDay
        {
            get { return selectedDay; }
            set { selectedDay = value; OnPropertyChanged(nameof(SelectedDay)); }
        }

        public ObservableCollection<ShowTime> ShowTimes
        {
            get { return showtimes; }
            set
            {
                showtimes = value;
                OnPropertyChanged("ShowTimes");
            }
        }
        public List<ShowTime> DayHaveShowTimes
        {
            get { return day_showtimes; }
            set
            {
                day_showtimes = value;
                OnPropertyChanged("DayHaveShowTimes");
            }
        }

        public ObservableCollection<ShowTime> ShowTimesSelected
        {
            get => showtimes_selected;
            set
            {
                showtimes_selected = value;
                OnPropertyChanged("ShowTimesSelected"); 
            }
        }

        public ICommand NavigateToBookingForm { get;  } 
        public ICommand ChangeSelectedDay { get; set; }
        public ICommand CheckoutCommand { get; set;  }
        
        private MovieRepository movieRepo { get; set; }
        private ShowTimeRepo ShowTimeRepo { get; set; }

        public MovieViewModel(int id)
        {
            Id = id;
            movieRepo = new MovieRepository();
            ShowTimeRepo = new ShowTimeRepo();
            Movie = movieRepo.GetMovieById(id);
            NavigateToBookingForm = new ViewModelCommand(ExecuteNavigateToBookingForm);
            ChangeSelectedDay = new ViewModelCommand(ExecuteChangeSelectedDay);
            CheckoutCommand = new ViewModelCommand(ExecuteCheckoutCommand);
            ShowTimes = ShowTimeRepo.GetShowTimeByMovieId(id);
            FilterShowTimeByDay();
        }

        private void ExecuteCheckoutCommand(object parameter)
        {
            NavigationStore._navigationStore.CurrentViewModel = new UserViewModel();
        }
        private void ExecuteNavigateToBookingForm(object parameter)
        {
            NavigationStore._navigationStore.CurrentViewModel = new Booking(Id); 
        }

        private void ExecuteChangeSelectedDay(object parameter)
        {
            string date = (string) parameter;
            SelectedDay = date;
            ShowTimesSelected = new ObservableCollection<ShowTime>(ShowTimes.Where(showtime => showtime.Day.Equals(selectedDay))
                               .Select(showtime => showtime).ToList());
        }

        private void FilterShowTimeByDay()
        {
            DayHaveShowTimes = ShowTimes.GroupBy(showtime => showtime.Day) // ->>  need more constrant here 
                              .Select(grp => grp.First())    
                               .ToList();
            selectedDay = DayHaveShowTimes.ElementAt(0).Day;
            ShowTimesSelected = new ObservableCollection<ShowTime>(ShowTimes.Where(showtime => showtime.Day.Equals(selectedDay))
                                .Select(showtime => showtime).ToList());  
        }

    }
}
