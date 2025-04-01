namespace project_mvc.Services.Admin.Models
{
    public class WebsiteModuleProducts
    {
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? NameAscii { get; set; }
		public string? ModuleTypeCode { get; set; }
		public string? Title { get; set; }
		public string? LinkUrl { get; set; }
		public int? OrderDisplay { get; set; }
		public string? UrlPicture { get; set; }
		public string? UrlVideo { get; set; }
		public bool IsShow { get; set; }
		public bool IsDeleted { get; set; } = false;
		public int? ParentId { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? ModuleIds { get; set; }

	}

	public class WebsiteModuleProductItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? NameAscii { get; set; }
		public string? ModuleTypeCode { get; set; }
		public string? Title { get; set; }
		public string? LinkUrl { get; set; }
		public int? OrderDisplay { get; set; }
		public string? UrlPicture { get; set; }
		public string? UrlVideo { get; set; }
		public bool IsShow { get; set; }
		public int? ParentId { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? ModuleIds { get; set; }

	}
}
