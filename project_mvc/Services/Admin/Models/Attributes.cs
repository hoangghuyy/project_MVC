namespace project_mvc.Services.Admin.Models
{
	public class Attributes
	{
		public int Id { get; set; }
		public int? ParentId { get; set; }
		public string? Name { get; set; }
		public string? NameAscii { get; set; }
		public string? LinkUrl { get; set; }
		public bool IsShow { get; set; }
		public int? OrderDisplay { get; set; }
		public bool IsDeleted { get; set; } = false;
		public string? Description { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public string? ListModuleIds { get; set; }
		public string? UrlPicture { get; set; }


	}

	public class AttributeItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }
		public int? ParentId { get; set; }
		public string? Name { get; set; }
		public string? NameAscii { get; set; }
		public string? LinkUrl { get; set; }
		public bool IsShow { get; set; }
        public int? Number { get; set; }
		public int? OrderDisplay { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public string? ListModuleIds { get; set; }
		public string? UrlPicture { get; set; }
	}
}
