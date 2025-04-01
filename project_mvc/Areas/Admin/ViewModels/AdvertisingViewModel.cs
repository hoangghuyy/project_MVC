using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class AdvertisingViewModel
	{
		public List<AdvertisingItem>? ListItems { get; set; }
		public Advertisings? Item { get; set; }
		
		public int Total { get; set; }
		public int PageSize { get; set; }
		public List<ModulePositions>? ListModulePosition { get; set; }
		public string? selectedValue { get; set; }
	}
}
