using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using FamilyDoctorMVC.Data;

namespace FamilyDoctorMVC.Controllers
{
    public class LoginController : Controller
    {
        // trang login
        public IActionResult Index()
        {
            return View();
        }

        // xử lý login
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            string sql = @"SELECT * FROM Account 
                           WHERE Username=@u AND Password=@p";

            SqlParameter[] parameters =
            {
                new SqlParameter("@u", username),
                new SqlParameter("@p", password)
            };

            DataTable dt = DbHelper.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                HttpContext.Session.SetString("user", username);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        // logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}
