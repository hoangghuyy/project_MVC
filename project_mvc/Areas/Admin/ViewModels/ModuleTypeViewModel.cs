using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class ModuleTypeViewModel
	{
		public List<ModuleTypeItem>? ListItems { get; set; }
		public ModuleTypes? Item { get; set; }
	}
}
