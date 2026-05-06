using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using FamilyDoctorMVC.Data;

namespace FamilyDoctorMVC.Controllers
{
    public class PrescriptionController : Controller
    {

        // ================= LIST =================
        public IActionResult Index()
        {
            string query = @"
            SELECT 
                p.PrescriptionId,
                p.RecordId,
                ISNULL(m.Diagnosis,'') AS Diagnosis,
                ISNULL(m.Note,'') AS Note,
                p.MedicineName,
                p.Quantity
            FROM Prescription p
            LEFT JOIN MedicalRecord m
            ON p.RecordId = m.RecordId";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt);
        }


        // ================= CREATE =================
        public IActionResult Create()
        {
            string query = "SELECT RecordId FROM MedicalRecord";
            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt);
        }

        [HttpPost]
        public IActionResult Create(int RecordId, string MedicineName, int Quantity)
        {
            string query = @"
            INSERT INTO Prescription(RecordId, MedicineName, Quantity)
            VALUES(@RecordId,@MedicineName,@Quantity)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RecordId",RecordId),
                new SqlParameter("@MedicineName",MedicineName),
                new SqlParameter("@Quantity",Quantity)
            };

            DbHelper.ExecuteNonQuery(query, parameters);

            return RedirectToAction("Index");
        }


        // ================= DETAILS =================
        public IActionResult Details(int id)
        {
            string query = @"
            SELECT * FROM Prescription
            WHERE PrescriptionId=@id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };

            DataTable dt = DbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return RedirectToAction("Index");

            return View(dt.Rows[0]);
        }


        // ================= EDIT =================
        public IActionResult Edit(int id)
        {
            string query = "SELECT * FROM Prescription WHERE PrescriptionId=@id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };

            DataTable dt = DbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return RedirectToAction("Index");

            return View(dt.Rows[0]);
        }

        [HttpPost]
        public IActionResult Edit(int PrescriptionId, int RecordId, string MedicineName, int Quantity)
        {
            string query = @"
            UPDATE Prescription
            SET RecordId=@RecordId,
                MedicineName=@MedicineName,
                Quantity=@Quantity
            WHERE PrescriptionId=@PrescriptionId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RecordId",RecordId),
                new SqlParameter("@MedicineName",MedicineName),
                new SqlParameter("@Quantity",Quantity),
                new SqlParameter("@PrescriptionId",PrescriptionId)
            };

            DbHelper.ExecuteNonQuery(query, parameters);

            return RedirectToAction("Index");
        }


        // ================= DELETE =================
        public IActionResult Delete(int id)
        {
            string query = "SELECT * FROM Prescription WHERE PrescriptionId=@id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };

            DataTable dt = DbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return RedirectToAction("Index");

            return View(dt.Rows[0]);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int PrescriptionId)
        {
            string query = "DELETE FROM Prescription WHERE PrescriptionId=@id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id",PrescriptionId)
            };

            DbHelper.ExecuteNonQuery(query, parameters);

            return RedirectToAction("Index");
        }

    }
}
