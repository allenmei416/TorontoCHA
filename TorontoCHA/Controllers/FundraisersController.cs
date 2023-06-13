using Microsoft.AspNetCore.Mvc;

namespace TorontoCHA.UI.Controllers
{
    public class FundraisersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CurrentFundraiser()
        {
            return View();
        }

        public IActionResult PastFundraisers()
        {
            return View();
        }
    }
}
