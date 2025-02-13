using Microsoft.AspNetCore.Mvc;
using project_mvc.Helpers;

namespace project_mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route(WebConfig.AdminAlias)]
    [CheckLoginAdmin]
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
