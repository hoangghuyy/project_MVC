using Microsoft.AspNetCore.Mvc;

namespace project_mvc.Controllers
{
	[Route("checkout")]
	[Route("[controller]/[action]")]
	public class CheckoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
