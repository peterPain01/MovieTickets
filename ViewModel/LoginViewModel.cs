using Netflix.Utils;
using Netflix.View;
using Netflix.ViewModel;
using NetFlix.CustomControls;
using NetFlix.Model;
using NetFlix.Repository;
using NetFlix.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ToastNotifications.Core;

namespace NetFlix.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private ToastViewModel _vm;
        // Fields 
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private IUserRepository userRepository;
        private bool _isLoggingIn;


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

        public bool IsLoggingIn
        {
            get { return _isLoggingIn; }
            set
            {
                _isLoggingIn = value;
                OnPropertyChanged(nameof(IsLoggingIn));
            }
        }

        // -> Commands 
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand RememberPasswordCommand { get; set; }

        public ICommand NavigateSignupCommand { get; }
        // Constructor 


        public LoginViewModel()
        {
            this.userRepository = new UserRepository();
            LoginCommand = new AsyncRelayCommand(async () => ExecuteLoginCommand(), () => CanExecuteLoginCommand());
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));

            this._vm = new ToastViewModel();
            NavigateSignupCommand = new ViewModelCommand(ExecuteNavigateSignupCommand);
        }

        private void ExecuteNavigateSignupCommand(object obj)
        {
            NavigationStore._navigationStore.CurrentViewModel = new SignupVM();
        }
        private async Task ExecuteLoginCommand()
        {

            Application.Current.Dispatcher.Invoke(() => { IsLoggingIn = true; OnPropertyChanged(nameof(IsLoggingIn)); }) ;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            string valid_user = valid_Username(Username);
            string valid_password = valid_Password(Password);
            // Validation
            if (valid_user != "")
            {
                //_vm.ShowWarning(valid_user, new MessageOptions
                //{
                //    FontSize = 14,
                //    FreezeOnMouseEnter = true
                //});
                //ErrorMessage = valid_user; 
            };

            if (valid_password != "")
            {
                //_vm.ShowWarning(valid_user, new MessageOptions
                //{
                //    FontSize = 14,
                //    FreezeOnMouseEnter = true
                //});
                //ErrorMessage = valid_password;
                //return;
            };

            

            NetworkCredential credential = new NetworkCredential(Username, Password);
            var user = await userRepository.AuthenticatedUser(credential);
            if (user != null && (user.IsAdmin == 0 || user.IsAdmin == null))
            {
                //Thread.CurrentPrincipal = new GenericPrincipal(
                //    new GenericIdentity(Username), null
                //    );
                IsLoggingIn = false;
                NavigationStore._navigationStore.CurrentViewModel = new LandingViewModel();
            }
            else if (user != null && user.IsAdmin == 1)
            {
                IsLoggingIn = false;
                NavigationStore._navigationStore.CurrentViewModel = new AdminDashBoardVM();
            }
            else
            {
                IsLoggingIn = false;
                _vm.ShowError("* Invalid username or password");
                this.ErrorMessage = "* Invalid username or password"; 
            }
            Mouse.OverrideCursor = null;
        }

        private bool CanExecuteLoginCommand()
        {
            bool validData;
            if (string.IsNullOrEmpty(Username) || Password == null)
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

        private string valid_Username(string username)
        {
            if (username.Length < 6)
            {
                return "Username must have at least 6 characters";
            }
            if (username.Length > 20)
            {
                return "Maximize charactes is 20";
            }
            if (!char.IsLetter(username[0]))
                return "Username must start with letter";
            return "";
        }
        private string valid_Password(SecureString password)
        {
            if (password.Length < 6)
            {
                return "Password must have at least 6 characters";
            }
            return "";
        }
    }
}
