using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;

namespace project_mvc.Areas.Admin.Controllers
{
	public class UserClientController : BaseController
	{
		private readonly UserClientDa UserClientDa;
		private readonly BaseDa BaseDa;


		public UserClientController()
		{
			UserClientDa = new UserClientDa(WebConfig.ConnectionString!);
			BaseDa = new BaseDa(WebConfig.ConnectionString!);

		}
		
		public IActionResult Index()
		{
			return View();
		}

		[Obsolete]
		public async Task<IActionResult> ListItems()
		{
			SearchModel search = new();
			await TryUpdateModelAsync(search);
			search.Keyword = Utility.ValidString(search.Keyword!, "", true);
			int pageSize = 2;
			UserClientViewModel model = new()
			{
				ListItems = await UserClientDa.ListSearch(search, search.Page, pageSize),
			};
			int total = model.ListItems != null && model.ListItems.Count != 0 ? model.ListItems.FirstOrDefault()!.TotalRecords : 0;
			ViewBag.Pagging = GetPage(search.Page, total, pageSize);
			ViewBag.Keyword = search.Keyword;
			model.Total = total;
			model.PageSize = pageSize;
			return View(model);
		}

		[Obsolete]
		public async Task<IActionResult> AjaxForm(int id)
		{
			string action = "Add";
			UserClientViewModel model = new()
			{
				Item = new(),
			};
			if (id > 0)
			{
				model.Item = await UserClientDa.GetId(id);
				action = "Edit";
			}
			ViewBag.Action = action;
			ViewBag.ActionText = Utility.ActionText(action);
			return View(model);
		}

		
		[HttpPost]
		[Obsolete]
		public async Task<IActionResult> Action()
		{
			JsonMessage msg = new()
			{
				Errors = true,
				Message = "Xử lý thất bại"
			};
			try
			{
				var action = Request.Form["action"];
				UserAdmins obj = new();
				await TryUpdateModelAsync(obj);
				switch (action)
				{
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.UserName))
							{
								msg.Message = "Tên đăng nhập không được để trống";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Password))
							{
								msg.Message = "Mật khẩu không được để trống";
								return Ok(msg);
							}
							bool check = await UserClientDa.CheckUser(obj.UserName);
							if (check)
							{
								msg.Message = "Tên đăng nhập đã tồn tại";
								return Ok(msg);
							}
							obj.PasswordSalt = Utility.CreateSaltKey(8);
							obj.Password = Utility.CreatePasswordHash(obj.Password!, obj.PasswordSalt);
							var rs = await BaseDa.Insert(obj, "UserClients");
							if (rs > 0)
							{
								msg.Message = "Add new success";
								msg.Errors = false;
								return Ok(msg);
							}
						}
						break;
					case StaticEnum.Edit:
						{
							var objOld = await UserClientDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.UserName))
							{
								msg.Message = "Tên đăng nhập không được để trống";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Password))
							{
								msg.Message = "Mật khẩu không được để trống";
								return Ok(msg);
							}
							objOld.Name = obj.Name;

							int rs = await BaseDa.Update(objOld, "UserClients");
							if (rs > 0)
							{
								msg.Errors = false;
								msg.Message = "Cập nhật thành công";
								return Ok(msg);
							}
						}
						break;
					case StaticEnum.Delete:
						{
							int rs = await BaseDa.Delete(obj.Id, "UserClients");
							if (rs > 0)
							{
								msg.Errors = false;
								msg.Message = "Xoá thành công";
								return Ok(msg);
							}
						}
						break;
					case StaticEnum.DeleteAll:
						{
							foreach (int item in ArrID)
							{
								int rs = await BaseDa.Delete(item, "UserClients");

							}
							msg.Errors = false;
							msg.Message = "Xoá tất cả thành công";
							return Ok(msg);
						}
				}
			}
			catch (Exception ex)
			{
				return Ok(msg);
			}
			return Ok(msg);
		}
	}
}
