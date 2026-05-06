using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;

namespace FamilyDoctorMVC.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            string query =
            @"SELECT i.*,a.AppointmentDate
            FROM Invoice i
            JOIN Appointment a ON i.AppointmentId=a.AppointmentId";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt);
        }
    }
}
