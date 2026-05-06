namespace FamilyDoctorMVC.Models
{
    public class Queue
    {
        public int QueueId { get; set; }

        public int AppointmentId { get; set; }

        public int QueueNumber { get; set; }

        public string Status { get; set; }
    }
}
