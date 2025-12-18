namespace Healthcare_ApoointmentAvailability.Model.Request
{
    public class CreateAppointment
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Start { get; set; }
        public int Duration { get; set; }
    }
}
