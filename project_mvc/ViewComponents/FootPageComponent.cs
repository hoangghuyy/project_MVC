using Microsoft.AspNetCore.Mvc;
using project_mvc.Helpers;
using project_mvc.Services.Client;
using project_mvc.ViewModels;

namespace project_mvc.ViewComponents
{
	//[ViewComponent(Name = "Footer")] //Solution
	public class FootPageViewComponent : ViewComponent
    {
		private readonly PositionManager _positionManager;
		private readonly ModuleContentManager _modulContentManager;
		public FootPageViewComponent()
		{
			_positionManager = new PositionManager(WebConfig.ConnectionString!);
			_modulContentManager = new ModuleContentManager(WebConfig.ConnectionString!);
		}

		[Obsolete]
		public async Task<IViewComponentResult> InvokeAsync()
		{
			BannerViewModel model = new()
			{
				ListPositionFooter = await _positionManager.GetPositionToList(StaticEnum.FooterMain),
				ListFooter = await _modulContentManager.GetContentToList(5)

			};
			return await Task.FromResult<IViewComponentResult>(View(model));
		}

	}
}
