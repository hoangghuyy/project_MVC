namespace project_mvc.Services.Admin.Models
{
	public class Departments
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int? OrderDisplay { get; set; }
		public bool IsDeleted { get; set; } = false;

	}
	public class DepartmentItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }
		public string? Name { get; set; }
		public int? OrderDisplay { get; set; }

	}
}
