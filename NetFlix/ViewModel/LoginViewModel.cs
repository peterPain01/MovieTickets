using Netflix.Utils;
using NetFlix.Model;
using NetFlix.Repository;
using NetFlix.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetFlix.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        // Fields 
        private string _username; 
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;  
        // Properties 
        public string Username
        {
            get => _username;
            set
            {
                this._username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get => _password;
            set
            {
                this._password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                this._errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                this._isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // -> Commands 
        public ICommand LoginCommand { get; } 
        public ICommand RecoverPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand RememberPasswordCommand { get; set; }

        public ICommand NavigateSignupCommand { get;  }
        // Constructor 

        private NavigationStore _navigationStore; 
        public LoginViewModel(NavigationStore navigationStore)
        {
            this.userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));

            this._navigationStore = navigationStore;
            NavigateSignupCommand = new ViewModelCommand(ExecuteNavigateSignupCommand); 
        }

        private void ExecuteNavigateSignupCommand(object obj)
        {
            _navigationStore.CurrentViewModel = new SignupVM(_navigationStore); 
        }
        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticatedUser(new System.Net.NetworkCredential(Username, Password), connection);
            
            if(isValidUser)
            {
                //Thread.CurrentPrincipal = new GenericPrincipal(
                //    new GenericIdentity(Username), null
                //    );
                this.IsViewVisible = false;

                // open Lading User Page 
                _navigationStore.CurrentViewModel = new LandingViewModel(_navigationStore); 
            }

            else
            {
                this.ErrorMessage = "* Invalid username or password"; 
            }
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrEmpty(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
            {
                validData = false;
            }
            else 
                validData = true;
            return validData;
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException(); 
        }
    }
}
