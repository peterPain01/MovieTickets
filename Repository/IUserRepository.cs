using NetFlix.EnityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Model
{
    public interface IUserRepository
    {
        Task<User> AuthenticatedUser(NetworkCredential credential);
        bool isLoggedIn(); 
        bool Add(User user); 
        void Edit(User userModel);
        void Remove(User userModel);
        User GetById(int id);
        User GetByUsername(string username);
        IEnumerable<User> GetAll();
    }
}
