using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Controllers;
using project_mvc.Helpers;

namespace project_mvc.Controllers
{
	[Route("cart")]
	[Route("[controller]/[action]")]
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
