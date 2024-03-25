using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Netflix.Repository;
using NetFlix.CustomControls;
using NetFlix.EnityModel;
using NetFlix.Repository;
using NetFlix.Utils;
using NetFlix.View.Admin;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using UserControl = System.Windows.Controls.UserControl;

namespace NetFlix.ViewModel
{
    public class AdminDashBoardVM : ViewModelBase
    {
        // Properties
        private int _totalShowtime;
        private int _totalNowShowing;
        private string _selectedShowtimeFilter = "today";
        private UserControl _currentMainSection;
        private ObservableCollection<Movie> _allMovies;
        private ObservableCollection<Showtime> _allShowtime;
        private ObservableCollection<Voucher> _allVoucher;
        private ObservableCollection<NetFlix.EnityModel.Booking> _allBookings;

        private Movie _currentMovie;
        private Showtime _currentShowtime;
        private Voucher _currentVoucher;

        //Statistic
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> ValueFormatter { get; } = value => value.ToString("C"); // Currency format
        public Func<double, string> DateFormatter { get; } = value => DateTime.FromOADate(value).ToString("dd/MM/yyyy");


        public Movie CurrentMovie
        {
            get { return _currentMovie; }
            set
            {
                _currentMovie = value;
                OnPropertyChanged(nameof(CurrentMovie));
            }
        }

        public Voucher CurrentVoucher
        {
            get { return _currentVoucher; }
            set
            {
                _currentVoucher = value;
                OnPropertyChanged(nameof(CurrentMovie));
            }
        }
        public Showtime CurrentShowtime
        {
            get { return _currentShowtime; }
            set
            {
                _currentShowtime = value;
                OnPropertyChanged(nameof(CurrentShowtime));
            }
        }

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

        public ObservableCollection<Movie> AllMovies
        {
            get { return _allMovies; }
            set
            {
                _allMovies = value;
                OnPropertyChanged("AllMovies");
            }
        }
        public ObservableCollection<NetFlix.EnityModel.Booking> AllBookings
        {
            get { return _allBookings; }
            set
            {
                _allBookings = value;
                OnPropertyChanged("AllBookings");
            }
        }

        public ObservableCollection<Showtime> AllShowtime
        {
            get { return _allShowtime; }
            set
            {
                _allShowtime = value;
                OnPropertyChanged("AllShowtime");
            }
        }
        public ObservableCollection<Voucher> AllVoucher
        {
            get { return _allVoucher; }
            set
            {
                _allVoucher = value;
                OnPropertyChanged("AllVoucher");
            }
        }
        public ICommand GetTotalShowTime { get; set; }
        public ICommand NavigateToFeature { get; set; }
        public ICommand OpenAddMovieForm { get; set; }
        public ICommand OpenAddShowTimeForm { get; set; }
        public ICommand OpenEditMovieForm { get; set; }
        public ICommand OpenEditShowtimeForm { get; set; }
        public ICommand CreateNewMovieCommand { get; set; }
        public ICommand CreateNewShowtimeCommand { get; set; }
        public ICommand DeleteMovieCommand { get; set; }
        public ICommand DeleteShowtimeCommand { get; set; }
        public ICommand EditMovieCommand { get; set; }
        public ICommand UpdateShowtimeCommand { get; set; }
        public ICommand OpenAddVoucherForm { get; set; }
        public ICommand CreateNewVoucherCommand { get; set; }
        public ICommand DeleteVoucherCommand { get; set; }
        public ICommand OpenEditVoucherForm { get; set; }
        public ICommand UpdateVoucherCommand { get; set; }
        private MovieRepository movieRepo;
        private ShowTimeRepo showtimeRepo;
        private VoucherRepository voucherRepo;
        private BookingRepository bookingRepo;
        public AdminDashBoardVM()
        {
            // Initital ICommand 
            GetTotalShowTime = new ViewModelCommand(ExecutetGetTotalShowTime);
            NavigateToFeature = new ViewModelCommand(ExecuteNavigateToFeature);
            OpenAddMovieForm = new ViewModelCommand(ExecuteOpenAddMovieForm);
            OpenAddShowTimeForm = new ViewModelCommand(ExecuteOpenAddShowTimeForm);
            CreateNewMovieCommand = new ViewModelCommand(ExecuteCreateNewMovieCommand);
            CreateNewShowtimeCommand = new ViewModelCommand(ExecuteCreateNewShowtimeCommand);
            DeleteMovieCommand = new ViewModelCommand(ExecuteDeleteMovieCommand);
            DeleteShowtimeCommand = new ViewModelCommand(ExecuteDeleteShowtimeCommand);
            OpenEditMovieForm = new ViewModelCommand(ExecuteOpenEditMovieForm);
            OpenEditShowtimeForm = new ViewModelCommand(ExecuteOpenEditShowtimeForm);
            EditMovieCommand = new ViewModelCommand(ExecuteEditMovieCommand);
            UpdateShowtimeCommand = new ViewModelCommand(ExecuteEditShowtimeCommand);
            OpenAddVoucherForm = new ViewModelCommand(ExecuteOpenAddVoucherForm);
            CreateNewVoucherCommand = new ViewModelCommand(ExecuteCreateNewVoucherCommand);
            DeleteVoucherCommand = new ViewModelCommand(ExecuteDeleteVoucherCommand);
            OpenEditVoucherForm = new ViewModelCommand(ExecuteOpenEditVoucherForm);
            UpdateVoucherCommand = new ViewModelCommand(ExecuteUpdateVoucherCommand);
            // Initital Variable 
            movieRepo = new MovieRepository();
            showtimeRepo = new ShowTimeRepo();
            voucherRepo = new VoucherRepository();
            bookingRepo = new BookingRepository();
            SeriesCollection = new SeriesCollection();

            CurrentMainSection = new View.Admin.AdminDashBoard();
            CurrentMovie = new Movie();
            CurrentShowtime = new Showtime();
            CurrentVoucher = new Voucher();

            TrendingMovies = movieRepo.GetTrendingMovie();
            AllMovies = movieRepo.GetAllMovies();
            AllShowtime = showtimeRepo.GetAllShowTime();
            AllVoucher = voucherRepo.GetAllVoucher();
            AllBookings = bookingRepo.GetAllBooking();
            LoadData();


            InitTotalShowtime();
            InitTotalNowShowing();
        }

