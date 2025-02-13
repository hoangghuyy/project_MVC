using Microsoft.AspNetCore.Mvc;

namespace project_mvc.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseController
    {
        public HomeAdminController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
