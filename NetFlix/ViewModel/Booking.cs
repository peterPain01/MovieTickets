using Netflix.Repository;
using NetFlix.EnityModel;
using NetFlix.Model;
using NetFlix.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.ViewModel
{
    public class Booking : ViewModelBase
    {
        public Movie Movie { get; set; }
        private ObservableCollection<Showtime> showtimes;
        private Showtime selectedShowTimes { get; set; }

        private MovieRepository MovieRepository { get; }
        private ShowTimeRepo ShowTimeRepo { get; }

        public ObservableCollection<Showtime> ShowTimes
        {
            get { return showtimes; }
            set
            {
                showtimes = value;
                OnPropertyChanged("ShowTimes");
            }
        }
        
        public Booking(int id) {
            MovieRepository = new MovieRepository();
            ShowTimeRepo = new ShowTimeRepo(); 
            Movie = MovieRepository.GetMovieById(id);
            ShowTimes = ShowTimeRepo.GetShowTimeByMovieId(id);
            selectedShowTimes = ShowTimes.ElementAt(0);
        }
    }
}
