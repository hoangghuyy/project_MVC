using Microsoft.AspNetCore.Mvc;
using project_mvc.Helpers;
using project_mvc.Services.Client;
using project_mvc.ViewModels;

namespace project_mvc.ViewComponents
{
	public class HeadPageViewComponent : ViewComponent
	{
		private readonly BannerManager _bannerManager;
		private readonly PositionManager _positionManager;
		private readonly ModuleContentManager _modulContentManager;
		private readonly ModuleProductManager _modulProductManager;
		public HeadPageViewComponent()
		{
			_bannerManager = new BannerManager(WebConfig.ConnectionString!);
			_positionManager = new PositionManager(WebConfig.ConnectionString!);
			_modulContentManager = new ModuleContentManager(WebConfig.ConnectionString!);
			_modulProductManager = new ModuleProductManager(WebConfig.ConnectionString!);
		}

		[Obsolete]
		public async Task<IViewComponentResult> InvokeAsync()
		{
			BannerViewModel model = new()
			{
				ListLogos = await _bannerManager.GetBannerToList(StaticEnum.LogoHeader),
				ListPositionContent = await _positionManager.GetPositionToList(StaticEnum.MenuMain),
				ListPositionProduct = await _positionManager.GetPositionToList(StaticEnum.ModuleMain),
				ListContent = await _modulContentManager.GetContentToList(2),
				ListProduct = await _modulProductManager.GetContentToList(3)

			};
			return await Task.FromResult<IViewComponentResult>(View(model));
		}
	}
}
