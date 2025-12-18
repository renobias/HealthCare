using Healthcare_ApoointmentAvailability.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_ApoointmentAvailability.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorRepository _repo;

        public DoctorsController(DoctorRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}/availability")]
        public async Task<IActionResult> GetAllDoctorAvailable(
            [FromRoute] int id,
            [FromQuery] DateTimeOffset from,
            [FromQuery] DateTimeOffset to,
            [FromQuery] int slot = 15)
        {
            try
            {
                var doctorAvilSlots = await _repo.GetDoctorAvailableSlots(id,from, to, slot);
                int lengthAvilSlots = doctorAvilSlots.Count();
                string formatDate = "MM/dd/yyyy HH:mm:ss";
                string response = "Slot: ";

                foreach (var item in doctorAvilSlots)
                {
                    response += DateTime.ParseExact(item.Slot_Start_Time, formatDate, System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm") + ", ";
                }

                response = response + "(" + lengthAvilSlots + " slot)";

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "General Error",
                    details = ex.Message
                });
            }
        }
    }
}
