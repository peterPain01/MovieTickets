using System.Data.SqlClient;


namespace NetFlix.Repository
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString; 
        public RepositoryBase()
        {
            _connectionString = "Server=(local); Database=BookingMovieApp; Integrated Security=true"; 
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection( _connectionString );
        }
    }
}
