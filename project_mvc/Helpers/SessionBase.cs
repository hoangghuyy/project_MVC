namespace project_mvc.Helpers
{
	public class SessionBase(HttpContext httpContext)
	{
		readonly HttpContext context = httpContext;
		public void SetAdminUserId(string id) => context.Session.SetString("AdminUserID", id);
		public string GetAdminUserId() => context.Session.GetString("AdminUserID")!;
		public void SetAdminRole(string RoleCode)
		=> context.Session.SetString("AdminRole", RoleCode);
		public string GetAdminRole()
		=> context.Session.GetString("AdminRole")!;

	}
}
