using Dapper;
using Healthcare_ApoointmentAvailability.Model.Response;
using Npgsql;
using System.Data;

namespace Healthcare_ApoointmentAvailability.Repository
{
    public class DoctorRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DoctorRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("PostgresConnection");
        }

        private IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);

        public async Task<IEnumerable<DoctorAvailableSlots>> GetDoctorAvailableSlots(int doctorId, DateTimeOffset from, DateTimeOffset to, int slotDuration)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM get_doctor_available_slots(@DoctorId, @From, @To, @SlotDuration)";
            var parameters = new
            {
                DoctorId = doctorId,
                From = from.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff K"),
                To = to.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff K"),
                SlotDuration = slotDuration
            };
            return await connection.QueryAsync<DoctorAvailableSlots>(sql, parameters);
        }
    }
}
