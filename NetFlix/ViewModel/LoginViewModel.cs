using Netflix.Utils;
using NetFlix.CustomControls;
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
using System.Windows;
using System.Windows.Input;
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

        public ICommand NavigateSignupCommand { get; }
        // Constructor 


        public LoginViewModel()
        {
            this.userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));

            this._vm = new ToastViewModel();
            NavigateSignupCommand = new ViewModelCommand(ExecuteNavigateSignupCommand);
        }

        private void ExecuteNavigateSignupCommand(object obj)
        {
            NavigationStore._navigationStore.CurrentViewModel = new SignupVM();
        }
        private void ExecuteLoginCommand(object obj)
        {
            string valid_user = valid_Username(Username);
            string valid_password = valid_Password(Password);
            // Validation
            //if (valid_user != "")
            //{
            //    _vm.ShowWarning(valid_user, new MessageOptions
            //    {
            //        FontSize = 14,
            //        FreezeOnMouseEnter = true
            //    });
            //    return;
            //};

            //if (valid_password != "")
            //{
            //    _vm.ShowWarning(valid_user, new MessageOptions
            //    {
            //        FontSize = 14,
            //        FreezeOnMouseEnter = true
            //    });
            //    return;
            //};


            var isValidUser = userRepository.AuthenticatedUser(new System.Net.NetworkCredential(Username, Password));
            if (isValidUser)
            {
                //Thread.CurrentPrincipal = new GenericPrincipal(
                //    new GenericIdentity(Username), null
                //    );
                this.IsViewVisible = false;
                NavigationStore._navigationStore.CurrentViewModel = new LandingViewModel();
            }

            else
            {
                _vm.ShowError("* Invalid username or password");
                //this.ErrorMessage = "* Invalid username or password"; 

            }
        }

        private bool CanExecuteLoginCommand(object obj)
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
                return "Password mst have at least 6 characters";
            }
            return "";
        }
    }
}
