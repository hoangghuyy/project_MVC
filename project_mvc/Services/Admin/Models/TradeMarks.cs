namespace project_mvc.Services.Admin.Models
{
	public class TradeMarks
	{
		public int Id { get; set; }
		public int? ParentId { get; set; }
		public string? Name { get; set; }
		public string? UrlPicture { get; set; }
		public string? ModuleTypeCode { get; set; }
		public int? OrderDisplay { get; set; }
		public bool? IsShow { get; set; }
		public bool IsDeleted { get; set; }
	}
	public class TradeMarkItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }
		public int? ParentId { get; set; }
		public string? Name { get; set; }
		public string? UrlPicture { get; set; }
		public string? ModuleTypeCode { get; set; }
		public int? OrderDisplay { get; set; }
		public bool? IsShow { get; set; }
		public bool IsDeleted { get; set; }
	}
}
