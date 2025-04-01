using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;

namespace project_mvc.Areas.Admin.Controllers
{
	public class AdvertisingController : BaseController
	{
		private readonly AdvertisingDa AdvertisingDa;
		private readonly ModulePositionDa ModulePositionDa;
		private readonly BaseDa BaseDa;


		public AdvertisingController()
		{
			AdvertisingDa = new AdvertisingDa(WebConfig.ConnectionString!);
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
			int pageSize = 5;
			AdvertisingViewModel model = new()
			{
				ListItems = await AdvertisingDa.ListSearch(search, search.Page, pageSize),

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
			AdvertisingViewModel model = new()
			{
				Item = new(),
				ListModulePosition = await ModulePositionDa.GetAll(),

			};
			if (id > 0)
			{
				model.Item = await AdvertisingDa.GetId(id);
				action = "Edit";
			}
			ViewBag.Action = action;
			ViewBag.ActionText = Utility.ActionText(action);
			return View(model);
		}

		[Obsolete]
		public async Task<ActionResult> AjaxGridPosition(string? selected = null)
		{

			ModulePositionViewModel model = new()
			{
				ListModulePosition = await ModulePositionDa.GetAll(),
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
				Advertisings obj = new();
				await TryUpdateModelAsync(obj);
				switch (action)
				{
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.Name))   
							{
								msg.Message = "Tên banner không được để trống";
								return Ok(msg);
							}
						
							//bool check = await AdvertisingDa.CheckAdvertising(obj.Name);
							//if (check)
							//{
							//	msg.Message = "Tên banner đã tồn tại";
							//	return Ok(msg);
							//}
							
							var rs = await BaseDa.Insert(obj, "Advertisings");
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
							var objOld = await AdvertisingDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên banner không được để trống";
								return Ok(msg);
							}
							
							objOld.Name = obj.Name;
							objOld.UrlPicture = obj.UrlPicture;
							objOld.UrlPictureMobile = obj.UrlPictureMobile;
							objOld.LinkUrl = obj.LinkUrl;
							objOld.Video = obj.Video;
							objOld.Description = obj.Description;
							objOld.PositionCode = obj.PositionCode;
							objOld.ModuleIds = obj.ModuleIds;

							int rs = await BaseDa.Update(objOld, "Advertisings");
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
							int rs = await BaseDa.Delete(obj.Id, "Advertisings");
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
								int rs = await BaseDa.Delete(item, "Advertisings");

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
