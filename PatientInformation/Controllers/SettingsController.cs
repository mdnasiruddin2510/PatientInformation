using Microsoft.AspNetCore.Mvc;

namespace PatientInformation.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult NCD()
        {
            return View();
        } 
        public IActionResult Allergies()
        {
            return View();
        }
        public IActionResult Disease()
        {
            return View();
        }
    }
}
