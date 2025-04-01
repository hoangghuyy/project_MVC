namespace project_mvc.Services.Client.Models
{
	public class ModuleContent
	{
		public int Id { get; set; }
		public int ParentId { get; set; }

		public string? Name { get; set; }
		public string? NameAscii { get; set; }

		public string? LinkUrl { get; set;}

	}
}
