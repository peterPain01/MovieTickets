using Netflix.Repository;
using NetFlix.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.ViewModel
{
    public class RepositoryContainer
    {
        public static UserRepository userRepo = new UserRepository();
        public static MovieRepository MovieRepository = new MovieRepository();
        public static ShowTimeRepo ShowTimeRepo = new ShowTimeRepo();
    }
}
