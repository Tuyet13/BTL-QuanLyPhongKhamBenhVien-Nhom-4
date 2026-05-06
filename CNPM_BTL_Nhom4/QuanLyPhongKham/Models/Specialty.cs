using System.ComponentModel.DataAnnotations;

namespace FamilyDoctorMVC.Models
{
    public class Specialty
    {
        [Key]
        public int SpecialtyId { get; set; }

        public string SpecialtyName { get; set; }
    }
}
