using System;

namespace FamilyDoctorMVC.Models
{
    public class Doctor
    {
        public string DoctorUsr { get; set; }

        public string DoctorPwd { get; set; }

        public string DoctorName { get; set; }

        public bool Gender { get; set; }

        public DateTime Dob { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int SpecialtyId { get; set; }
    }
}
