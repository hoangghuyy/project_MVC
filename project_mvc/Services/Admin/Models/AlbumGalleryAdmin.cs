namespace project_mvc.Services.Admin.Models
{
	public class AlbumGalleryAdmin
	{
		public string? ID { get; set; }
		public string? AlbumTitle { get; set; }
		public string? AlbumUrl { get; set; }
        public int? AlbumType { get; set; }
        public int AlbumOrderDisplay { get; set; }
		public string? AlbumAlt { get; set; }
	}

}
