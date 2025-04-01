using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
    public class WebsiteModuleContentViewModel
    {
		public List<WebsiteModuleContentItem>? ListItems { get; set; }
		public WebsiteModuleContents? Item { get; set; }
		public WebsiteModuleContents? WebsiteModuleContents { get; set; }
		public List<AlbumGalleryAdmin>? ListAlbumGalleryAdmin { get; set; }
		public List<ModuleTypes>? ListModuleType { get; set; }
		public List<WebsiteModuleContents>? ListModuleContent { get; set; }
		public List<WebsiteModuleProducts>? ListModuleProduct { get; set; }
		public List<ModulePositions>? ListModulePosition { get; set; }

		public string? selectedValue { get; set; }
        //public int Total { get; set; }
        //public int PageSize { get; set; }
    }
}
