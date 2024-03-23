using NetFlix.CustomControls;
using NetFlix.EnityModel;
using NetFlix.Model;
using NetFlix.Utils;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public static User CurrentUser { get; set; }
        public bool isLoggedIn()
        {
            return CurrentUser != null;
        }
        public bool Add(User userModel) 
        {
            bool success = false;

            User user = GetByUsername(userModel.Username);
            if (user != null)
            {
         
                return false; 
            }

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = " INSERT INTO [Users] (username, password, birth_date, gender) VALUES (@username, @password, @dob, @gender)";
                command.Parameters.AddWithValue("@username", userModel.Username);
                command.Parameters.AddWithValue("@password", userModel.Password);
                command.Parameters.AddWithValue("@dob", userModel.Dob);
                command.Parameters.AddWithValue("@gender", userModel.Gender);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    success = true; 
                }
                connection.Close();
            }
            return success;
        }
        public User AuthenticatedUser(NetworkCredential credential)
        {
            User user = null;
            var hashedPassword = helper.HassPassword(credential.Password); 
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [Users] where username=@username and [password]=@password";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password",System.Data.SqlDbType.NVarChar).Value = hashedPassword;

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int isAdminIndex = reader.GetOrdinal("is_admin");
                    int isAdminValue = reader.IsDBNull(isAdminIndex) ? 0 : reader.GetInt32(isAdminIndex);
                    user = new User(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetString(reader.GetOrdinal("username")),
                        isAdminValue
                    );
                }

            }
            CurrentUser = user; 
            return user;
        }
        public void Edit(User userModel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
        public User GetById(int id)
        {
            throw new NotImplementedException();
        }
        public User GetByUsername(string username)
        {
            User user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Users] WHERE username = @username";
                command.Parameters.AddWithValue("@username", username);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new User(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("username")));
                }
            }
           return user;
        }

        public void Remove(User userModel)
        {
            throw new NotImplementedException();
        }
    }
}
