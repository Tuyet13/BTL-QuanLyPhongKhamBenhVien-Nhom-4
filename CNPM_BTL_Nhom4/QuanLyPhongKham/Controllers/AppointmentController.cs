using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;
using FamilyDoctorMVC.Models;

namespace FamilyDoctorMVC.Controllers
{
    public class AppointmentController : Controller
    {
        // ================= LIST =================
        public IActionResult Index()
        {
            DataTable dt = DbHelper.ExecuteQuery("SELECT * FROM Appointment");

            List<Appointment> list = new List<Appointment>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Appointment
                {
                    AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                    PatientId = Convert.ToInt32(row["PatientId"]),
                    DoctorUsr = row["DoctorUsr"].ToString(),
                    AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                    Status = row["Status"].ToString()
                });
            }

            return View(list);
        }

        // ================= DETAILS =================
        public IActionResult Details(int id)
        {
            string query = $"SELECT * FROM Appointment WHERE AppointmentId = {id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return Content("KHÔNG CÓ DỮ LIỆU");

            var row = dt.Rows[0];

            Appointment a = new Appointment
            {
                AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                PatientId = Convert.ToInt32(row["PatientId"]),
                DoctorUsr = row["DoctorUsr"].ToString(),
                AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                Status = row["Status"].ToString()
            };

            return View(a);
        }

        // ================= CREATE =================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment a)
        {
            string date = a.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss");

            string query = $@"
            INSERT INTO Appointment
            (PatientId, DoctorUsr, AppointmentDate, Status)
            VALUES
            ({a.PatientId}, '{a.DoctorUsr}', '{date}', 'Pending')";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // ================= EDIT =================
        public IActionResult Edit(int id)
        {
            string query = $"SELECT * FROM Appointment WHERE AppointmentId = {id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return Content("KHÔNG CÓ DỮ LIỆU");

            var row = dt.Rows[0];

            return View(new Appointment
            {
                AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                PatientId = Convert.ToInt32(row["PatientId"]),
                DoctorUsr = row["DoctorUsr"].ToString(),
                AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                Status = row["Status"].ToString()
            });
        }

        [HttpPost]
        public IActionResult Edit(Appointment a)
        {
            string date = a.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss");

            string query = $@"
            UPDATE Appointment
            SET PatientId = {a.PatientId},
                DoctorUsr = '{a.DoctorUsr}',
                AppointmentDate = '{date}',
                Status = '{a.Status}'
            WHERE AppointmentId = {a.AppointmentId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // ================= DELETE =================
        public IActionResult Delete(int id)
        {
            string query = $"SELECT * FROM Appointment WHERE AppointmentId = {id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return Content("KHÔNG CÓ DỮ LIỆU");

            var row = dt.Rows[0];

            return View(new Appointment
            {
                AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                PatientId = Convert.ToInt32(row["PatientId"]),
                DoctorUsr = row["DoctorUsr"].ToString(),
                AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                Status = row["Status"].ToString()
            });
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int AppointmentId)
        {
            string query = $"DELETE FROM Appointment WHERE AppointmentId = {AppointmentId}";
            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }
    }
}
