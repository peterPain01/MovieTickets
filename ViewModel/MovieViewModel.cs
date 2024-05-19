using Netflix.Repository;
using Netflix.Utils;
using NetFlix.CustomControls;
using NetFlix.EnityModel;
using NetFlix.Repository;
using NetFlix.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using XAct;
using Voucher = NetFlix.EnityModel.Voucher;

// Having fews of Show time per Movie 
// so I fetched all Showtime Data and Filtering them on Client :) 

namespace NetFlix.ViewModel
{
    public class MovieViewModel : ViewModelBase
    {
        private int id;
        private int selectedDay;
        private Movie movie;
        private decimal _totalPrice;
        private decimal _originalPrice;


        public Movie Movie { get => movie; set { movie = value; OnPropertyChanged(nameof(Movie)); } }
        private ObservableCollection<Showtime> showtimes;
        private ObservableCollection<Showtime> showtimes_selected;
        private List<Showtime> day_showtimes;
        private ObservableCollection<Seat> seat_of_selected_showtime;
        private bool _isSelectedShowTime = false;
        private Showtime _selectedShowtime;
        private ObservableCollection<Seat> _selectedSeats = new ObservableCollection<Seat>();
        private Voucher _voucher;
        private String _voucherMessage;
        private int _totalChairs;
        private int _remainingChairs;

        public bool IsSelectedShowTime
        {
            get { return _isSelectedShowTime; }
            set { _isSelectedShowTime = value; OnPropertyChanged(nameof(IsSelectedShowTime)); }
        }

        public Voucher Voucher
        {
            get { return _voucher; }
            set { _voucher = value; OnPropertyChanged(nameof(Voucher)); }
        }

        public Showtime SelectedShowTime
        {
            get { return _selectedShowtime; }
            set { _selectedShowtime = value; OnPropertyChanged(nameof(SelectedShowTime)); }
        }
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        } 
        
        public int TotalChairs
        {
            get { return _totalChairs; }
            set { _totalChairs = value; OnPropertyChanged(nameof(TotalChairs)); }
        }

        public int RemainingChairs
        {
            get { return _remainingChairs; }
            set { _remainingChairs = value; OnPropertyChanged(nameof(RemainingChairs)); }
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }
        
        public decimal OriginalPrice
        {
            get { return _originalPrice; }
            set { _originalPrice = value; OnPropertyChanged(nameof(OriginalPrice)); }
        }

