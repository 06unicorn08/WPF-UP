using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class UserEditorViewModel : ViewModelBase
    {
        private UserModel _selectedUser;
        private ObservableCollection<UserModel> _users;

        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ICommand EditUserCommand { get; }

        private IUserRepository userEditor;

        public UserEditorViewModel()
        {
            userEditor = new UserRepository();
            Users = new ObservableCollection<UserModel>(userEditor.GetByAll());
            EditUserCommand = new ViewModelCommand(EditUser, CanEditUser);
        }

        private bool CanEditUser(object parameter)
        {
            return SelectedUser != null;
        }

        private void EditUser(object parameter)
        {
            if (SelectedUser == null)
                return;

            Users = new ObservableCollection<UserModel>(userEditor.GetByAll());
        }
    }
}
