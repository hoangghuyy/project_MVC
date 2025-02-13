using Microsoft.AspNetCore.Mvc;

namespace project_mvc.Areas.Admin.Controllers
{
    public class WebsiteModuleContentController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
