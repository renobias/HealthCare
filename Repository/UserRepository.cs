using Dapper;
using Healthcare_ApoointmentAvailability.Model;
using Npgsql;
using System.Data;

namespace Healthcare_ApoointmentAvailability.Repository
{
    public class UserRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("PostgresConnection");
        }

        private IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT username, email FROM users";
            return await connection.QueryAsync<User>(sql);
        }

        public async Task<IEnumerable<User>> GetAllUsersPatientAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT username, email FROM users where typeid = 2";
            return await connection.QueryAsync<User>(sql);
        }

        public async Task<IEnumerable<User>> GetAllUsersDoctorAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT username, email FROM users where typeid=1";
            return await connection.QueryAsync<User>(sql);
        }
    }
}
