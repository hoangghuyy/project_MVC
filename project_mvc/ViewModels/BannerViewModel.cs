using project_mvc.Services.Admin.Models;
using project_mvc.Services.Client.Models;

namespace project_mvc.ViewModels
{
	public class BannerViewModel
	{
		public List<Banner>? ListLogos { get; set; }
		public List<Banner>? ListSlides { get; set; }
        public List<Banner>? ListTradeMark { get; set; }
		public List<ModulePosition>? ListPositionContent { get; set; }
		public List<ModulePosition>? ListPositionProduct { get; set; }
		public List<ModulePosition>? ListPositionFooter { get; set; }
		public List<ModuleContent>? ListContent { get; set; }
		public List<ModuleProduct>? ListProduct { get; set; }
		public List<ModuleContent>? ListFooter { get; set; }

	}
}
