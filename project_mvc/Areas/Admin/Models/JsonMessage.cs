namespace project_mvc.Areas.Admin.Models
{
    [Serializable]
    public class JsonMessage
    {
        public bool Errors { get; set; } = true;
        public string? Message { get; set; }
        //public object? Obj { get; set; }
    }
}
