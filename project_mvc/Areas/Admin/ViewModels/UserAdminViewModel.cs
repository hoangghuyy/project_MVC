using project_mvc.Services.Admin;
using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class UserAdminViewModel
	{
		public List<UserAdminItem>? ListItems { get; set; }
		public UserAdmins? Item { get; set; }
		public List<Departments>? ListDepartment { get; set; }
		public int Total { get; set; }
		public int PageSize { get; set; }

	}
}
