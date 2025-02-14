using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Helpers;
using project_mvc.Services.Admin;
using project_mvc.Services.Admin.Models;

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
