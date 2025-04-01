using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;
using project_mvc.Helpers;
using project_mvc.Areas.Admin.Models;

namespace project_mvc.Areas.Admin.Controllers
{
	public class DepartmentController : BaseController
	{
		private readonly DepartmentDa DepartmentDa;
		private readonly BaseDa BaseDa;

		public DepartmentController()
		{
			DepartmentDa = new DepartmentDa(WebConfig.ConnectionString!);
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
			int pageSize = 1;
			DepartmentViewModel model = new()
			{
				ListItems = await DepartmentDa.ListSearch(search, search.Page, pageSize, false)
			};
			int total = model.ListItems != null && model.ListItems.Count != 0 ? model.ListItems.FirstOrDefault()!.TotalRecords : 0;
			ViewBag.Pagging = GetPage(search.Page, total, pageSize);
			ViewBag.Keyword = search.Keyword;
			return View(model);
		}

		[Obsolete]
		public async Task<IActionResult> AjaxForm(int id)
		{
			string action = "Add";
			DepartmentViewModel model = new()
			{
				Item = new()
			};
			if (id > 0)
			{
				model.Item = await DepartmentDa.GetId(id);
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
				Departments obj = new();
				await TryUpdateModelAsync(obj);
				switch (action)
				{
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên phòng ban không được để trống";
								return Ok(msg);
							}
							
							bool check = await DepartmentDa.CheckDepartment(obj.Name);
							if (check)
							{
								msg.Message = "Tên phòng ban đã tồn tại";
								return Ok(msg);
							}
							//obj.PasswordSalt = Utility.CreateSaltKey(8);
							//obj.Password = Utility.CreatePasswordHash(obj.Password!, obj.PasswordSalt);
							var rs = await BaseDa.Insert(obj, "Departments");
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
							var objOld = await DepartmentDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên phòng ban không được để trống";
								return Ok(msg);
							}
							objOld.Name = obj.Name;

							int rs = await BaseDa.Update(objOld, "Departments");
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
							int rs = await BaseDa.Delete(obj.Id, "Departments");
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
								int rs = await BaseDa.Delete(item, "Departments");

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
