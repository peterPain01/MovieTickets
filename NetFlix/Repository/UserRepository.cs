using Microsoft.EntityFrameworkCore;
using NetFlix.EnityModel;
using NetFlix.Model;
using NetFlix.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace NetFlix.Repository
{   
    // Need access token to Authorticate cause This Repo have some important function
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public static User CurrentUser { get; set; }

        public bool isLoggedIn()
        {
            return CurrentUser != null;
        }
        public bool Add(User user)
        {
            bool success = false;

            User validUser = GetByUsername(user.Username);
            if (validUser != null)
            {
                return false;
            }

            using (var context = new BookingMovieAppContext())
            {
                context.AddAsync(user);
                context.SaveChanges();
                success = true; 
            }
            return success;
        }
        public async Task<User> AuthenticatedUser(NetworkCredential credential)
        {
            var hashedPassword = helper.HassPassword(credential.Password);
            using (var context = new BookingMovieAppContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(user => user.Username.Equals(credential.UserName) && user.Password == hashedPassword);
                CurrentUser = user;
                return user;
            }
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
            using (var context = new BookingMovieAppContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Username.Equals(username));
                return user;
            }
        }

        public void Remove(User user)
        {
            throw new NotImplementedException();
        }
    
        public void UpdateUserInfo(User newUser)
        {
            using(var context = new BookingMovieAppContext())
            {
                var user = context.Users.FirstOrDefault(user => user.Id == newUser.Id);
                if(user != null)
                {
                    user.Email = newUser.Email; 
                    user.FullName = newUser.FullName;
                    user.BirthDate = newUser.BirthDate;
                    user.Gender = newUser.Gender;
                    context.SaveChanges(); 
                }

            }
        }
       
        public void UpdateUserPassword(int Id, SecureString Password)
        {
            using (var context = new BookingMovieAppContext())
            {
                var user = context.Users.FirstOrDefault(user => user.Id == Id);
                if (user != null)
                {
                    var credential = new NetworkCredential(null, Password); 
                    user.Password = helper.HassPassword(credential.Password);
                    context.SaveChanges();
                }

            }
        }
    }
}
