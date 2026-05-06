using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;

namespace FamilyDoctorMVC.Controllers
{
    public class ReceptionController : Controller
    {

        // =========================
        // INDEX - LIST APPOINTMENT
        // =========================
        public IActionResult Index()
        {
            string query =
            @"SELECT a.AppointmentId,
                     p.PatientName,
                     d.DoctorName,
                     a.AppointmentDate,
                     a.Status
              FROM Appointment a
              JOIN Patient p ON a.PatientId = p.PatientId
              JOIN Doctor d ON a.DoctorUsr = d.DoctorUsr";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt);
        }


        // =========================
        // DETAILS
        // =========================
        public IActionResult Details(int id)
        {
            string query =
            $@"SELECT a.AppointmentId,
                      p.PatientName,
                      d.DoctorName,
                      a.AppointmentDate,
                      a.Status
               FROM Appointment a
               JOIN Patient p ON a.PatientId = p.PatientId
               JOIN Doctor d ON a.DoctorUsr = d.DoctorUsr
               WHERE a.AppointmentId = {id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt.Rows[0]);
        }


        // =========================
        // EDIT PAGE
        // =========================
        public IActionResult Edit(int id)
        {
            string query =
            $@"SELECT a.AppointmentId,
                      p.PatientName,
                      d.DoctorName,
                      a.AppointmentDate,
                      a.Status
               FROM Appointment a
               JOIN Patient p ON a.PatientId = p.PatientId
               JOIN Doctor d ON a.DoctorUsr = d.DoctorUsr
               WHERE a.AppointmentId = {id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt.Rows[0]);
        }


        // =========================
        // EDIT SAVE
        // =========================
        [HttpPost]
        public IActionResult Edit(int AppointmentId, DateTime AppointmentDate, string Status)
        {
            string date = AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss");

            string query =
            $@"UPDATE Appointment
               SET AppointmentDate = '{date}',
                   Status = '{Status}'
               WHERE AppointmentId = {AppointmentId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }


        // =========================
        // DELETE PAGE
        // =========================
        public IActionResult Delete(int id)
        {
            string query =
            $@"SELECT a.AppointmentId,
                      p.PatientName,
                      d.DoctorName,
                      a.AppointmentDate
               FROM Appointment a
               JOIN Patient p ON a.PatientId = p.PatientId
               JOIN Doctor d ON a.DoctorUsr = d.DoctorUsr
               WHERE a.AppointmentId = {id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt.Rows[0]);
        }


        // =========================
        // DELETE CONFIRM
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int AppointmentId)
        {
            string query =
            $"DELETE FROM Appointment WHERE AppointmentId = {AppointmentId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }


        // =========================
        // COMPLETE APPOINTMENT
        // =========================
        public IActionResult Complete(int id)
        {
            string query =
            $"UPDATE Appointment SET Status='Completed' WHERE AppointmentId={id}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }


        // =========================
        // PRINT PAGE
        // =========================
        public IActionResult Print(int id)
        {
            string query =
            $@"SELECT a.AppointmentId,
                      p.PatientName,
                      d.DoctorName,
                      a.AppointmentDate,
                      a.Status
               FROM Appointment a
               JOIN Patient p ON a.PatientId = p.PatientId
               JOIN Doctor d ON a.DoctorUsr = d.DoctorUsr
               WHERE a.AppointmentId = {id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt.Rows[0]);
        }

    }
}
