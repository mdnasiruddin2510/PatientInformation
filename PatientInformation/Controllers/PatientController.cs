using Microsoft.AspNetCore.Mvc;

namespace PatientInformation.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
