using project_mvc.Services.Client.Models;

namespace project_mvc.ViewModels
{
	public class HomeViewModel
	{
		public List<Banner>? ListSlides { get; set; }
		public List<Banner>? ListTradeMark { get; set; }
		public List<ModulePosition>? ListCode { get; set; }
		public List<ModulePosition>? ListPosition { get; set; }
		public List<ModulePosition>? ListModuleProductIds { get; set; }
	}
}
