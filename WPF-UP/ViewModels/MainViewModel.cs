using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private IUser userRepository;

        public UserAccountModel CurrentUserAccount
        {

            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var users = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (users != null)
            {
                CurrentUserAccount.Username = users.Username;
                CurrentUserAccount.DisplayName = users.Name;
                CurrentUserAccount.DisplayLastName = users.LastName;
                CurrentUserAccount.DisplayMiddleName = users.MiddleName;             
            }
        }
    }
}
