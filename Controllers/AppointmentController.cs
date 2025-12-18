using Healthcare_ApoointmentAvailability.Model.Request;
using Healthcare_ApoointmentAvailability.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Healthcare_ApoointmentAvailability.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentRepository _repo;

        public AppointmentController(AppointmentRepository repo)
        {
            _repo = repo;
        }
        [HttpPost(Name = "post-appointment")]
        public async Task<IActionResult> PostAppointment([FromBody] CreateAppointment request)
        {
            try
            {
                var result = await _repo.CreateAppointmentAsync(request.DoctorId, request.PatientId, request.Start, request.Duration);
                var appointment = result.FirstOrDefault();
                string formatDate = "MM/dd/yyyy HH:mm:ss";

                if (result != null)
                {
                    var msg = appointment.Message;

                    if(msg == "out off office")
                    {
                        return BadRequest(new
                        {
                            message = "diluar jam kerja"
                        });
                    }
                    if (msg == "out off limit")
                    {
                        return BadRequest(new
                        {
                            message = "diluar jam kerja : waktu pelayanan tidak tercukupi"
                        });
                    }
                    if (msg == "double booking")
                    {
                        return Conflict(new
                        {
                            message = "tidak ada double booking"
                        });
                    }
                    if (msg == "overlap")
                    {
                        return Conflict(new
                        {
                            message = "overlap dengan " +  DateTime.ParseExact(appointment.Start_Time, formatDate, System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm") + " - " + DateTime.ParseExact(appointment.End_Time, formatDate, System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm")
                        });
                    }
                    if (msg == "created")
                    {
                        return Ok("Created : " + "slot " + DateTime.ParseExact(appointment.Start_Time, formatDate, System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm") + " - " + DateTime.ParseExact(appointment.End_Time, formatDate, System.Globalization.CultureInfo.InvariantCulture).ToString("HH:mm") + " tidak lagi tersedia");
                    }
                    return StatusCode(500, new
                    {
                        error = "General Error",
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        error = "General Error",
                    });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "General Error",
                    details = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointemnt([FromRoute] int id)
        {
            try
            {
                var result = await _repo.DeleteAppointmentAsync(id);
                var appointmentDeleted = result.FirstOrDefault();
                string formatDate = "MM/dd/yyyy HH:mm:ss";

                if (result != null)
                {
                    var msg = appointmentDeleted.Message;

                    if (msg == "exceeds time")
                    {
                        return BadRequest(new
                        {
                            message = "tidak boleh"
                        });
                    }
                    if (msg == "deleted")
                    {
                        return Ok(new
                        {
                            message = "slot kembali tersedia"
                        });
                    }
                    return StatusCode(500, new
                    {
                        error = "General Error"
                    });
                } else
                {
                    return StatusCode(500, new
                    {
                        error = "General Error"
                    });
                }
            }
            catch(Exception ex)
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
