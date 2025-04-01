using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class ProductViewModel
	{
        public List<ProductAdminItem>? ListItems { get; set; }
        public Products? Item { get; set; }
		public Products? Products { get; set; }
		public List<AlbumGalleryAdmin>? ListAlbumGalleryAdmin { get; set; }
		public List<WebsiteModuleProducts>? ListModuleProduct { get; set; }
		public List<TradeMarks>? ListTradeMark { get; set; }

		public int Total { get; set; }
        public int PageSize { get; set; }
    }
}
