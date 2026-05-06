using System;

namespace FamilyDoctorMVC.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public bool Gender { get; set; }

        public DateTime Dob { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
