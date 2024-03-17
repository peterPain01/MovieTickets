using Netflix.Utils;
using NetFlix.CustomControls;
using NetFlix.Model;
using NetFlix.Repository;
using NetFlix.Utils;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Core;
using XSystem.Security.Cryptography;

namespace NetFlix.ViewModel

{
    public class SignupVM : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private SecureString _confirm_password;
        private DateTime _dob;
        private string _gender; 
        private string _errorMessage;

        private IUserRepository userRepository;
        ToastViewModel _vm;

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
        public SecureString Confirm_password
        {
            get => _confirm_password;
            set
            {
                this._confirm_password = value;
                OnPropertyChanged(nameof(Confirm_password));
            }
        }
       
  
        public DateTime Dob { get => _dob; set {
                this._dob = value;
                OnPropertyChanged(nameof(Dob));
            } }
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public string ErrorMessage { get => _errorMessage; set {
                this._errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            } }

        public ICommand SignupCommand { get; }

        public ICommand NavigateToLoginCommand { get; }
        public SignupVM()
        {
            _vm = new ToastViewModel();
            _dob = DateTime.Today; 
            userRepository = new UserRepository(); 
            SignupCommand = new ViewModelCommand(ExecuteSignupCommand, CanExecuteSignupCommand);

            NavigateToLoginCommand = new ViewModelCommand(ExecuteNavigateToLoginCommand); 
        }

        private void ExecuteNavigateToLoginCommand(object parameter)
        {
            NavigationStore._navigationStore.CurrentViewModel = new LoginViewModel(); 
        }
        private void ExecuteSignupCommand(object obj)
        {
            if (helper.CompareSecureStrings(Password, Confirm_password) == false)
            {
                ErrorMessage = "Confirm password must be matched";
                return;
            }

            var unhashed  = new System.Net.NetworkCredential(string.Empty, Password).Password;
            var hasedPassword = helper.HassPassword(unhashed);
            try
            {
                var result = userRepository.Add(new User(Username, hasedPassword, Dob, Gender));
                if(result == false)
                {
                    _vm.ShowError("Username already exists");
                }
                else
                {
                    _vm.ShowSuccess("Account succesfully created");
                }
              
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message; 
            }
        }

        private bool CanExecuteSignupCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3 
                || Confirm_password == null || Confirm_password.Length < 3)
            {
                return false; 
            }
            
            else validData = true; 
            return validData;
        }
    }
}
