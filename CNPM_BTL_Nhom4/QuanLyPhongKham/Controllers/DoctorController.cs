using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;

namespace FamilyDoctorMVC.Controllers
{
    public class DoctorController : Controller
    {
        // =========================
        // LIST
        // =========================
        public IActionResult Index()
        {
            DataTable dt = DbHelper.ExecuteQuery("SELECT * FROM Doctor");
            return View(dt);
        }

        // =========================
        // CREATE FORM
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // SAVE CREATE
        // =========================
        [HttpPost]
        public IActionResult Create(string DoctorUsr, string DoctorPwd, string DoctorName, string Phone, string Address)
        {
            string query = $@"
            INSERT INTO Doctor
            (DoctorUsr,DoctorPwd,DoctorName,Phone,Address)
            VALUES
            ('{DoctorUsr}','{DoctorPwd}',N'{DoctorName}','{Phone}',N'{Address}')";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index", "Home");
        }

        // =========================
        // DELETE
        // =========================
        public IActionResult Delete(string id)
        {
            string query = $"DELETE FROM Doctor WHERE DoctorUsr='{id}'";
            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index", "Home");
        }

        // =========================
        // DETAILS
        // =========================
        public IActionResult Details(string id)
        {
            DataTable dt = DbHelper.ExecuteQuery(
                $"SELECT * FROM Doctor WHERE DoctorUsr='{id}'"
            );

            return View(dt);
        }

        // =========================
        // EDIT FORM
        // =========================
        public IActionResult Edit(string id)
        {
            DataTable dt = DbHelper.ExecuteQuery(
                $"SELECT * FROM Doctor WHERE DoctorUsr='{id}'"
            );

            return View(dt);
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPost]
        public IActionResult Edit(string DoctorUsr, string DoctorPwd, string DoctorName, string Phone, string Address)
        {
            string query = $@"
            UPDATE Doctor
            SET DoctorPwd='{DoctorPwd}',
                DoctorName=N'{DoctorName}',
                Phone='{Phone}',
                Address=N'{Address}'
            WHERE DoctorUsr='{DoctorUsr}'";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index", "Home");
        }
    }
}
