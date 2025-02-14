using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Helpers;
using project_mvc.Services.Admin;
using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route(WebConfig.AdminAlias + "/[controller]/[action]")]
    public class PublicController : Controller
    {
        private readonly UserDa UserDa;
        public PublicController()
        {
            UserDa = new UserDa(WebConfig.ConnectionString!);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken()]
        [Obsolete]
        public async Task<IActionResult> LoginAction()
        {
            JsonMessage msg = new();
            LoginModel loginModel = new();
            await TryUpdateModelAsync(loginModel);
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                msg.Errors = true;
                msg.Message = "Tài khoản và mật khẩu không duoc de trong.";
                return Ok(msg);
            }
            loginModel.UserName = Utility.RemoveHTMLTag(loginModel.UserName);
            loginModel.Password = Utility.RemoveHTMLTag(loginModel.Password);

            UserAdmins? user = await UserDa.GetByUserName(loginModel.UserName);
            if (user == null)
            {
                msg.Errors = true;
                msg.Message = "Tài khoản và mật khẩu không duoc de trong.";
                return Ok(msg);
            }

            var pass1 = Utility.CreatePasswordHash(loginModel.Password, user.PasswordSalt!);
            if (pass1 != user.Password)
            {
                msg.Errors = true;
                msg.Message = "Tài khoản và mật khẩu không duoc de trong.";
                return Ok(msg);
            }

            SessionBase session = new(HttpContext);
            session.SetAdminUserId(Convert.ToString(user.Id));
            session.SetAdminRole(user.RoleCode!);
            return Ok(new
            {
                Url = "/" + WebConfig.AdminAlias + "/HomeAdmin/Index"
            });
        }
    }
}
