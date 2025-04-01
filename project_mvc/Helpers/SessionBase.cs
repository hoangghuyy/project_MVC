namespace project_mvc.Helpers
{
	public class SessionBase(HttpContext httpContext)
	{
		readonly HttpContext context = httpContext;
		public void SetAdminUserId(string id) => context.Session.SetString("AdminUserID", id);
		public void SetAdminUserName(string username) => context.Session.SetString("AdminUserName", username);
		public string GetAdminUserId() => context.Session.GetString("AdminUserID")!;
		public string GetAdminUserName() => context.Session.GetString("AdminUserName")!;
		public void SetAdminRole(string RoleCode)
		=> context.Session.SetString("AdminRole", RoleCode);
		public string GetAdminRole()
		=> context.Session.GetString("AdminRole")!;

	}
}
