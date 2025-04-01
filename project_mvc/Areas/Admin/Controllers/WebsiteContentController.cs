using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;

namespace project_mvc.Areas.Admin.Controllers
{
	public class WebsiteContentController : BaseController
	{
		private readonly WebsiteContentDa WebsiteContentDa;
		private readonly WebsiteModuleContentDa WebsiteModuleContentDa;
		private readonly WebsiteModuleProductDa WebsiteModuleProductDa;
		private readonly BaseDa BaseDa;



		public WebsiteContentController()
		{
			WebsiteContentDa = new WebsiteContentDa(WebConfig.ConnectionString!);
			WebsiteModuleContentDa = new WebsiteModuleContentDa(WebConfig.ConnectionString!);
			WebsiteModuleProductDa = new WebsiteModuleProductDa(WebConfig.ConnectionString!);
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
			WebsiteContentViewModel model = new()
			{
				ListItems = await WebsiteContentDa.ListSearch(search, search.Page, pageSize),
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

			WebsiteContentViewModel model = new()
			{
				Item = new(),
				ListModuleContent = await WebsiteModuleContentDa.GetAll(),
				//ListModuleProduct = await WebsiteModuleProductDa.GetAll()

			};
			if (id > 0)
			{
				model.Item = await WebsiteContentDa.GetId(id);
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
			SessionBase session = new(HttpContext);
			var username = session.GetAdminUserName();
			var userId = session.GetAdminUserId();
			try
			{
				var action = Request.Form["action"];
				WebsiteContents obj = new();
				await TryUpdateModelAsync(obj);
				switch (action)
				{
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tiêu đề không được để trống";
								return Ok(msg);
							}

							bool check = await WebsiteContentDa.CheckContent(obj.Name);
							if (check)
							{
								msg.Message = "Tiêu đề đã tồn tại";
								return Ok(msg);
							}
							obj.CreatorName = username;
							obj.CreatorID = Convert.ToInt32(userId != null ? userId : 0);
							var rs = await BaseDa.Insert(obj, "WebsiteContents");
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
							var objOld = await WebsiteContentDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tiêu đề không được để trống";
								return Ok(msg);
							}
							objOld.Name = obj.Name;
							objOld.NameAscii = obj.NameAscii;
							objOld.Title = obj.Title;
							objOld.OrderDisplay = obj.OrderDisplay;
							objOld.UrlPicture = obj.UrlPicture;
							objOld.LinkUrl = obj.LinkUrl;

							objOld.ModuleNameAscii = obj.ModuleNameAscii;

							int rs = await BaseDa.Update(objOld, "WebsiteContents");
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
							int rs = await BaseDa.Delete(obj.Id, "WebsiteContents");
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
								int rs = await BaseDa.Delete(item, "WebsiteContents");

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
