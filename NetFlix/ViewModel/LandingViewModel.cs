using Netflix.Utils;
using NetFlix.Model;
using NetFlix.Repository;
using NetFlix.View;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace NetFlix.ViewModel
{
    public class LandingViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _carouselItems;
        private bool _isHovering = false;
        private DispatcherTimer _timer;
        private int _currentIndex = 0;
        
        public class Movie
        {
            private string _name;
            private string _poster_url;
            private bool is_Selected;
            public Movie(string name, string poster_url)
            {
                _name = name;
                _poster_url = poster_url;
            }
            public bool Is_Selected { get => is_Selected; set => is_Selected = value; }
            public string Name { get => _name; set => _name = value; }
            public string Poster_url { get => _poster_url; set => _poster_url = value; }
        }
        public ObservableCollection<Movie> Items
        {
            get { return _carouselItems; }
            set
            {
                _carouselItems = value;
                OnPropertyChanged("Items");
            }
        }

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                OnPropertyChanged("CurrentIndex");
            }
        }

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand GridLeaveCommand { get; set; }
        public ICommand GridEnterCommand { get; set; }

        public ICommand NavigateToMoviePage { get; set; }


        private NavigationStore _navigationStore; 
        public LandingViewModel(NavigationStore navigationStore)
        {
            _carouselItems = new ObservableCollection<Movie>
            {
                new Movie("Movie 1","/Images/slider-kungfu.jpg"),
                new Movie("Movie 2","/Images/mv-mai.jpg"),
                new Movie("Movie 3","/Images/mv-dune.jpg"),
                new Movie("Movie 4","/Images/mv-demonslayer.jpg"),
                new Movie("Movie 5", "/Images/mv-madamweb.jpg"),
                new Movie("Movie 1","/Images/slider-kungfu.jpg"),
                new Movie("Movie 1","/Images/slider-kungfu.jpg"),
                new Movie("Movie 1","/Images/slider-kungfu.jpg"),
                new Movie("Movie 1","/Images/slider-kungfu.jpg")
            };

            NextCommand = new ViewModelCommand(NextButton_Click);
            PreviousCommand = new ViewModelCommand(PreviousButton_Click);

            GridLeaveCommand = new ViewModelCommand(Grid_MouseLeave);
            GridEnterCommand = new ViewModelCommand(Grid_MouseEnter);

            this._navigationStore = navigationStore; 
            NavigateToMoviePage = new ViewModelCommand(ExecuteNavigatetoMoviePage); 

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Tick += Timer_Tick;

        }
            
        private void ExecuteNavigatetoMoviePage(object parameter)
        {
            _navigationStore.CurrentViewModel = new MovieViewModel(_navigationStore, "OpenHeimer"); 
        }
        private void NextButton_Click(object obj)
        {

            if (_currentIndex > Items.Count - 2)
            {
                return;
            }
            _currentIndex++;

            ItemsControl itemControl = obj as ItemsControl;
            Canvas.SetLeft(itemControl, -400 * _currentIndex);

            var animation = new DoubleAnimation
            {
                To = -400 * _currentIndex, // Adjust this value based on the desired position
                Duration = TimeSpan.FromSeconds(1)
            };

            Storyboard.SetTarget(animation, itemControl);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        private void PreviousButton_Click(object obj)
        {
            if (_currentIndex < 1)
                return;
            _currentIndex--;
            ItemsControl itemControl = obj as ItemsControl;

            Canvas.SetRight(itemControl, -400 * _currentIndex);

            var animation = new DoubleAnimation
            {
                To = -400 * _currentIndex, // Adjust this value based on the desired position
                Duration = TimeSpan.FromSeconds(1)
            };

            Storyboard.SetTarget(animation, itemControl);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_isHovering)
            {
                grid.Children[0].Visibility = Visibility.Hidden;   // Image
                grid.Children[1].Visibility = Visibility.Visible;
                MediaElement mediaElement = (MediaElement)grid.Children[1];
                // Hide the video
                if (mediaElement != null)
                {
                    mediaElement.Play();
                }
                // Stop the timer
                _timer.Stop();
            }
        }

        private Grid grid; 
        
        private void Grid_MouseEnter(object obj)
        {
            var _myGrid = obj as Grid;

            Debug.WriteLine("Hello World"); 
            Debug.WriteLine(_myGrid); 

            this.grid = _myGrid; 
            _isHovering = true;
            _timer.Start();
            Grid grid2 = (Grid)_myGrid.Children[0];
            grid2.Children[1].Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object obj)
        {
            _isHovering = false;
            _timer.Stop();
            var _myGrid = obj as Grid;

            // Get the Grid containing Image and MediaElement

            // Hide the video
            _myGrid.Children[0].Visibility = Visibility.Visible;   // Image
            _myGrid.Children[1].Visibility = Visibility.Collapsed; // MediaElement

            // Pause the video
            MediaElement mediaElement = (MediaElement)_myGrid.Children[1];
            Grid grid2 = (Grid)_myGrid.Children[0];
            grid2.Children[1].Visibility = Visibility.Hidden;
            // Pause the video when mouse leaves
            if (mediaElement != null)
            {
                mediaElement.Pause();
                mediaElement.Position = TimeSpan.Zero;
            }
        }
    }
}
