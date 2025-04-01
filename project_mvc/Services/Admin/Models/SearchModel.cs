namespace project_mvc.Services.Admin.Models
{
	public class SearchModel
	{
		public string? Keyword { get; set; }
		public int Page {  get; set; } = 1;
		public int Sort {  get; set; } = 1;
		public string? selected { get; set; }
        public string? unselected { get; set; }

	}
}
