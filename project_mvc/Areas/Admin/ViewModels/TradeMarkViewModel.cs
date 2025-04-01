using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class TradeMarkViewModel
	{
		public List<TradeMarkItem>? ListItems { get; set; }
		public TradeMarks? Item { get; set; }
        public SearchModel? SearchModel { get; set; }
		public List<ModuleTypes>? ListModuleType { get; set; }
		public int Total { get; set; }
		public int PageSize { get; set; }

	}
}
