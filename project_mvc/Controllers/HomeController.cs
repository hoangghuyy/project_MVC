using Microsoft.AspNetCore.Mvc;
using project_mvc.Dappers;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Client;
using project_mvc.Services.Client.Models;
using project_mvc.ViewModels;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace project_mvc.Controllers
{

	public class HomeController : Controller
	{
		private readonly BannerManager _bannerManager;
		private readonly PositionManager _positionManager;
		//private readonly DapperDA DapperDA = new DapperDA();
		private readonly DapperDA DapperDA;


		public HomeController()
		{
			_bannerManager = new BannerManager(WebConfig.ConnectionString!);
			_positionManager = new PositionManager(WebConfig.ConnectionString!);
			DapperDA = new DapperDA(WebConfig.ConnectionString!);

		}
		[Obsolete]
		public async Task<IActionResult> Index(string moduleIds)
		{

			string view = StaticEnum.HomeIndex;
			List<ModulePosition> listPosition = new();
			IEnumerable<ProductAdminItem> listProduct = new List<ProductAdminItem>();

			try
			{
				listPosition = _positionManager.GetListViewIndex(view);
				if (listPosition.Any())
				{
					listPosition.ForEach(x =>
					{

						if (x.TypeView == StaticEnum.Product)
						{
							x.WebsiteModuleProductItems = _positionManager.GetListModuleInPositionIds(x.ModuleProductIds!);

							x.ProductItems = _positionManager.GetListProductByModule(x.ModuleProductIds!);

						}
						//if (x.TypeView == StaticEnum.Content)
						//{
						//	x.WebsiteModuleContentItems = _positionManager.GetListModuleInPositionIds(x.ModuleProductIds!);

						//	x.ContentItems = _positionManager.GetListProductByModule(x.ModuleProductIds!);


						//}
						//if (x.TypeView == StaticEnum.Module)
						//{
						//	x.WebsiteModuleProductItems = _positionManager.GetListModuleInPositionIds(x.ModuleProductIds!);
						//	x.WebsiteModuleContentItems = _positionManager.GetListModuleInPositionIds(x.ModuleProductIds!);

						//}
						//if (x.TypeView == StaticEnum.Banner)
						//{
						//	x.BannerItems = _positionManager.GetListProductByModule(x.ModuleProductIds!);
						//}
					});
				}
			}
			catch (Exception ex)
			{
				return null;

			}
			HomeViewModel model = new()
			{
				ListSlides = await _bannerManager.GetBannerToList(StaticEnum.HomeBanner),
				ListTradeMark = await _bannerManager.GetBannerToList(StaticEnum.Partner),
				//ListCode = await _positionManager.GetPositionToList(StaticEnum.ProductIndex),
				//ListModuleProductIds = await _positionManager.GetModuleProductIds(moduleIds)
				ListPosition = listPosition,

			};
			return View(model);
		}
	}
}
