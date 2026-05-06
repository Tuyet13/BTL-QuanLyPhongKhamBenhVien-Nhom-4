using System;

namespace FamilyDoctorMVC.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public int AppointmentId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentStatus { get; set; }
    }
}
