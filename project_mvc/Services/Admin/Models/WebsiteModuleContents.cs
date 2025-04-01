namespace project_mvc.Services.Admin.Models
{
	public class WebsiteModuleContents
	{
		public int Id { get; set; }
		public int ParentId { get; set; }
		public string? Name { get; set; }
		public string? NameAscii { get; set; }
        public string? ModuleTypeCode { get; set; }
        public string? Code { get; set; }
		public string? LinkUrl { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? Video { get; set; }
		public string? UrlPicture { get; set; }
		public string? AlbumPictureJson { get; set; }
		public bool? IsDeleted { get; set; } = false;
		public bool IsShow { get; set; }
		public DateTime? CreatedDate { get; set; } = DateTime.Now;
		public DateTime? ModifiedDate { get; set; } = DateTime.Now;
		public string? ModuleIds { get; set; }
		public int? OrderDisplay { get; set; }
	}

	public class WebsiteModuleContentItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }
		public int ParentId { get; set; }
		public string? Name { get; set; }
		public string? NameAscii { get; set; }
        public string? ModuleTypeCode { get; set; }
		public string? Code { get; set; }
		public string? LinkUrl { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? Video { get; set; }
		public string? UrlPicture { get; set; }
		public string? AlbumPictureJson { get; set; }
		public bool IsShow { get; set; } = true;
		public DateTime? CreatedDate { get; set; } = DateTime.Now;
		public DateTime? ModifiedDate { get; set; } = DateTime.Now;
		public int? OrderDisplay { get; set; }
		public string? ModuleIds { get; set; }
		public List<AlbumGalleryAdmin>? AlbumGalleryItems { get; set; }

	}
}
