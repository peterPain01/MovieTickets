using Netflix.Repository;
using Netflix.Utils;
using NetFlix.Repository;
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
        public ICommand NavigateToUserProfile;
        public ICommand NavigateToNowShowing;
        public ICommand NavigateToAdvanceSearch;
        public ICommand NavigateToLogin;

        public HeaderViewModel()
        {
            SearchCommand = new ViewModelCommand(ExecuteSearchCommand);
            NavigateToLanding = new ViewModelCommand(ExecuteNavigateToLanding);
            NavigateToUserProfile = new ViewModelCommand(ExecuteNavigateToUserProfile);
            NavigateToNowShowing = new ViewModelCommand(ExecuteNavigateToNowShowing);
            NavigateToAdvanceSearch = new ViewModelCommand(ExecuteNavigateToAdvanceSearch);
            NavigateToLogin = new ViewModelCommand(ExecuteNavigateToLogin);
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

        private void ExecuteNavigateToUserProfile(object parameter)
        {
            NavigationStore._navigationStore.CurrentViewModel = new UserViewModel();
        }

        private void ExecuteNavigateToNowShowing(object parameter)
        {
            MovieRepository movieRepository = new MovieRepository(); 
            NavigationStore._navigationStore.CurrentViewModel = new NowShowing(movieRepository);
        }
        
        private void ExecuteNavigateToAdvanceSearch(object parameter)
        {
            NavigationStore._navigationStore.PrevViewModel = new LandingViewModel(); 
            NavigationStore._navigationStore.CurrentViewModel = new AdvanceSearch();
        }
        
        private void ExecuteNavigateToLogin(object parameter)
        {
            NavigationStore._navigationStore.CurrentViewModel = new LoginViewModel();
            UserRepository.CurrentUser = null;
        }

    }
}
