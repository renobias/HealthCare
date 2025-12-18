using Dapper;
using Healthcare_ApoointmentAvailability.Model;
using Healthcare_ApoointmentAvailability.Model.Response;
using Npgsql;
using System.Data;

namespace Healthcare_ApoointmentAvailability.Repository
{
    public class AppointmentRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public AppointmentRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("PostgresConnection");
        }

        private IDbConnection CreateConnection()
         => new NpgsqlConnection(_connectionString);

        public async Task<IEnumerable<CreateAppointment>> CreateAppointmentAsync(int doctorId, int patiendId, string start, int duration)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM appointment_slot(@DoctorId, @PatientId, @Start, @Duration)";
            var parameters = new
            {
                DoctorId = doctorId,
                PatientId = patiendId,
                Start = DateTimeOffset.Parse(start).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff K"),
                Duration = duration
            };
            return await connection.QueryAsync<CreateAppointment>(sql, parameters);
        }

        public async Task<IEnumerable<DeleteAppointment>> DeleteAppointmentAsync(int appointmentId)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM delete_appointment(@AppointmentId)";
            var parameters = new
            {
                AppointmentId = appointmentId
            };
            return await connection.QueryAsync<DeleteAppointment>(sql, parameters);
        }
    }
}
