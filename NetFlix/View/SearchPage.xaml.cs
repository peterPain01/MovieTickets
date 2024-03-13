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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetFlix.View
{
    public partial class SearchPage : UserControl
    {
        public class Movie
        {
            private string _title; 
            private string _poster_url;
            public string Title { get => _title; set => _title = value; }
            public string Poster_url { get => _poster_url; set => _poster_url = value; }
            public Movie(string title, string poster_url)
            {
                Title = title;
                _poster_url = poster_url;
            }
        }

        public ObservableCollection<Movie> Movies = new ObservableCollection<Movie>(); 
        public SearchPage()
        {
            InitializeComponent();
        }
    }
}
