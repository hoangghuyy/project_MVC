namespace project_mvc.Services.Admin.Models
{
	public class Advertisings
	{
		public int Id { get; set; } 

		public string? Name { get; set; } // NOT NULL

		public string? UrlPicture { get; set; }

		public string? UrlPictureMobile { get; set; }

		public string? LinkUrl { get; set; }

		public string? Video { get; set; }

		public string? Description { get; set; }

		public string? Content { get; set; } 
		public string? PositionCode { get; set; }

        public int? OrderDisplay { get; set; }

		public int? ParentID { get; set; }

		public bool? IsShow { get; set; }

		public bool IsDeleted { get; set; } = false; 

		public DateTime CreatedDate { get; set; } = DateTime.Now; 
		public string? ModuleIds { get; set; } 

		public string? Title { get; set; }
	}

	public class AdvertisingItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }

		public string? Name { get; set; }

		public string? UrlPicture { get; set; }

		public string? UrlPictureMobile { get; set; }

		public string? LinkUrl { get; set; }

		public string? Video { get; set; }

		public string? Description { get; set; }

		public string? Content { get; set; }

		public string? PositionCode { get; set; }
		public int? OrderDisplay { get; set; }

        public int? ParentID { get; set; }

		public bool? IsShow { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public string? ModuleIds { get; set; }

		public string? Title { get; set; }
	}
}
