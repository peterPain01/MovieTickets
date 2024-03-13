using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Utils
{
    public class NavigationStore
    {
        public ViewModelBase _currentViewModel;

        public static NavigationStore _navigationStore { get;  set; }
        public event Action CurrentViewModelChanged; 
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { 
                _currentViewModel = value;
                OnCurrentViewModelChanged(); 
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
