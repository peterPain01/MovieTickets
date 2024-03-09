using NetFlix.CustomControls;
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
using XAct.Users;
namespace NetFlix.Repository
{
    class UserRepository : RepositoryBase, IUserRepository
    {
        public bool Add(UserModel userModel) 
        {
            bool success = false;


            UserModel user = GetByUsername(userModel.Username);
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
        public bool AuthenticatedUser(NetworkCredential credential)
        {
            bool validUser;

            var hashedPassword = helper.HassPassword(credential.Password); 
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [Users] where username=@username and [password]=@password";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password",System.Data.SqlDbType.NVarChar).Value = hashedPassword;

                validUser = command.ExecuteScalar() == null ? false : true; 
                connection.Close();
            }
            return validUser;
        }
        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
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
                command.CommandText = "SELECT * FROM [Users] WHERE username = @username";
                command.Parameters.AddWithValue("@username", username);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserModel(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("username")));
                }
            }
           return user;
        }

        public void Remove(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
