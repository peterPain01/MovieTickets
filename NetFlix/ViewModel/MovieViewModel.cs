using Netflix.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.ViewModel
{
    public class MovieViewModel : ViewModelBase
    {
        // Navigation 
        private NavigationStore _navigationStore; 
        private string title; 

        public string Title
        {
            get { return title; } set { title = value; OnPropertyChanged(nameof(Title)); }
        }

        public MovieViewModel(NavigationStore navigationStore, string title)
        {
            this._navigationStore = navigationStore; 
            Title = title;
        }
    }
}