        public int SelectedDay
        {
            get { return selectedDay; }
            set { selectedDay = value; OnPropertyChanged(nameof(SelectedDay)); }
        }
        public string VoucherMessage
        {
            get { return _voucherMessage; }
            set { _voucherMessage = value; OnPropertyChanged(nameof(VoucherMessage)); }
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
        public ObservableCollection<Showtime> ShowTimes
        {
            get { return showtimes; }
            set
            {
                showtimes = value;
                OnPropertyChanged("ShowTimes");
            }
        }

        // Store List of Showtime with distinct day -> It's should be just store date
        public List<Showtime> DayHaveShowTimes
        {
            get { return day_showtimes; }
            set
            {
                day_showtimes = value;
                OnPropertyChanged("DayHaveShowTimes");
            }
        }

        // Store all showtimes in day user selected 
        public ObservableCollection<Showtime> ShowTimesSelected
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
        public ICommand OpenVoucherForm { get; set; }
        public ICommand ApplyVoucherCommand { get; set; }
        public ICommand NavigateToStarPage { get; set; }

        private MovieRepository movieRepo { get; set; }
        private ShowTimeRepo ShowTimeRepo { get; set; }
        private BookingRepository BookingRepo { get; set; }
        private VoucherRepository voucherRepo { get; set; }

        public MovieViewModel(int id)
        {
            Id = id;
            movieRepo = new MovieRepository();
            ShowTimeRepo = new ShowTimeRepo();
            BookingRepo = new BookingRepository();
            voucherRepo = new VoucherRepository();

            Movie = movieRepo.GetMovieById(id);

            ChangeSelectedDay = new ViewModelCommand(ExecuteChangeSelectedDay);
            CheckoutCommand = new ViewModelCommand(ExecuteCheckoutCommand);
            GetSeatsCommand = new ViewModelCommand(ExecuteGetSeatsCommand);
            SeatClickedCommand = new ViewModelCommand(ExecuteSeatClickedCommand);
            OpenVoucherForm = new ViewModelCommand(ExecuteOpenVoucherForm);
            ApplyVoucherCommand = new ViewModelCommand(ExecuteApplyVoucherCommand);
            NavigateToStarPage = new ViewModelCommand(ExeNavigateToStarPage);

            SeatOfSelectedShowtime = new ObservableCollection<Seat>();

            ShowTimes = ShowTimeRepo.GetShowTimeByMovieId(id);
            FilterShowTimeByDay();
        }

        private void ExeNavigateToStarPage(object parameter)
        {
            int StarId = (int)parameter;
            SubMovieRepo subMovieRepo = new SubMovieRepo();
            NavigationStore._navigationStore.PrevViewModel = this; 
            NavigationStore._navigationStore.CurrentViewModel = new StarModel(StarId, subMovieRepo);
        }
        private decimal DiscountAmount(Voucher voucher, decimal original_price)
        {
            decimal? discountedAmount = voucherRepo.DiscountAmount(voucher, original_price);
            if (discountedAmount.HasValue)
            {
                ToastViewModel vm = new ToastViewModel();
                vm.ShowSuccess("Voucher Successfully Applied");
                window.Close();
                return discountedAmount.Value;
            }
            return -1;
        }
        private void ExecuteApplyVoucherCommand(object parameter)
        {
            string code = (string)parameter;
            var voucher = voucherRepo.CheckValidVoucher(code);
            if (voucher != null)
            {
                Voucher = voucher;
                if (TotalPrice != 0)
                {
                    TotalPrice = DiscountAmount(voucher, TotalPrice);
                    if (voucher.VoucherType.Equals("Percentage"))
                    {
                        VoucherMessage = $"Discount percentage:{voucher.DiscountValue}%";
                    }
                    else if(voucher.VoucherType.Equals("Fixed Amount"))
                    {
                        VoucherMessage = $"Discount amount:{voucher.DiscountValue * 1000}đ";
                    }
                }
            }

        }

        private VoucherForm window;
        private void ExecuteOpenVoucherForm(object parameter)
        {
            window = new VoucherForm();
            window.DataContext = this;
            window.ShowDialog();
        }
        private void ExecuteSeatClickedCommand(object parameter)
        {
            Seat seat = (Seat)parameter;
            if (SelectedSeats.Any(s => s.Row == seat.Row && s.Number == seat.Number))
            {
                SelectedSeats.Remove(seat);
            }
            else
            {
                SelectedSeats.Add(seat);
            }
            if (Voucher != null && Voucher.VoucherType.Equals("Percentage"))
            {
                TotalPrice = DiscountAmount(Voucher, SelectedSeats.Sum(s => s.Price.Value));
            }
            else
            {
                TotalPrice = SelectedSeats.Sum(s => s.Price.Value);
                OriginalPrice = SelectedSeats.Sum(s => s.Price.Value);
            }
            OnPropertyChanged(nameof(SelectedSeats));
        }
        private void ExecuteGetSeatsCommand(object parameter)
        {
            int ShowtimeId = (int)parameter;
            SelectedShowTime = ShowTimes.FirstOrDefault(s => s.ShowtimeId == ShowtimeId);

            SeatOfSelectedShowtime = ShowTimeRepo.GetSeatByShowtimeId(ShowtimeId);
            TotalChairs = SeatOfSelectedShowtime.Count;
            RemainingChairs = SeatOfSelectedShowtime.Where(s => s.Status.Equals("Available")).Count(); 
            if (SeatOfSelectedShowtime.Count > 0)
            {
                IsSelectedShowTime = true;
            }
            else { IsSelectedShowTime = false; }
        }
        private void ExecuteCheckoutCommand(object parameter)
        {
            EnityModel.Booking newBooking = new EnityModel.Booking
            {
                ShowtimeId = SelectedShowTime.ShowtimeId,
                UserId = UserRepository.CurrentUser.Id,
                OriginalPrice = Convert.ToInt32(OriginalPrice),
                TotalPrice = Convert.ToInt32(TotalPrice),
            };

            BookingRepo.CreateBooking(newBooking, SelectedSeats);
            NavigationStore._navigationStore.CurrentViewModel = new UserViewModel();
        }

        private void ExecuteChangeSelectedDay(object parameter)
        {
            IsSelectedShowTime = false;
            SeatOfSelectedShowtime.Clear();
            DateTime datetime = (DateTime)parameter;
            int date = datetime.Day; 
            SelectedDay = date;
            ShowTimesSelected = new ObservableCollection<Showtime>(ShowTimes.Where(showtime => showtime.ShowtimeDatetime.Value.Day.Equals(selectedDay))
                               .Select(showtime => showtime).ToList());
        }

        private void FilterShowTimeByDay()
        {
            DayHaveShowTimes = ShowTimes.GroupBy(showtime => showtime.ShowtimeDatetime.Value.Day) // ->>  need more constrant here 
                              .Select(grp => grp.First())
                               .ToList();
            if(DayHaveShowTimes.Count > 0)
            {
                selectedDay = DayHaveShowTimes.ElementAt(0).ShowtimeDatetime.Value.Day;
                ShowTimesSelected = new ObservableCollection<Showtime>(ShowTimes.Where(showtime => showtime.ShowtimeDatetime.Value.Day == selectedDay)
                                    .Select(showtime => showtime).ToList());
            }
        }

    }
}
