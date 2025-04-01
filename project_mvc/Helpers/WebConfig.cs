namespace project_mvc.Helpers
{
    public class WebConfig
    {
		private IConfiguration Configuration { get; set; }
		public const string AdminAlias = "AdminCms";
		public static string? ConnectionString { get; set; }
        

        public WebConfig(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
		{
			Configuration = configuration;
			ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];

		}
    }
}
