using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;

namespace project_mvc.Areas.Admin.Controllers
{
    public class WebsiteModuleProductController : BaseController
    {
		private readonly WebsiteModuleProductDa WebsiteModuleProductDa;
		private readonly ModuleTypeDa ModuleTypeDa;
		private readonly ModulePositionDa ModulePositionDa;
		private readonly BaseDa BaseDa;



		public WebsiteModuleProductController()
		{
			WebsiteModuleProductDa = new WebsiteModuleProductDa(WebConfig.ConnectionString!);
			ModuleTypeDa = new ModuleTypeDa(WebConfig.ConnectionString!);
			ModulePositionDa = new ModulePositionDa(WebConfig.ConnectionString!);
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
			WebsiteModuleProductViewModel model = new()
			{
				ListItems = await WebsiteModuleProductDa.ListSearch(search, search.Page, pageSize, false),
				ListModuleType = await ModuleTypeDa.GetAll(),
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
			WebsiteModuleProductViewModel model = new()
			{
				Item = new(),
				ListItems = await WebsiteModuleProductDa.GetAllById(id),
				ListModuleType = await ModuleTypeDa.GetAll(),
				ListModulePosition = await ModulePositionDa.GetAll(),

			};
			if (id > 0)
			{
				model.Item = await WebsiteModuleProductDa.GetId(id);
				action = "Edit";
			}
			ViewBag.Action = action;
			ViewBag.ActionText = Utility.ActionText(action);
			return View(model);
		}

		[Obsolete]
		public async Task<ActionResult> AjaxGridSelectProduct(string? selected = null)
		{

			WebsiteModuleProductViewModel model = new()
			{
				ListModuleProduct = await WebsiteModuleProductDa.GetAll(),
				selectedValue = selected
			};
			return View(model);
		}

		[Obsolete]
		public async Task<ActionResult> AjaxSelect(string? selected = null)
		{

			WebsiteModuleProductViewModel model = new()
			{
				ListModuleProduct = await WebsiteModuleProductDa.GetAll(),
				selectedValue = selected
			};
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
				WebsiteModuleProducts obj = new();
				await TryUpdateModelAsync(obj);
				switch (action)
				{
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = " Tên Danh mục sản phẩm không được để trống";
								return Ok(msg);
							}

							bool check = await WebsiteModuleProductDa.CheckModuleProduct(obj.Name);
							if (check)
							{
								msg.Message = "Danh mục sản phẩm đã tồn tại";
								return Ok(msg);
							}

							var rs = await BaseDa.Insert(obj, "WebsiteModuleProducts");
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
							var objOld = await WebsiteModuleProductDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên Danh mục sản phẩm không được để trống";
								return Ok(msg);
							}
							objOld.Name = obj.Name;
							objOld.NameAscii = obj.NameAscii;
							objOld.Title = obj.Title;
							objOld.OrderDisplay = obj.OrderDisplay;
							objOld.UrlPicture = obj.UrlPicture;
							objOld.LinkUrl = obj.LinkUrl;
							//objOld.ModuleIds = obj.ModuleIds;


							int rs = await BaseDa.Update(objOld, "WebsiteModuleProducts");
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
							int rs = await BaseDa.Delete(obj.Id, "WebsiteModuleProducts");
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
								int rs = await BaseDa.Delete(item, "WebsiteModuleProducts");

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
