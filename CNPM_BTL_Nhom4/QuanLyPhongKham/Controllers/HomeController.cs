using Microsoft.AspNetCore.Mvc;

namespace FamilyDoctorMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
