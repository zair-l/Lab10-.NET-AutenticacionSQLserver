using System.Web.Mvc;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Courses");
        }
    }
}
