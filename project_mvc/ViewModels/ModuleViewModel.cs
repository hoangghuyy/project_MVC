using project_mvc.Services.Admin.Models;
using project_mvc.Services.Client.Models;

namespace project_mvc.ViewModels
{
    public class ModuleViewModel
    {
        public WebsiteModuleContentItem? ModuleContentItem { get; set; }
        public WebsiteModuleProductItem? ModuleProductItem { get; set; }
		public List<ModulePosition>? ListPositionProduct { get; set; }
		//public List<ModuleProduct>? ListProduct { get; set; }
		public List<WebsiteModuleProductItem>? ListModuleProducts { get; set; }
		public IEnumerable<ProductAdminItem>? ListProductItem { get; set; }



	}
}
