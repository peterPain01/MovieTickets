using Netflix.Utils;
using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Netflix.ViewModel
{
    class HeaderViewModel : ViewModelBase
    {
        public ICommand SearchCommand;
        public ICommand NavigateToLanding;
        public HeaderViewModel()
        {
            SearchCommand = new ViewModelCommand(ExecuteSearchCommand);
            NavigateToLanding = new ViewModelCommand(ExecuteNavigateToLanding);
        }

        private void ExecuteSearchCommand(object parameter)
        {
            string title = parameter as string;
            NavigationStore._navigationStore.CurrentViewModel = new SearchViewModel(title);
        }

        private void ExecuteNavigateToLanding(object parameter)
        {
            NavigationStore._navigationStore.CurrentViewModel = new LandingViewModel(); 
        }

    }
}
