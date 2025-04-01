using project_mvc.Areas.Admin.Controllers;
using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
    public class WebsiteModuleProductViewModel
    {
		public List<WebsiteModuleProductItem>? ListItems { get; set; }
		public WebsiteModuleProducts? Item { get; set; }
		public WebsiteModuleProducts? WebsiteModuleProducts { get; set; }
		public List<AlbumGalleryAdmin>? ListAlbumGalleryAdmin { get; set; }
		public List<ModuleTypes>? ListModuleType { get; set; }
		public List<WebsiteModuleProducts>? ListModuleProduct { get; set; }
		public List<ModulePositions>? ListModulePosition { get; set; }

		public string? selectedValue { get; set; }

	}
}
