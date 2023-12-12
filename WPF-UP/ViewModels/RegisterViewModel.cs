using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _username;
        private string _lastName;
        private string _name;
        private string _middleName;
        private SecureString _password; // Используем SecureString для пароля
        private string _errorMessage;

        public string Username
        {
            get { return _username; }
            set
            {

                 _username = value;
                 OnPropertyChanged(nameof(Username));

            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string FirstName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(MiddleName));
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

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new ViewModelCommand(Register);
        }

        private void Register(object parameter)
        {
            try
            {
                if (Username.Length < 3)
                {
                    MessageView messageView = new MessageView("Ошибка", "Логин не может быть короче 3 символов!");
                    messageView.Show();
                }
                else if (Password.Length < 6)
                {
                    MessageView messageView = new MessageView("Ошибка", "Пароль не может быть короче 6 символов!");
                    messageView.Show();
                }
                else
                {
                    var passwordPtr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(_password);
                    var password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(passwordPtr);
                    var userRepository = new UserRepository();
                    userRepository.RegisterUser(new UserModel
                    {
                        Username = Username,
                        LastName = LastName,
                        Name = FirstName,
                        MiddleName = MiddleName,
                        Password = password
                    });

                    // Очистка конвертированных данных
                    System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(passwordPtr);

                    Username = string.Empty;
                    LastName = string.Empty;
                    FirstName = string.Empty;
                    MiddleName = string.Empty;
                    Password.Clear(); 
                    ErrorMessage = string.Empty;

                    if (parameter is Window window)
                    {
                        window.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка регистрации: {ex.Message}";
            }
        }
    }
}
