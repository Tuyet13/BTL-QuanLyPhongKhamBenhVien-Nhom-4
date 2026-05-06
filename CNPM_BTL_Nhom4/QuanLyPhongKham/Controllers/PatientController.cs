using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;
using FamilyDoctorMVC.Models;

namespace FamilyDoctorMVC.Controllers
{
    public class PatientController : Controller
    {

        // =========================
        // LIST
        // =========================
        public IActionResult Index()
        {
            string query = "SELECT * FROM Patient";
            DataTable dt = DbHelper.ExecuteQuery(query);

            List<Patient> list = new List<Patient>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Patient
                {
                    PatientId = Convert.ToInt32(row["PatientId"]),
                    PatientName = row["PatientName"].ToString(),
                    Gender = Convert.ToBoolean(row["Gender"]),
                    Dob = Convert.ToDateTime(row["Dob"]),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString(),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString()
                });
            }

            return View(list);
        }

        // =========================
        // DETAILS
        // =========================
        public IActionResult Details(int id)
        {
            string query = $"SELECT * FROM Patient WHERE PatientId={id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            Patient p = new Patient();

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];

                p.PatientId = Convert.ToInt32(row["PatientId"]);
                p.PatientName = row["PatientName"].ToString();
                p.Gender = Convert.ToBoolean(row["Gender"]);
                p.Dob = Convert.ToDateTime(row["Dob"]);
                p.Phone = row["Phone"].ToString();
                p.Address = row["Address"].ToString();
                p.Username = row["Username"].ToString();
                p.Password = row["Password"].ToString();
            }

            return View(p);
        }

        // =========================
        // CREATE
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Patient p)
        {
            string dob = p.Dob.ToString("yyyy-MM-dd");

            string query = $@"
            INSERT INTO Patient
            (PatientName,Gender,Dob,Phone,Address,Username,Password)
            VALUES
            (N'{p.PatientName}',
             '{(p.Gender ? 1 : 0)}',
             '{dob}',
             '{p.Phone}',
             N'{p.Address}',
             '{p.Username}',
             '{p.Password}')";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // =========================
        // EDIT
        // =========================
        public IActionResult Edit(int id)
        {
            string query = $"SELECT * FROM Patient WHERE PatientId={id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            Patient p = new Patient();

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];

                p.PatientId = Convert.ToInt32(row["PatientId"]);
                p.PatientName = row["PatientName"].ToString();
                p.Gender = Convert.ToBoolean(row["Gender"]);
                p.Dob = Convert.ToDateTime(row["Dob"]);
                p.Phone = row["Phone"].ToString();
                p.Address = row["Address"].ToString();
                p.Username = row["Username"].ToString();
                p.Password = row["Password"].ToString();
            }

            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Patient p)
        {
            string dob = p.Dob.ToString("yyyy-MM-dd");

            string query = $@"
            UPDATE Patient
            SET
            PatientName = N'{p.PatientName}',
            Gender = '{(p.Gender ? 1 : 0)}',
            Dob = '{dob}',
            Phone = '{p.Phone}',
            Address = N'{p.Address}',
            Username = '{p.Username}',
            Password = '{p.Password}'
            WHERE PatientId = {p.PatientId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // =========================
        // DELETE
        // =========================

        public IActionResult Delete(int id)
        {
            string query = $"SELECT * FROM Patient WHERE PatientId={id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return Content("KHÔNG CÓ DỮ LIỆU");

            var row = dt.Rows[0];

            Patient p = new Patient
            {
                PatientId = Convert.ToInt32(row["PatientId"]),
                PatientName = row["PatientName"].ToString(),
                Gender = Convert.ToBoolean(row["Gender"]),
                Dob = Convert.ToDateTime(row["Dob"]),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                Username = row["Username"].ToString(),
                Password = row["Password"].ToString()
            };

            return View(p);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int PatientId)
        {
            string query = $"DELETE FROM Patient WHERE PatientId={PatientId}";
            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // =========================
        // REGISTER
        // =========================
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Patient p)
        {
            string dob = p.Dob.ToString("yyyy-MM-dd");

            string query = $@"
            INSERT INTO Patient
            (PatientName,Gender,Dob,Phone,Address,Username,Password)
            VALUES
            (N'{p.PatientName}',
             '{(p.Gender ? 1 : 0)}',
             '{dob}',
             '{p.Phone}',
             N'{p.Address}',
             '{p.Username}',
             '{p.Password}')";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Login");
        }

        // =========================
        // LOGIN
        // =========================
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            string query = $@"
            SELECT * FROM Patient
            WHERE Username='{username}'
            AND Password='{password}'";

            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count > 0)
            {
                HttpContext.Session.SetString("user", username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        // =========================
        // LOGOUT
        // =========================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
