namespace Healthcare_ApoointmentAvailability.Model.Response
{
    public class CreateAppointment
    {
        public int IdBooking { get; set; }
        public int DoctorId { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public int PatientId { get; set; }
        public int Duration { get; set; }
        public string Message { get; set; }

    }
}
