namespace project_mvc.Services.Admin.Models
{
	public class ModulePositions
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int? ParentId { get; set; }
		public bool IsShow { get; set; }
		public bool IsDeleted { get; set; } = false;
		public int? OrderDisplay { get; set; }
		public string? Code { get; set; }
		public string? TypeView { get; set; }
		public string? ModuleTypeCode { get; set; }
		public int? NumberCount { get; set; }
		public int? NumberContent { get; set; }
		public string? ModuleContentIds { get; set; }
		public string? ModuleProductIds { get; set; }
		public string? UrlPicture { get; set; }
		public string? UrlPictureMobile { get; set; }
		public string? Video { get; set; }
		public string? LinkBanner { get; set; }
	}

	public class ModulePositionItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int? ParentId { get; set; }
		public bool IsShow { get; set; }
		public int? OrderDisplay { get; set; }
		public string? Code { get; set; }
		public string? TypeView { get; set; }
		public string? ModuleTypeCode { get; set; }
		public int? NumberCount { get; set; }
		public int? NumberContent { get; set; }
		public string? UrlPicture { get; set; }
		public string? UrlPictureMobile { get; set; }
		public string? Video { get; set; }
		public string? LinkBanner { get; set; }
		public string? ModuleContentIds { get; set; }
		public string? ModuleProductIds { get; set; }

	}
}
