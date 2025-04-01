using project_mvc.Services.Admin.Models;

namespace project_mvc.Services.Client.Models
{
	public class ModulePosition
	{
		public int Id { get; set; }
		public int ParentId { get; set; }

		public string? Name { get; set; }

		public string? Code { get; set; }
		public string? ModuleContentIds { get; set; }
		public string? ModuleProductIds { get; set; }
		public string? LinkBanner { get; set; }
		public string? ModuleProducts { get; set; }
		public string? TypeView { get; set; }
		public int? OrderDisplay { get; set; }
		public List<ProductAdminItem>? ProductItems { get; set; }

		public List<WebsiteModuleProducts>? WebsiteModuleProductItems { get; set; }
		public List<WebsiteModuleProducts>? WebsiteModuleContentItems { get; set; }
	}
}
