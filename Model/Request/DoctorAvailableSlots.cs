namespace Healthcare_ApoointmentAvailability.Model.Request
{
    public class DoctorAvailableSlots
    {
        public int DoctorId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Slot { get; set; }
    }
}
