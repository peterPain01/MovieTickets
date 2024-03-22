using Netflix.Model;
using Netflix.Repository;
using Netflix.Utils;
using NetFlix.CustomControls;
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
        private int _totalPrice;
        public Movie Movie { get => movie; set { movie = value; OnPropertyChanged(nameof(Movie)); } }
        private ObservableCollection<ShowTime> showtimes;
        private ObservableCollection<ShowTime> showtimes_selected;
        private List<ShowTime> day_showtimes;
        private ObservableCollection<Seat> seat_of_selected_showtime;
        private bool _isSelectedShowTime = false;
        private ShowTime _selectedShowtime;
        private ObservableCollection<Seat> _selectedSeats = new ObservableCollection<Seat>();

        public bool IsSelectedShowTime
        {
            get { return _isSelectedShowTime; }
            set { _isSelectedShowTime = value; OnPropertyChanged(nameof(IsSelectedShowTime)); }
        }

        public ShowTime SelectedShowTime
        {
            get { return _selectedShowtime; }
            set { _selectedShowtime = value; OnPropertyChanged(nameof(SelectedShowTime)); }
        }
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }
        public int TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        public string SelectedDay
        {
            get { return selectedDay; }
            set { selectedDay = value; OnPropertyChanged(nameof(SelectedDay)); }
        }

        public ObservableCollection<Seat> SeatOfSelectedShowtime
        {
            get { return seat_of_selected_showtime; }
            set
            {
                seat_of_selected_showtime = value;
                OnPropertyChanged(nameof(SeatOfSelectedShowtime));
            }
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

        // Store List of Showtime with distinct day -> It's should be just store date
        public List<ShowTime> DayHaveShowTimes
        {
            get { return day_showtimes; }
            set
            {
                day_showtimes = value;
                OnPropertyChanged("DayHaveShowTimes");
            }
        }

        // Store all showtimes in day user selected 
        public ObservableCollection<ShowTime> ShowTimesSelected
        {
            get => showtimes_selected;
            set
            {
                showtimes_selected = value;
                OnPropertyChanged("ShowTimesSelected");
            }
        }

        public ObservableCollection<Seat> SelectedSeats
        {
            get => _selectedSeats;
            set
            {
                _selectedSeats = value;
                OnPropertyChanged("SelectedSeats");
            }
        }

        public ICommand ChangeSelectedDay { get; set; }
        public ICommand CheckoutCommand { get; set; }
        public ICommand GetSeatsCommand { get; set; }
        public ICommand SeatClickedCommand { get; set; }
        private MovieRepository movieRepo { get; set; }
        private ShowTimeRepo ShowTimeRepo { get; set; }
        private BookingRepository BookingRepo { get; set; }

        public MovieViewModel(int id)
        {
            Id = id;
            movieRepo = new MovieRepository();
            ShowTimeRepo = new ShowTimeRepo();
            BookingRepo = new BookingRepository();
            
            Movie = movieRepo.GetMovieById(id);

            ChangeSelectedDay = new ViewModelCommand(ExecuteChangeSelectedDay);
            CheckoutCommand = new ViewModelCommand(ExecuteCheckoutCommand);
            GetSeatsCommand = new ViewModelCommand(ExecuteGetSeatsCommand);
            SeatClickedCommand = new ViewModelCommand(ExecuteSeatClickedCommand);

            SeatOfSelectedShowtime = new ObservableCollection<Seat>();

            ShowTimes = ShowTimeRepo.GetShowTimeByMovieId(id);
            FilterShowTimeByDay();
        }

        private void ExecuteSeatClickedCommand(object parameter)
        {
            Seat seat = (Seat)parameter;
            if (SelectedSeats.Any(s => s.row == seat.row && s.number == seat.number))
            {
                SelectedSeats.Remove(seat);
            }
            else
            {
                SelectedSeats.Add(seat);
            }
            TotalPrice = SelectedSeats.Sum(s => s.Price);
            OnPropertyChanged("SelectedSeats");
            OnPropertyChanged(nameof(SeatOfSelectedShowtime));
        }
        private void ExecuteGetSeatsCommand(object parameter)
        {
            int ShowtimeId = (int)parameter;
            SelectedShowTime = ShowTimes.FirstOrDefault(s => s.ShowTimeId == ShowtimeId);

            SeatOfSelectedShowtime = ShowTimeRepo.GetSeatByShowtimeId(ShowtimeId);
            if (SeatOfSelectedShowtime.Count > 0)
            {
                IsSelectedShowTime = true;
            }
            else { IsSelectedShowTime = false; }
        }
        private void ExecuteCheckoutCommand(object parameter)
        {
            BookingModel newBooking = new BookingModel
            {
                ShowtimeId = SelectedShowTime.ShowTimeId,
                UserId = UserRepository.CurrentUser.Id,
                SelectedSeats = SelectedSeats,
                OriginalPrice = TotalPrice,
                TotalPrice = TotalPrice
            };

            BookingRepo.CreateBooking(newBooking);
            NavigationStore._navigationStore.CurrentViewModel = new UserViewModel(); 
        }

        private void ExecuteChangeSelectedDay(object parameter)
        {
            IsSelectedShowTime = false;
            SeatOfSelectedShowtime.Clear();
            string date = (string)parameter;
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