        public ToastViewModel _vm = new ToastViewModel();
        private void LoadData()
        {
            var lineSeries = new LineSeries
            {
                Title = "Total Price",
                Values = new ChartValues<ObservablePoint>()
            };

            foreach (NetFlix.EnityModel.Booking booking in AllBookings)
            {
                lineSeries.Values.Add(new ObservablePoint(booking.BookingDatetime.Value.ToOADate(), (double)booking.OriginalPrice));
            }

            SeriesCollection.Add(lineSeries);
        }

        private void ExecuteUpdateVoucherCommand(object parameter)
        {
            voucherRepo.UpdateVoucher(CurrentVoucher);
            _vm.ShowSuccess("Voucher successfully updated");
        }
        private void ExecuteOpenEditVoucherForm(object parameter)
        {
            CurrentVoucher = (Voucher)parameter;
            var form = new VoucherForm();
            form.DataContext = this;
            form.FormHeadingVoucher = "Edit Voucher Form";
            form.CommandVoucher = UpdateVoucherCommand;
            form.Show();
        }
        private void ExecuteDeleteVoucherCommand(object parameter)
        {
            var voucher = (Voucher)parameter;
            voucherRepo.DeletetVoucher(voucher);
            AllVoucher.Remove(voucher);
            OnPropertyChanged(nameof(Voucher));
        }
        private void ExecuteCreateNewVoucherCommand(object parameter)
        {
            MessageBox.Show("Hello World");
            voucherRepo.AddVoucher(CurrentVoucher);
            AllVoucher.Add(CurrentVoucher);
            OnPropertyChanged(nameof(AllVoucher));
            _vm.ShowSuccess("Voucher successfully added");
        }

