using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.Repositories
{
    public class UserRepository : DataBase, IUser
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [Users] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            List<UserModel> users = new List<UserModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Users]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new UserModel()
                        {
                            Username = reader[1].ToString(),
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            MiddleName = reader[5].ToString(),
                        };

                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public void RemoveByUsername(string username)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [Users] WHERE username = @username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                command.ExecuteNonQuery();
            }
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [Users] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            MiddleName = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public void RegisterUser(UserModel user)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO Users (Username, LastName, Name, MiddleName, Password) " +
                               "VALUES (@Username, @LastName, @Name, @MiddleName, @Password)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                    command.Parameters.AddWithValue("@Password", user.Password); // Исправлено на @Password

                    command.ExecuteNonQuery();
                }
            }
        }
        public void EditUser(UserModel user)
        {
            if (user == null)
            {
                MessageView messageView = new MessageView("Ошибка", "Вы ничего не редактировали!");
                messageView.Show();
            }
            else
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "UPDATE Users SET LastName = @LastName, Name = @Name, MiddleName = @MiddleName WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@MiddleName", user.MiddleName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            
        }
    }
}
