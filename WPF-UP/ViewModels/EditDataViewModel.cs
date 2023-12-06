using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class EditDataViewModel : ViewModelBase
    {
        private UserModel _selectedUser;

        public ObservableCollection<UserModel> Users { get; set; }

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

        public EditDataViewModel()
        {
            // Загрузка пользователей (предположим, что у вас есть метод для этого)
            Users = new ObservableCollection<UserModel>(LoadUsers());

            EditUserCommand = new ViewModelCommand(EditUser);
        }

        private void EditUser(object parameter)
        {

            try
            {
                if (SelectedUser != null)
                {
                    var userRepository = new UserRepository();
                    userRepository.EditUser(SelectedUser);

                    // Обновление пользователя в коллекции
                    var updatedUserIndex = Users.IndexOf(Users.FirstOrDefault(u => u.Username == SelectedUser.Username));
                    if (updatedUserIndex != -1)
                    {
                        Users[updatedUserIndex] = SelectedUser;
                    }
                }
                else
                {
                    MessageView messageView = new MessageView("Ошибка", "Вы ничего не редактировали!");
                    messageView.Show();
                }
            }
            catch (Exception ex)
            {
                MessageView messageView = new MessageView("Ошибка редактирования", $"Произошла ошибка: {ex.Message}");
                messageView.Show();
            }
        }

        private IEnumerable<UserModel> LoadUsers()
        {
            // Загрузка пользователей из репозитория или другого источника данных
            var userRepository = new UserRepository();
            return userRepository.GetByAll();
        }
    }
}
