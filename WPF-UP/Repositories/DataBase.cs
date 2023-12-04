using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WPF_LoginForm.Repositories
{
    public abstract class DataBase
    {
        private readonly string _connectionString;
        public DataBase()
        {
            _connectionString = @"Server=DESKTOP-F72EN31; Database=RegWPF; Integrated Security=true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
