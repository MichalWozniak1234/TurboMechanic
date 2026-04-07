using System.Diagnostics;
using Company.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Contact() => View();
        public IActionResult About() => View();
        public IActionResult Products() => View();
        public IActionResult StyleGuide() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
