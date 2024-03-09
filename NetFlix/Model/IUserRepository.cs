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
        bool AuthenticatedUser(NetworkCredential credential); 
        bool Add(UserModel userModel); 
        void Edit(UserModel userModel);
        void Remove(UserModel userModel);
        UserModel GetById(int id);
        UserModel GetByUsername(string username);
        IEnumerable<UserModel> GetAll();
    }
}
