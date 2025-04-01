using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class WebsiteContentViewModel
	{
		public List<WebsiteContentItem>? ListItems { get; set; }
		public List<WebsiteModuleContents>? ListModuleContent { get; set; }
		public WebsiteContents? Item { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
    }
}
