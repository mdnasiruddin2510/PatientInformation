using Microsoft.AspNetCore.Mvc;

namespace PatientInformation.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult NCD()
        {
            return View();
        }
    }
}
