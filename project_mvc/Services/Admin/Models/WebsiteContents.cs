namespace project_mvc.Services.Admin.Models
{
	public class WebsiteContents
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Title { get; set; }
		public string? NameAscii { get; set; }
		public string? LinkUrl { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? Video { get; set; }
		public string? UrlPicture { get; set; }
		public int CreatorID { get; set; }
		public string? CreatorName { get; set; }
		public string? ModuleIds { get; set; }
		public int? OrderDisplay { get; set; }
		public bool? IsShow { get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime PublishDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
		public string? ModifiedName { get; set; }
		public string? ModuleNameAscii { get; set; }
	}

	public class WebsiteContentItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Title { get; set; }
		public string? NameAscii { get; set; }
		public string? LinkUrl { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? Video { get; set; }
		public string? UrlPicture { get; set; }
		public Guid? CreatorID { get; set; }
		public string? CreatorName { get; set; }
		public string? ModuleIds { get; set; }
		public int? OrderDisplay { get; set; }
		public bool? IsShow { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime PublishDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
		public string? ModifiedName { get; set; }
		public string? ModuleNameAscii { get; set; }
		public string? ModuleNames { get; set; }
    }
}
