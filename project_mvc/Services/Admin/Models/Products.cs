namespace project_mvc.Services.Admin.Models
{
	public class Products
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? NameAscii { get; set; }
		public string? ModuleNameAscii { get; set; }
		public string? LinkUrl { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? ProductCode { get; set; }
		public string? UrlPicture { get; set; }
		public string? AlbumPictureJson { get; set; }
		public decimal? Price { get; set; }
		public decimal? PriceOld { get; set; }
		public int CreatorID { get; set; }
		public string? CreatorName { get; set; }
		public string? ModuleIds { get; set; }
		public int? OrderDisplay { get; set; }
		public int? BrandId { get; set; }
		public DateTime PublishDate { get; set; } = DateTime.Now;
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
		public string? ModifiedName { get; set; }
		public bool IsDeleted { get; set; } = false;
		public bool IsShow { get; set; }
	}

	public class ProductAdminItem
	{
		public int TotalRecords { get; set; }
		public int Id { get; set; }
		public string? _Name;
		public string? Name
		{
			get
			{
				if (!string.IsNullOrEmpty(ProductCode) && !_Name.Contains(ProductCode))
					return _Name + " " + ProductCode;
				return _Name;
			}
			set
			{
				_Name = value;
			}
		}
		public string? _NameAscii { get; set; }
		public string NameAscii
		{
			get
			{
				if (!string.IsNullOrEmpty(ModuleNameAscii))
					return ModuleNameAscii + "/" + _NameAscii;
				return _NameAscii!;
			}
			set
			{
				_NameAscii = value;
			}
		}
		public string? ModuleNameAscii { get; set; }
		public string? LinkUrl { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? ProductCode { get; set; }
		public string? UrlPicture { get; set; }
		public string? AlbumPictureJson { get; set; }
		public decimal? Price { get; set; }
		public decimal? PriceOld { get; set; }
		public int CreatorID { get; set; }
		public string? CreatorName { get; set; }
		public string? ModuleIds { get; set; }
		public int? OrderDisplay { get; set; }
		public int? BrandId { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime PublishDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
		public string? ModifiedName { get; set; }
        public bool IsShow { get; set; }

    }
}
