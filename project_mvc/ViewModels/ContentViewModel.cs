using project_mvc.Services.Admin.Models;

namespace project_mvc.ViewModels
{
    public class ContentViewModel
    {
		public WebsiteContentItem? ContentItem { get; set; }
		public WebsiteModuleContentItem? ModuleItem { get; set; }
		public IEnumerable<WebsiteContentItem>? ListContentItem { get; set; }

	}
}
