using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;

namespace project_mvc.Areas.Admin.Controllers
{
	public class ModulePositionController : BaseController
	{
		private readonly ModulePositionDa ModulePositionDa;
		private readonly WebsiteModuleProductDa WebsiteModuleProductDa;
		//private readonly ModuleTypeDa ModuleTypeDa;
		private readonly WebsiteModuleContentDa WebsiteModuleContentDa;

		private readonly BaseDa BaseDa;


		public ModulePositionController()
		{
			ModulePositionDa = new ModulePositionDa(WebConfig.ConnectionString!);
			WebsiteModuleProductDa = new WebsiteModuleProductDa(WebConfig.ConnectionString!);
			//ModuleTypeDa = new ModuleTypeDa(WebConfig.ConnectionString!);
			WebsiteModuleContentDa = new WebsiteModuleContentDa(WebConfig.ConnectionString!);

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
			ModulePositionViewModel model = new()
			{
				ListItems = await ModulePositionDa.ListSearch(search, search.Page, pageSize, false),
				//ListModuleType = await ModuleTypeDa.GetAll(),
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
			ModulePositionViewModel model = new()
			{
				Item = new(),
				ListItems = await ModulePositionDa.GetAllById(id),
				ListModuleProduct = await WebsiteModuleProductDa.GetAll(),
				ListModuleContent = await WebsiteModuleContentDa.GetAll()

			};
			if (id > 0)
			{
				model.Item = await ModulePositionDa.GetId(id);
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
				//ListModuleProduct = await WebsiteModuleProductDa.GetAll(),
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
				ModulePositions obj = new();
				await TryUpdateModelAsync(obj);
				switch (action)
				{
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên không được để trống";
								msg.Errors = true;
								return Ok(msg);
							}
							bool check = await ModulePositionDa.CheckModulePosition(obj.Name);
							if (check)
							{
								msg.Message = "Tên đã tồn tại";
								msg.Errors = true;
								return Ok(msg);
							}
							var rs = await BaseDa.Insert(obj, "ModulePositions");
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
							var objOld = await ModulePositionDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên không được để trống";
								return Ok(msg);
							}

							objOld.Name = obj.Name;
							objOld.Code = obj.Code;
							objOld.OrderDisplay = obj.OrderDisplay;
							objOld.Description = obj.Description;
							objOld.ParentId = obj.ParentId;
							objOld.UrlPicture = obj.UrlPicture;
							objOld.ModuleContentIds = obj.ModuleContentIds;
							objOld.ModuleProductIds = obj.ModuleProductIds;
							objOld.IsShow = obj.IsShow;

							int rs = await BaseDa.Update(objOld, "ModulePositions");
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
							int rs = await BaseDa.Delete(obj.Id, "ModulePositions");
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
								int rs = await BaseDa.Delete(item, "ModulePositions");

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
