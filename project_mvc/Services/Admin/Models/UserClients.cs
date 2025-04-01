namespace project_mvc.Services.Admin.Models
{
	public class UserClients
	{
		public int Id { get; set; }

		public string? UserName { get; set; }

		public string? Password { get; set; }

        public string? PasswordSalt { get; set; }

		public string? Name { get; set; }
		public string? Email { get; set; }

		public bool IsDeleted { get; set; } = false;

		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
	}

	public class UserClientItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }

		public string? UserName { get; set; }

		public string? Password { get; set; }

		public string? PasswordSalt { get; set; }

		public string? Name { get; set; }
		public string? Email { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
	}
}
