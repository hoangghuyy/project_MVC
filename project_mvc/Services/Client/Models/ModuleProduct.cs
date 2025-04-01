namespace project_mvc.Services.Client.Models
{
	public class ModuleProduct
	{
		public int Id { get; set; }
		public int ParentId { get; set; }

		public string? Name { get; set; }
		public string? NameAscii { get; set; }

		public string? LinkUrl { get; set; }
		public string? UrlPicture { get; set; }

	}
}
