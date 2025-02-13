using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class DepartmentViewModel
	{
		public List<DepartmentItem>? ListItems { get; set; }
		public Departments? Item { get; set; }
	}
}
