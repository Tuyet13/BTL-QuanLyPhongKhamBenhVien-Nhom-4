namespace FamilyDoctorMVC.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        public int RecordId { get; set; }

        public string MedicineName { get; set; }

        public int Quantity { get; set; }
    }
}
