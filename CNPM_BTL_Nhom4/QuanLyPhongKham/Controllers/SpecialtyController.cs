using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;

namespace FamilyDoctorMVC.Controllers
{
    public class SpecialtyController : Controller
    {
        // LIST
        public IActionResult Index()
        {
            string query = "SELECT * FROM Specialty";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt);
        }

        // CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string SpecialtyName)
        {
            string query =
            $@"INSERT INTO Specialty(SpecialtyName)
               VALUES(N'{SpecialtyName}')";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            string query =
            $@"SELECT * FROM Specialty
               WHERE SpecialtyId={id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt.Rows[0]);
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            string query =
            $@"SELECT * FROM Specialty
               WHERE SpecialtyId={id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt.Rows[0]);
        }

        [HttpPost]
        public IActionResult Edit(int SpecialtyId, string SpecialtyName)
        {
            string query =
            $@"UPDATE Specialty
               SET SpecialtyName=N'{SpecialtyName}'
               WHERE SpecialtyId={SpecialtyId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            string query =
            $@"SELECT * FROM Specialty
               WHERE SpecialtyId={id}";

            DataTable dt = DbHelper.ExecuteQuery(query);

            return View(dt.Rows[0]);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int SpecialtyId)
        {
            string query =
            $@"DELETE FROM Specialty
               WHERE SpecialtyId={SpecialtyId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }
    }
}
