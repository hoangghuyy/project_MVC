namespace project_mvc.Services.Admin.Models
{
	public class UserAdmins
	{
		public int Id { get; set; }

		public string? UserName { get; set; }

		public string? Password { get; set; }

		public string? PasswordSalt { get; set; }

		public string? Name { get; set; }

		public int? DepartmentId { get; set; }

		public string? RoleCode { get; set; } = "USER";

		public string? ModuleIds { get; set; }

		public string? ModuleWebsiteIds { get; set; }

		public bool IsDeleted { get; set; } = false;

		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
	}

	public class UserAdminItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }

		public string? UserName { get; set; }

		public string? Name { get; set; }

        public string? DepartmentName { get; set; }

        public string? RoleCode { get; set; } = "USER";

		public string? ModuleIds { get; set; }

		public string? ModuleWebsiteIds { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
	}
}
