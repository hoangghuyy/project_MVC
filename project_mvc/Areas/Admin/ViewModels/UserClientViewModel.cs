using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class UserClientViewModel
	{
		public List<UserClientItem>? ListItems { get; set; }
		public UserClients? Item { get; set; }
		public int Total { get; set; }
		public int PageSize { get; set; }

	}
}
