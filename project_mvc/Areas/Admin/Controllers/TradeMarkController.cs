using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;

namespace project_mvc.Areas.Admin.Controllers
{
	public class TradeMarkController : BaseController
	{
		private readonly TradeMarkDa TradeMarkDa;
		private readonly ModuleTypeDa ModuleTypeDa;
		private readonly BaseDa BaseDa;



		public TradeMarkController()
		{
			TradeMarkDa = new TradeMarkDa(WebConfig.ConnectionString!);
			ModuleTypeDa = new ModuleTypeDa(WebConfig.ConnectionString!);
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
			int pageSize = 10;
			TradeMarkViewModel model = new()
			{
				ListItems = await TradeMarkDa.ListSearch(search, search.Page, pageSize),
				ListModuleType = await ModuleTypeDa.GetAll(),
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
			TradeMarkViewModel model = new()
			{
				Item = new(),
				ListItems = await TradeMarkDa.GetAllById(id),
				ListModuleType = await ModuleTypeDa.GetAll(),

			};
			if (id > 0)
			{
				model.Item = await TradeMarkDa.GetId(id);
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
				TradeMarks obj = new();
				await TryUpdateModelAsync(obj);
				switch (action)
				{
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = " Tên Module không được để trống";
								return Ok(msg);
							}

							bool check = await TradeMarkDa.CheckTradeMark(obj.Name);
							if (check)
							{
								msg.Message = "Danh mục sản phẩm đã tồn tại";
								return Ok(msg);
							}

							var rs = await BaseDa.Insert(obj, "TradeMarks");
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
							var objOld = await TradeMarkDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên Module không được để trống";
								return Ok(msg);
							}
							objOld.Name = obj.Name;
							objOld.UrlPicture = obj.UrlPicture;
							objOld.ModuleTypeCode = obj.ModuleTypeCode;


							int rs = await BaseDa.Update(objOld, "TradeMarks");
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
							int rs = await BaseDa.Delete(obj.Id, "TradeMarks");
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
								int rs = await BaseDa.Delete(item, "TradeMarks");

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
