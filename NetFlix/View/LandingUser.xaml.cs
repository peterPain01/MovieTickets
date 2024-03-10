using Netflix.ViewModel;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace NetFlix.View
{
    public partial class LandingUser : Window
    {
        //private string[] imagePaths;
        //private Grid grid; 
        //private DispatcherTimer timer;
        //private bool isHovering = false;


        //private int currentIndex = 0;
        //public class Movie
        //{
        //    private string _name; 
        //    private string _poster_url;
        //    private bool is_Selected ; 
        //    public Movie(string name, string poster_url)
        //    {
        //        _name = name;
        //        _poster_url = poster_url;
        //    }
        //    public bool Is_Selected { get => is_Selected; set => is_Selected = value; }
        //    public string Name { get => _name; set => _name = value; }
        //    public string Poster_url { get => _poster_url; set => _poster_url = value; }
        //}

        //// create List change real time Items
        //public ObservableCollection<Movie> Items { get; } = new ObservableCollection<Movie> {
        //new Movie("Movie 1","/Images/slider-kungfu.jpg"), 
        //new Movie("Movie 2","/Images/mv-mai.jpg"), 
        //new Movie("Movie 3","/Images/mv-dune.jpg"), 
        //new Movie("Movie 4","/Images/mv-demonslayer.jpg"), 
        //new Movie("Movie 5", "/Images/mv-madamweb.jpg"),
        //new Movie("Movie 1","/Images/slider-kungfu.jpg"),
        //new Movie("Movie 1","/Images/slider-kungfu.jpg"),
        //new Movie("Movie 1","/Images/slider-kungfu.jpg"),
        //new Movie("Movie 1","/Images/slider-kungfu.jpg"),
        //};

        public LandingUser()
    {
        InitializeComponent();
        //DataContext = this;
        //timer = new DispatcherTimer();
        //timer.Interval = TimeSpan.FromSeconds(2);
        //timer.Tick += Timer_Tick;
    }

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    // Start playing the video after the delay
        //    if (isHovering)
        //    {
        //        // Get the MediaElement instance
        //        grid.Children[0].Visibility = Visibility.Hidden;   // Image
        //        grid.Children[1].Visibility = Visibility.Visible;
        //        MediaElement mediaElement = (MediaElement)grid.Children[1];
        //        // Hide the video
        //        if (mediaElement != null)
        //        {
        //            mediaElement.Play();
        //        }
        //        // Stop the timer
        //        timer.Stop();
        //    }
        //}

        //private void SelectFirstItem()
        //{
        //    foreach (var item in this.Items)
        //    {
        //        item.Is_Selected = item == Items[0];
        //    }
        //}
        //private void NextButton_Click(object sender, RoutedEventArgs e)
        //{

        //    if (currentIndex > Items.Count - 2)
        //    {
        //        return;
        //    }
        //    currentIndex++;
        //    Canvas.SetLeft(ItemsControl, -400 * currentIndex);
        //    var animation = new DoubleAnimation
        //    {
        //        To = -400 * currentIndex, // Adjust this value based on the desired position
        //        Duration = TimeSpan.FromSeconds(1)
        //    };

        //    Storyboard.SetTarget(animation, ItemsControl);
        //    Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

        //    var storyboard = new Storyboard();
        //    storyboard.Children.Add(animation);
        //    storyboard.Begin();
        //}

        //private void PreviousButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (currentIndex < 1)
        //        return;
        //    currentIndex--;
        //    Canvas.SetRight(ItemsControl, -400 * currentIndex);

        //    var animation = new DoubleAnimation
        //    {
        //        To = -400 * currentIndex, // Adjust this value based on the desired position
        //        Duration = TimeSpan.FromSeconds(1)
        //    };

        //    Storyboard.SetTarget(animation, ItemsControl);
        //    Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

        //    var storyboard = new Storyboard();
        //    storyboard.Children.Add(animation);
        //    storyboard.Begin();

        //}


        //private void Grid_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    isHovering = true;
        //    timer.Start();
        //    this.grid = (Grid)sender;
        //    Grid grid2 = (Grid)grid.Children[0];
        //    grid2.Children[1].Visibility = Visibility.Visible;
        //}

        //private void Grid_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    isHovering = false;
        //    timer.Stop();

        //    // Get the Grid containing Image and MediaElement
        //    Grid grid = (Grid)sender;

        //    // Hide the video
        //    grid.Children[0].Visibility = Visibility.Visible;   // Image
        //    grid.Children[1].Visibility = Visibility.Collapsed; // MediaElement

        //    // Pause the video
        //    MediaElement mediaElement = (MediaElement)grid.Children[1];
        //    Grid grid2 = (Grid)grid.Children[0];
        //    grid2.Children[1].Visibility = Visibility.Hidden;
        //    // Pause the video when mouse leaves
        //    if (mediaElement != null)
        //    {
        //        mediaElement.Pause();
        //        mediaElement.Position = TimeSpan.Zero;
        //    }
        //}

        //private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    // Get the MediaElement instance
        //    MediaElement mediaElement = (MediaElement)sender;

        //    // Get the parent Grid
        //    Grid grid = (Grid)mediaElement.Parent;

        //    // Hide the video when it ends
        //    grid.Children[0].Visibility = Visibility.Visible;   // Image
        //    grid.Children[1].Visibility = Visibility.Collapsed; // MediaElement
        //}




        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MediaElement_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        
    }

}
