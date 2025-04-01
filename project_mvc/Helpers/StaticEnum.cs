namespace project_mvc.Helpers
{
    public class StaticEnum
    {
        // kieu hien thi
        public const string Product = "Product";
		public const string Introduce = "Introduce";
		public const string News = "News";
        public const string Contact = "Contact";
		public const string Recuitment = "Recuitment";
		public const string Trademark = "Trademark";
		public const string Partner = "Partner";




        public const string Add = "Add";
        public const string Edit = "Edit";
        public const string Delete = "Delete";
        public const string DeleteAll = "DeleteAll";

		public const string LogoHeader = "LogoHeader";
        public const string HomeBanner = "HomeBanner";
		public const string MenuMain = "MenuMain";
		public const string ModuleMain = "ModuleMain";
		public const string FooterMain = "FooterMain";
		public const string ProductIndex = "ProductIndex";
		public const string ProductHotIndex = "ProductHotIndex";
		public const string HomeIndex = "HomeIndex";
		public const string SlideIndex = "SlideIndex";




		public static string Show(bool? status) => status == true ? "<span class='badge bg-primary'>Hiện</span>" : "<span class='badge bg-danger'>Ẩn</span>";

    }
}
