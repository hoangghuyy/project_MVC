using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class ModulePositionViewModel
	{
		public List<ModulePositionItem>? ListItems { get; set; }
		public ModulePositions? Item { get; set; }
		public List<WebsiteModuleContents>? ListModuleContent { get; set; }
		public List<WebsiteModuleProducts>? ListModuleProduct { get; set; }
		public List<ModulePositions>? ListModulePosition { get; set; }
		public string? selectedValue { get; set; }

	}
}
