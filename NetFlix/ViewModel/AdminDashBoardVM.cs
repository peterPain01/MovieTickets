using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Netflix.Repository;
using NetFlix.CustomControls;
using NetFlix.EnityModel;
using NetFlix.Repository;
using NetFlix.Utils;
using NetFlix.View.Admin;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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
        private ObservableCollection<Movie> _allPaginatedMovies;
        private ObservableCollection<Showtime> _allShowtime;
        private ObservableCollection<Voucher> _allVoucher;
        private ObservableCollection<Cinema> _allCinema;
        private ObservableCollection<NetFlix.EnityModel.Booking> _allBookings;
        private User _currentUser;
        private Movie _currentMovie;
        private Showtime _currentShowtime;
        private Voucher _currentVoucher;

        //Statistic
        private string _selectedrevenuemode;
        private int _selectedRevenueMovie;
        private int _selectedRevenueShowtime;
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> ValueFormatter { get; } = value => value.ToString("C"); // Currency format
        public Func<double, string> DateFormatter { get; } = value => DateTime.FromOADate(value).ToString("dd/MM/yyyy");


        // Movie Detail Infomation 
        private ObservableCollection<Star> _allStarsOfMovie;
        private ObservableCollection<Director> _allDirectorOfMovie;
        private ObservableCollection<Star> _allStars;
        private ObservableCollection<Genre> _allGenres;
        private ObservableCollection<Director> _allDirectors;
        private int? _genreOfMovie;
        private int _selectedMovieToGetStar;
        private Star _currentStar;

        private int _starIdtoUpdate;


        public Star CurrentStar
        {
            get { return _currentStar; }
            set
            {
                _currentStar = value;
                OnPropertyChanged(nameof(CurrentStar));
            }
        }

        public int StarIdtoUpdate
        {
            get { return _starIdtoUpdate; }
            set
            {
                _starIdtoUpdate = value;
                OnPropertyChanged(nameof(StarIdtoUpdate));
            }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentStar));
            }
        }

        public ObservableCollection<Star> AllStarsOfMovie
        {
            get { return _allStarsOfMovie; }
            set
            {
                _allStarsOfMovie = value;
                OnPropertyChanged(nameof(AllStarsOfMovie));
            }
        } 
        
        public ObservableCollection<Cinema> AllCinema
        {
            get { return _allCinema; }
            set
            {
                _allCinema = value;
                OnPropertyChanged(nameof(AllCinema));
            }
        }

        public ObservableCollection<Star> AllStars
        {
            get { return _allStars; }
            set
            {
                _allStars = value;
                OnPropertyChanged(nameof(AllStars));
            }
        } 
        
        public ObservableCollection<Director> AllDirectors
        {
            get { return _allDirectors; }
            set
            {
                _allDirectors = value;
                OnPropertyChanged(nameof(AllDirectors));
            }
        }

        public ObservableCollection<Director> AllDirectorsOfMovie
        {
            get { return _allDirectorOfMovie; }
            set
            {
                _allDirectorOfMovie = value;
                OnPropertyChanged(nameof(AllDirectorsOfMovie));
            }
        }

        public int SelectedMovieToGetStar
        {
            get { return _selectedMovieToGetStar; }
            set
            {
                _selectedMovieToGetStar = value;
                OnPropertyChanged(nameof(SelectedMovieToGetStar));
                GetStarsOfMovie(_selectedMovieToGetStar);
            }
        }

        public Movie CurrentMovie
        {
            get { return _currentMovie; }
            set
            {
                _currentMovie = value;
                OnPropertyChanged(nameof(CurrentMovie));
            }
        }

        public ObservableCollection<Genre> AllGenres
        {
            get { return _allGenres; }
            set { _allGenres = value; OnPropertyChanged(nameof(AllGenres)); }
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

        public string SelectedRevenueMode
        {
            get { return _selectedrevenuemode; }
            set
            {
                _selectedrevenuemode = value.ToString();
                OnPropertyChanged(nameof(SelectedRevenueMode));
            }
        }

        public int SelectedRevenueMovie
        {
            get { return _selectedRevenueMovie; }
            set
            {
                _selectedRevenueMovie = value;
                OnPropertyChanged("SelectedRevenueMovie");
                SelectionRevenueChange(0);
            }
        }

        public int SelectedRevenueShowtime
        {
            get { return _selectedRevenueShowtime; }
            set
            {
                _selectedRevenueShowtime = value;
                OnPropertyChanged("SelectedRevenueShowtime");
                SelectionRevenueChange(1);
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
                OnPropertyChanged(nameof(AllMovies));
            }
        }

        public ObservableCollection<Movie> AllPaginatedMovies
        {
            get { return _allPaginatedMovies; }
            set
            {
                _allPaginatedMovies = value;
                OnPropertyChanged("AllPaginatedMovies");
            }
        }

        public int? GenreOfMovie
        {
            get { return _genreOfMovie; }
            set
            {
                _genreOfMovie = value;
                OnPropertyChanged(nameof(GenreOfMovie));
                GenreSelectionChange();
            }
        }
        public ObservableCollection<NetFlix.EnityModel.Booking> AllBookings
        {
            get { return _allBookings; }
            set
            {
                _allBookings = value;
                OnPropertyChanged(nameof(AllBookings));
            }
        }

        public ObservableCollection<Showtime> AllShowtime
        {
            get { return _allShowtime; }
            set
            {
                _allShowtime = value;
                OnPropertyChanged(nameof(AllShowtime));
            }
        }
        public ObservableCollection<Voucher> AllVoucher
        {
            get { return _allVoucher; }
            set
            {
                _allVoucher = value;
                OnPropertyChanged(nameof(AllVoucher));
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
        public ICommand OpenAddStarForm { get; set; }
        public ICommand OpenAddDirectorForm { get; set; }
        public ICommand AddDirectorCommand { get; set; }
        public ICommand AddStarCommand { get; set; }
        public ICommand StarUpdateCommand { get; set; }
        public ICommand RemoveStarCommand { get; set; }
        public ICommand RemoveDirectorCommand { get; set; }
        public ICommand OpenAddBrandNewStarForm { get; set; }
        public ICommand AddBrandNewStarCommand { get; set; }
        public ICommand OpenEditStarForm { get; set; }
        public ICommand EditStarCommand { get; set; }

        //Movie Detail Information 
        public ICommand RemoveStarToMovieCommand { get; set; }
        public ICommand DeleteDirectorCommand { get; set; }


        private MovieRepository movieRepo;
        private ShowTimeRepo showtimeRepo;
        private VoucherRepository voucherRepo;
        private BookingRepository bookingRepo;
        private SubMovieRepo subMovieRepo;

        // Pagiante
        public ICommand PaginateMovieCommand { get; set; }


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
            RemoveStarToMovieCommand = new ViewModelCommand(ExeRemoveStarToMovieCommand);
            DeleteDirectorCommand = new ViewModelCommand(ExecuteDeleteDirectorCommand);
            OpenAddStarForm = new ViewModelCommand(ExecuteOpenAddStarForm);
            OpenAddDirectorForm = new ViewModelCommand(ExecuteOpenAddDirectorForm);
            AddDirectorCommand = new ViewModelCommand(ExecuteAddDirectorCommand);
            AddStarCommand = new ViewModelCommand(ExecuteAddStarCommand);
            StarUpdateCommand = new ViewModelCommand(StarUpdateSelectionChanged);
            RemoveStarCommand = new ViewModelCommand(ExeRemoveStarCommand);
            RemoveDirectorCommand = new ViewModelCommand(ExeRemoveDirectorCommand);
            OpenAddBrandNewStarForm = new ViewModelCommand(ExeOpenAddBrandNewStarForm);
            AddBrandNewStarCommand = new ViewModelCommand(ExeAddBrandNewStarCommand);
            OpenEditStarForm = new ViewModelCommand(ExeOpenEditStarForm);
            EditStarCommand = new ViewModelCommand(ExeEditStarCommand);

            
            // Initital Variable 
            movieRepo = new MovieRepository();
            showtimeRepo = new ShowTimeRepo();
            voucherRepo = new VoucherRepository();
            bookingRepo = new BookingRepository();
            subMovieRepo = new SubMovieRepo();
            CurrentUser = UserRepository.CurrentUser;

            CurrentMainSection = new View.Admin.AdminDashBoard();
            CurrentMovie = new Movie();
            CurrentShowtime = new Showtime();
            CurrentVoucher = new Voucher();
            CurrentStar = new Star();
            CurrentStar.Movies.Add(movieRepo.GetMovieById(SelectedMovieToGetStar));

            TrendingMovies = movieRepo.GetTrendingMovie();
            AllMovies = movieRepo.GetAllMovies();


            // Paginate
            PaginateMovieCommand = new ViewModelCommand(ExePaginateMovieCommand);
            AllPaginatedMovies = new ObservableCollection<Movie>(AllMovies.Take(6).ToList());


            SeriesCollection = new SeriesCollection();
            InitTotalShowtime();
            InitTotalNowShowing();
        }

        public ToastViewModel _vm = new ToastViewModel();


        private void ExeOpenEditStarForm(object p)
        {
            CurrentStar = (Star)p;
            starForm = new AddNewStar();
            starForm.DataContext = this;
            starForm.CommandAddNewStar = EditStarCommand;
            starForm.Show();
        }

        private void ExeEditStarCommand(object p)
        {
            var star = (Star)p; 
            subMovieRepo.EditStar(CurrentStar);
            _vm.ShowSuccess("Star Successfullyy Updated");
            starForm.Close(); 
        }
        private void ExeAddBrandNewStarCommand(object p)
        {
            var star = subMovieRepo.AddStar(CurrentStar); 
            AllStars.Add(star);
            OnPropertyChanged(nameof(AllStars));
            _vm.ShowSuccess("Star Successfullyy Added");
            starForm.Close(); 
        }
        public void ExeRemoveStarCommand(object p)
        {
            var star = (Star)p;
            subMovieRepo.DeleteStar(star); 
            AllStars.Remove(star);
            OnPropertyChanged(nameof(AllStars));
            _vm.ShowSuccess("Star successfully deleted"); 
        }

        private AddNewStar starForm;
        public void ExeOpenAddBrandNewStarForm(object p)
        {
            starForm = new AddNewStar();
            starForm.DataContext = this;
            starForm.CommandAddNewStar = AddBrandNewStarCommand;
            starForm.Show(); 
        }
        public void ExeRemoveDirectorCommand(object p)
        {
            var dirrector = (Director)p;
            subMovieRepo.DeleteDirector(dirrector);
            AllDirectors.Remove(dirrector);
            OnPropertyChanged(nameof(AllDirectors));
            _vm.ShowSuccess("Director successfully deleted");
        }
        public void GenreSelectionChange()
        {
            // update genre 
            if (GenreOfMovie == null || GenreOfMovie == CurrentMovie.GenreId) return;

            subMovieRepo.UpdateGenreForMovie(CurrentMovie.MovieId, GenreOfMovie.Value);
            _vm.ShowSuccess("Genre successfully updated");
        }
        public void ExePaginateMovieCommand(object p)
        {
            int page_size = 6; // move it to other file
            int page = (int)p;
            int from = (page - 1) * page_size;
            AllPaginatedMovies = new ObservableCollection<Movie>(AllMovies.Skip(from).Take(page_size).ToList());
        }

        public void GetStarsOfMovie(int movieId)
        {
            AllStarsOfMovie = subMovieRepo.GetAllStarMovie(movieId);
            //AllDirectorsOfMovie = subMovieRepo.GetAllDirectorMovie(movieId);
            CurrentMovie = subMovieRepo.GetMovie(movieId);
            AllGenres = subMovieRepo.GetAllGenres();
            GenreOfMovie = CurrentMovie?.Genre?.GenreId;
        }

        private void ExecuteAddDirectorCommand(object p)
        {

        }

        private void StarUpdateSelectionChanged(object p)
        {
            var star = subMovieRepo.AddNewStarToMovie(CurrentMovie.MovieId, StarIdtoUpdate);
            if (star != null)
            {
                AllStarsOfMovie.Add(star);
                OnPropertyChanged(nameof(AllStarsOfMovie));
            }
            _vm.ShowSuccess("Star successfully added to Movie");
        }


        private void ExecuteAddStarCommand(object p)
        {
            subMovieRepo.AddStarToMovie(SelectedMovieToGetStar, CurrentStar);
            AllStarsOfMovie.Add(CurrentStar);
            OnPropertyChanged(nameof(AllStarsOfMovie));
            _vm.ShowSuccess("Star successfully added");
        }
        public void ExecuteOpenAddStarForm(object parameter)
        {
            var form = new MovieDetailForm();
            form.DataContext = this;
            form.FormHeadingMovieDetail = "Add Star Form";
            form.CommandMovieDetail = StarUpdateCommand;
            form.Show();
        }

        public void ExecuteOpenAddDirectorForm(object parameter)
        {
            var form = new MovieDetailForm();
            form.DataContext = this;
            form.FormHeadingMovieDetail = "Add Director Form";
            form.CommandMovieDetail = AddDirectorCommand;
            form.Show();
            return;
        }
        private void ExecuteDeleteDirectorCommand(object parameter)
        {
            var director = (Director)parameter;
            subMovieRepo.DeleteDirectorFromMovie(SelectedMovieToGetStar, director.DirectorId);
            AllDirectorsOfMovie.Remove(director);
            OnPropertyChanged(nameof(AllDirectorsOfMovie));
            _vm.ShowSuccess("Director Successfully Deleted");
        }
        private void ExeRemoveStarToMovieCommand(object paerameter)
        {
            var star = (Star)paerameter;
            subMovieRepo.DeleteStarFromMovie(SelectedMovieToGetStar, star.StarId);
            AllStarsOfMovie.Remove(AllStarsOfMovie.Where(s => s.StarId == star.StarId).Single());
            OnPropertyChanged(nameof(AllStarsOfMovie)); 
            _vm.ShowSuccess("Star Successfully deleted");
        }

        // 0 - movie 1 - showtime
        private void SelectionRevenueChange(int mode)
        {
            if (mode == 0)
            {
                if (SelectedRevenueMovie != null)
                {
                    AllBookings = bookingRepo.GetBookingByMovieId(SelectedRevenueMovie);
                    LoadData();
                }
            }
            else if (mode == 1)
            {
                if (SelectedRevenueShowtime != null)
                {
                    AllBookings = bookingRepo.GetBookingByShowtimeId(SelectedRevenueShowtime);
                    LoadData();
                }
            }

        }
        private void LoadData()
        {
            var lineSeries = new LineSeries
            {
                Title = "Total Price",
                Values = new ChartValues<ObservablePoint>()
            };

            foreach (NetFlix.EnityModel.Booking booking in AllBookings)
            {
                if (booking.BookingDatetime != null && booking.TotalPrice != null)
                    lineSeries.Values.Add(new ObservablePoint(booking.BookingDatetime.Value.ToOADate(), (double)booking.OriginalPrice));
            }
            SeriesCollection.Clear();
            SeriesCollection.Add(lineSeries);
        }

        private void ExecuteUpdateVoucherCommand(object parameter)
        {
            voucherRepo.UpdateVoucher(CurrentVoucher);
            _vm.ShowSuccess("Voucher successfully updated");
            vForm.Close(); 
        }
        private void ExecuteOpenEditVoucherForm(object parameter)
        {
            CurrentVoucher = (Voucher)parameter;
            vForm = new VoucherForm();
            vForm.DataContext = this;
            vForm.FormHeadingVoucher = "Edit Voucher Form";
            vForm.CommandVoucher = UpdateVoucherCommand;
            vForm.Show();
        }
        private void ExecuteDeleteVoucherCommand(object parameter)
        {
            var voucher = (Voucher)parameter;
            voucherRepo.DeletetVoucher(voucher);
            AllVoucher.Remove(AllVoucher.FirstOrDefault(v => v.VoucherId ==voucher.VoucherId));
            OnPropertyChanged(nameof(AllVoucher));
            _vm.ShowSuccess("Voucher successfully deleteted");
        }
        private void ExecuteCreateNewVoucherCommand(object parameter)
        {
            voucherRepo.AddVoucher(CurrentVoucher);
            AllVoucher.Add(CurrentVoucher);
            OnPropertyChanged(nameof(AllVoucher));
            _vm.ShowSuccess("Voucher successfully added");
            vForm.Close(); 
        }

        public VoucherForm vForm = new VoucherForm(); 
        private void ExecuteOpenAddVoucherForm(object parameter)
        {
            vForm = new VoucherForm();
            CurrentVoucher = new Voucher();
            vForm.DataContext = this;
            vForm.FormHeadingVoucher = "Add Voucher Form";
            vForm.CommandVoucher = CreateNewVoucherCommand;
            vForm.Show();
        }
        private void ExecuteEditShowtimeCommand(object parameter)
        {
            var b = CurrentShowtime; 
            showtimeRepo.UpdateShowtime(CurrentShowtime);
            var showtime = AllShowtime.FirstOrDefault(st => st.ShowtimeId == CurrentShowtime.ShowtimeId); 
            if(showtime != null)
            {
                AllShowtime.Remove(showtime);
                OnPropertyChanged(nameof(AllShowtime));
                CurrentShowtime.Movie = movieRepo.GetMovieById(CurrentShowtime.MovieId.Value);
                AllShowtime.Add(CurrentShowtime);
                OnPropertyChanged(nameof(AllShowtime)); 
            }
            _vm.ShowSuccess("Showtime successfully updated");
            stForm.Close(); 
        }

        public ShowtimeForm stForm; 
        private void ExecuteOpenEditShowtimeForm(object parameter)
        {
            CurrentShowtime = (Showtime)parameter;
            stForm = new ShowtimeForm();
            stForm.DataContext = this;
            stForm.FormHeadingShowtime = "Edit Showtime Form";
            stForm.CommandShowtime = UpdateShowtimeCommand;
            stForm.Show();
        }
        private void ExecuteCreateNewShowtimeCommand(object parameter)
        {
            showtimeRepo.InsertShowtime(CurrentShowtime);
            CurrentShowtime.Movie = movieRepo.GetMovieById(CurrentShowtime.MovieId.Value); 
            AllShowtime.Add(CurrentShowtime);
            OnPropertyChanged(nameof(AllShowtime));
            _vm.ShowSuccess("Showtime successfully added");
            if(stForm != null) stForm.Close();
        }
        private void ExecuteOpenAddShowTimeForm(object parameter)
        {
            stForm = new ShowtimeForm();
            CurrentShowtime = new Showtime(); 
            stForm.DataContext = this;
            stForm.FormHeadingShowtime = "Add Showtime Form";
            stForm.CommandShowtime = CreateNewShowtimeCommand;
            stForm.Show();
        }
        private void ExecuteDeleteShowtimeCommand(object parameter)
        {
            var showtime = (Showtime)parameter;
            showtimeRepo.DeleteShowtime(showtime.ShowtimeId);
            AllShowtime.Remove(AllShowtime.FirstOrDefault(st => st.ShowtimeId == showtime.ShowtimeId));
            OnPropertyChanged(nameof(AllShowtime));
            _vm.ShowSuccess("Showtime Successflly Deleted");
        }

        private void ExecuteEditMovieCommand(object parameter)
        {
            if (!CurrentMovie.PosterUrl.StartsWith("/Images/slider-"))
            {
                CurrentMovie.PosterUrl = helper.CreateImagePath(CurrentMovie.PosterUrl, 0);
            }
            if (!CurrentMovie.PosterVerticalUrl.StartsWith("/Images/mv-"))
            {
                CurrentMovie.PosterVerticalUrl = helper.CreateImagePath(CurrentMovie.PosterVerticalUrl, 1);
            }
            movieRepo.UpdateMovie(CurrentMovie);
            editMovieForm.Close();
            _vm.ShowSuccess("Movie Successflly Edited");
        }

        MovieForm editMovieForm = new MovieForm();

        private void ExecuteOpenEditMovieForm(object parameter)
        {
            editMovieForm = new MovieForm(); 
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
            AllMovies.Remove(AllMovies.FirstOrDefault(mv => mv.MovieId == movies.MovieId));
            AllPaginatedMovies.Remove(AllPaginatedMovies.FirstOrDefault(mv => mv.MovieId == movies.MovieId));
            OnPropertyChanged(nameof(AllMovies));
            OnPropertyChanged(nameof(AllPaginatedMovies));
            _vm.ShowSuccess("Movie successfully deleted");
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
                    AllCinema = showtimeRepo.GetAllCinema(); 
                    AllShowtime = showtimeRepo.GetAllShowTime();
                    CurrentMainSection = new View.Admin.AdminShowtime();
                    break;
                case "Voucher":
                    AllVoucher = voucherRepo.GetAllVoucher();
                    CurrentMainSection = new View.Admin.AdminVoucher();
                    break;
                case "Statistic":
                    AllBookings = bookingRepo.GetAllBooking();
                    LoadData();
                    CurrentMainSection = new View.Admin.AdminStatistic();
                    break;
                case "MovieDetails":
                    CurrentMainSection = new View.Admin.AdminMovieDetail();
                    AllStars = subMovieRepo.GetAllStar();
                    break;
                case "ManageStarDir":
                    CurrentMainSection = new View.Admin.ManageStarDir();
                    AllStars = subMovieRepo.GetAllStar();
                    AllDirectors = subMovieRepo.GetAllDirector();
                    CurrentStar = new Star(); 
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
                CurrentMovie = new Movie(); 
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
