using Netflix.Utils;
using NetFlix.EnityModel;
using NetFlix.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetFlix.ViewModel
{
    public class StarModel : ViewModelBase
    {
        private Star _currentStar; 

        public Star CurrentStar
        {
            get => _currentStar; 
            set
            {
                _currentStar = value;
                OnPropertyChanged(nameof(CurrentStar));
            }
        }

        public ICommand BackToPrev { get; set; }

        public StarModel(int StarId, SubMovieRepo subMovieRepo)
        {
            BackToPrev = new ViewModelCommand(ExeBackToPrev); 
            CurrentStar = subMovieRepo.GetStar(StarId); 
        }

        private void ExeBackToPrev(object p)
        {
            NavigationStore._navigationStore.CurrentViewModel = NavigationStore._navigationStore.PrevViewModel; 
        }
    }
}
