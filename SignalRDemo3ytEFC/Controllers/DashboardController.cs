using Microsoft.AspNetCore.Mvc;

namespace SignalRDemo3ytEFC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
