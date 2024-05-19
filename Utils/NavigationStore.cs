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
        public ViewModelBase _prevViewModel;

        public static NavigationStore _navigationStore { get;  set; }

        public event Action CurrentViewModelChanged; 
        public event Action PrevViewModelChanged; 

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { 
                _currentViewModel = value;
                OnCurrentViewModelChanged(); 
            }
        }

        public ViewModelBase PrevViewModel
        {
            get { return _prevViewModel; }
            set
            {
                _prevViewModel = value;
                OnPrevViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        } 
        
        private void OnPrevViewModelChanged()
        {
            PrevViewModelChanged?.Invoke();
        }

    }
}
