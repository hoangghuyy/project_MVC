namespace project_mvc.Services.Admin.Models
{
	public class ModuleTypes
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public int? OrderDisplay { get; set; }

		public bool IsShow { get; set; }

		public string? Code { get; set; }

		public bool IsDeleted { get; set; } = false;
	}

	public class ModuleTypeItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }

		public string? Name { get; set; }

		public int? OrderDisplay { get; set; }

		public bool IsShow { get; set; }

		public string? Code { get; set; }

		public bool IsDeleted { get; set; } = false;
	}
}
