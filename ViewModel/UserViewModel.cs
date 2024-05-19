using NetFlix.CustomControls;
using NetFlix.EnityModel;
using NetFlix.Repository;
using NetFlix.Utils;
using NetFlix.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetFlix.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        // Get all Booking of User and display 
        // Update password
        // Update User profile 
        private User _currentUser;
        private SecureString _password; 
        private SecureString _c_password;
        private ObservableCollection<NetFlix.EnityModel.Booking> _allBooking; 

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public SecureString CPassword
        {
            get { return _c_password; }
            set
            {
                _c_password = value;
                OnPropertyChanged(nameof(CPassword));
            }
        }
         
        
        public ObservableCollection<NetFlix.EnityModel.Booking> AllBookings
        {
            get { return _allBooking; }
            set
            {
                _allBooking = value;
                OnPropertyChanged(nameof(AllBookings));
            }
        }

        public ICommand OpenChangeInfoFormCommand { get; set; }  
        public ICommand OpenChangePasswordFormCommand { get; set; }
        public ICommand ChangeUserInfoCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        private UserRepository _userRepository { get; set; }
        private BookingRepository _bookingRepository { get; set; }
        private ToastViewModel vm = new ToastViewModel(); 


        public UserViewModel()
        {
            CurrentUser = UserRepository.CurrentUser;
            OpenChangeInfoFormCommand = new ViewModelCommand(OpenChangeUserInfo);
            OpenChangePasswordFormCommand = new ViewModelCommand(OpenChangePassword);
            ChangeUserInfoCommand = new ViewModelCommand(ChangeUserInfo);
            ChangePasswordCommand = new ViewModelCommand(ChangePassword);

            _userRepository = new UserRepository();
            _bookingRepository = new BookingRepository();
            AllBookings = _bookingRepository.GetBookingByUserId(CurrentUser.Id); 
        }

        private void OpenChangeUserInfo(object p)
        {
            var form = new EditUserInfoForm();
            form.DataContext = this;
            form.CommandEditUserInfo = ChangeUserInfoCommand; 
            form.Show();
        }

        private void OpenChangePassword(object p)
        {
            var form = new EditUserPasswordForm();
            form.DataContext = this;
            form.CommandEditUserPassword = ChangePasswordCommand;
            form.Show();
        }

        private void ChangeUserInfo(object p)
        {
            _userRepository.UpdateUserInfo(CurrentUser);
            vm.ShowSuccess("Your info successfully updated");
        }

        private void ChangePassword(object p)
        {
            if (helper.CompareSecureStrings(Password, CPassword) == false)
            {
                vm.ShowError("2 Password must be match, please try again");
            }
            else
            {
                _userRepository.UpdateUserPassword(CurrentUser.Id, Password);
                vm.ShowSuccess("Your password successfully updated");
            }
        }



    }
}