        private void ExecuteOpenAddVoucherForm(object parameter)
        {
            var form = new VoucherForm();
            form.DataContext = this;
            form.FormHeadingVoucher = "Add Voucher Form";
            form.CommandVoucher = CreateNewVoucherCommand;
            form.Show();
        }
        private void ExecuteEditShowtimeCommand(object parameter)
        {
            showtimeRepo.UpdateShowtime(CurrentShowtime);
            _vm.ShowSuccess("Showtime successfully updated");
        }
        private void ExecuteOpenEditShowtimeForm(object parameter)
        {
            CurrentShowtime = (Showtime)parameter;
            var form = new ShowtimeForm();
            form.DataContext = this;
            form.FormHeadingShowtime = "Edit Showtime Form";
            form.CommandShowtime = UpdateShowtimeCommand;
            form.Show();
        }
        private void ExecuteCreateNewShowtimeCommand(object parameter)
        {
            showtimeRepo.InsertShowtime(CurrentShowtime);
            AllShowtime.Add(CurrentShowtime);
            OnPropertyChanged(nameof(AllShowtime));
            _vm.ShowSuccess("Showtime successfully added");
        }
        private void ExecuteOpenAddShowTimeForm(object parameter)
        {
            var form = new ShowtimeForm();
            form.DataContext = this;
            form.FormHeadingShowtime = "Add Showtime Form";
            form.CommandShowtime = CreateNewShowtimeCommand;
            form.Show();
        }
        private void ExecuteDeleteShowtimeCommand(object parameter)
        {
            var showtime = (Showtime)parameter;
            showtimeRepo.DeleteShowtime(showtime.ShowtimeId);
            AllShowtime.Remove(showtime);
            OnPropertyChanged(nameof(AllShowtime));
            _vm.ShowSuccess("Showtime Successflly Deleted");
        }

        private void ExecuteEditMovieCommand(object parameter)
        {
            if (!CurrentMovie.PosterUrl.StartsWith(@"C:\\Learning\\School\\CURRENT\\Window\\Project\\NetFlix\\NetFlix\\Images\\slider-\"))
            {
                CurrentMovie.PosterUrl = helper.CreateImagePath(CurrentMovie.PosterUrl, 0);
            }
            if (!CurrentMovie.PosterVerticalUrl.StartsWith(@"C:\\Learning\\School\\CURRENT\\Window\\Project\\NetFlix\\NetFlix\\Images\\mv-\"))
            {
                CurrentMovie.PosterUrl = helper.CreateImagePath(CurrentMovie.PosterUrl, 1);
            }

            movieRepo.UpdateMovie(CurrentMovie);
            editMovieForm.Close();
            _vm.ShowSuccess("Movie Successflly Edited");
        }

        MovieForm editMovieForm = new MovieForm();
        private void ExecuteOpenEditMovieForm(object parameter)
        {
            var movie = (Movie)parameter;
            CurrentMovie = movie;
            editMovieForm.DataContext = this;
            editMovieForm.Command = EditMovieCommand;
            editMovieForm.FormHeading = "Edit Movie Form";
            editMovieForm.Show();
        }


        private void ExecuteDeleteMovieCommand(object parameter)
        {
            var movies = (Movie)parameter;
            movieRepo.DeleteMovie(movies);
            AllMovies.Remove(movies);
            _vm.ShowSuccess("Movie successfully deleted");
            OnPropertyChanged(nameof(AllMovies));

        }
        private void ExecuteCreateNewMovieCommand(object parameter)
        {
            if (CurrentMovie.PosterUrl != null)
            {
                CurrentMovie.PosterUrl = helper.CreateImagePath(CurrentMovie.PosterUrl, 0);
            }
            if (CurrentMovie.PosterVerticalUrl != null)
            {
                CurrentMovie.PosterVerticalUrl = helper.CreateImagePath(CurrentMovie.PosterVerticalUrl, 1);
            }
            movieRepo.CreateMovie(CurrentMovie);
            AllMovies.Add(CurrentMovie);
            OnPropertyChanged(nameof(AllMovies));
            _vm.ShowSuccess("Movie successfully added");
            form.Close();
            form = null;
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
                case "Showtime":
                    CurrentMainSection = new View.Admin.AdminShowtime();
                    break;
                case "Voucher":
                    CurrentMainSection = new View.Admin.AdminVoucher();
                    break;
                case "Statistic":
                    CurrentMainSection = new View.Admin.AdminStatistic();
                    break;
            }
        }

        public MovieForm? form;
        private void ExecuteOpenAddMovieForm(object parameter)
        {
            if (form != null)
            {
                _vm.ShowWarning("You currently open form", new ToastNotifications.Core.MessageOptions()
                {
                    FontSize = 22
                });
            }
            else
            {
                form = new MovieForm();
                form.DataContext = this;
                form.Command = CreateNewMovieCommand;
                form.FormHeading = "Add Movie Form";
                form.Show();
            }
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
