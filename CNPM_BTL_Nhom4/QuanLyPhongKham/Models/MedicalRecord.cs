namespace FamilyDoctorMVC.Models
{
    public class MedicalRecord
    {
        public int RecordId { get; set; }

        public int AppointmentId { get; set; }

        public string Diagnosis { get; set; }

        public string Note { get; set; }
    }
}
