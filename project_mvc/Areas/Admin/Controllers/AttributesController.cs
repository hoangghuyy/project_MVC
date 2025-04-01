using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;

namespace project_mvc.Areas.Admin.Controllers
{
	public class AttributesController : BaseController
	{
		private readonly AttributesDa AttributesDa;
		private readonly BaseDa BaseDa;
		


		public AttributesController()
		{
			AttributesDa = new AttributesDa(WebConfig.ConnectionString!);
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
			AttributesViewModel model = new()
			{
				ListItems = await AttributesDa.ListSearch(search, search.Page, pageSize, false),
				
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
			AttributesViewModel model = new()
			{
				Item = new(),
				ListItems = await AttributesDa.GetAll(id),

			};
			if (id > 0)
			{
				model.Item = await AttributesDa.GetId(id);
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
				Attributes obj = new();
				await TryUpdateModelAsync(obj);
				switch (action)
				{
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên thuộc tính không được để trống";
								return Ok(msg);
							}
							
							bool check = await AttributesDa.CheckName(obj.Name);
							if (check)
							{
								msg.Message = "Tên thuộc tính đã tồn tại";
								return Ok(msg);
							}
							
							var rs = await BaseDa.Insert(obj, "Attributes");
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
							var objOld = await AttributesDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên thuộc tính không được để trống";
								return Ok(msg);
							}
							
							objOld.Name = obj.Name;
							objOld.NameAscii = obj.NameAscii;
							objOld.UrlPicture = obj.UrlPicture;


							int rs = await BaseDa.Update(objOld, "Attributes");
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
							int rs = await BaseDa.Delete(obj.Id, "Attributes");
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
								int rs = await BaseDa.Delete(item, "Attributes");

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
