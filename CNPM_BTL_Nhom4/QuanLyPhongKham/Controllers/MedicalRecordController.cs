using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;

namespace FamilyDoctorMVC.Controllers
{
    public class MedicalRecordController : Controller
    {
        public IActionResult Index()
        {
            string query = "SELECT * FROM MedicalRecord";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt);
        }

        // ================= CREATE =================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int AppointmentId, string Diagnosis, string Note)
        {
            string query = $@"
            INSERT INTO MedicalRecord(AppointmentId, Diagnosis, Note)
            VALUES({AppointmentId}, N'{Diagnosis}', N'{Note}')";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // ================= DETAILS =================
        public IActionResult Details(int id)
        {
            string query = $@"
            SELECT * FROM MedicalRecord
            WHERE RecordId = {id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return RedirectToAction("Index");

            return View(dt.Rows[0]);
        }

        // ================= EDIT =================
        public IActionResult Edit(int id)
        {
            string query = $@"
            SELECT * FROM MedicalRecord
            WHERE RecordId = {id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return RedirectToAction("Index");

            return View(dt.Rows[0]);
        }

        [HttpPost]
        public IActionResult Edit(int RecordId, int AppointmentId, string Diagnosis, string Note)
        {
            string query = $@"
            UPDATE MedicalRecord
            SET AppointmentId = {AppointmentId},
                Diagnosis = N'{Diagnosis}',
                Note = N'{Note}'
            WHERE RecordId = {RecordId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // ================= DELETE =================
        public IActionResult Delete(int id)
        {
            string query = $@"
            SELECT * FROM MedicalRecord
            WHERE RecordId = {id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return RedirectToAction("Index");

            return View(dt.Rows[0]);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int RecordId)
        {
            string query = $@"
            DELETE FROM MedicalRecord
            WHERE RecordId = {RecordId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }
    }
}
