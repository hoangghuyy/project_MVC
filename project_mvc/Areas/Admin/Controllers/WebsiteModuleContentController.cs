using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin;
using project_mvc.Services.Admin.Models;

namespace project_mvc.Areas.Admin.Controllers
{
    public class WebsiteModuleContentController : BaseController
    {
		private readonly WebsiteModuleContentDa WebsiteModuleContentDa;
		private readonly WebsiteModuleProductDa WebsiteModuleProductDa;
		private readonly ModuleTypeDa ModuleTypeDa;
		private readonly ModulePositionDa ModulePositionDa;
		private readonly BaseDa BaseDa;


		public WebsiteModuleContentController()
		{
			WebsiteModuleContentDa = new WebsiteModuleContentDa(WebConfig.ConnectionString!);
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
			int pageSize = 2;
			WebsiteModuleContentViewModel model = new()
			{
				ListItems = await WebsiteModuleContentDa.ListSearch(search, search.Page, pageSize, false),
				ListModuleType = await ModuleTypeDa.GetAll(),
			};
			int total = model.ListItems != null && model.ListItems.Count != 0 ? model.ListItems.FirstOrDefault()!.TotalRecords : 0;
			ViewBag.Pagging = GetPage(search.Page, total, pageSize);
			ViewBag.Keyword = search.Keyword;

			//model.Total = total;
			//model.PageSize = pageSize;
			return View(model);
		}

		[Obsolete]
		public async Task<IActionResult> AjaxForm(int id)
		{
			string action = "Add";
			WebsiteModuleContentViewModel model = new()
			{
				Item = new(),
				ListItems = await WebsiteModuleContentDa.GetAllById(id),
				ListModuleType = await ModuleTypeDa.GetAll(),
				ListModulePosition = await ModulePositionDa.GetAll(),

			};
			if (id > 0)
			{
				model.Item = await WebsiteModuleContentDa.GetId(id);
				action = "Edit";
				WebsiteModuleContents websiteModuleContents = new();
				model.WebsiteModuleContents = websiteModuleContents;

				if (!string.IsNullOrEmpty(websiteModuleContents.AlbumPictureJson))
				{
					model.ListAlbumGalleryAdmin = JsonConvert.DeserializeObject<List<AlbumGalleryAdmin>>(websiteModuleContents.AlbumPictureJson);
				}
			}
			ViewBag.Action = action;
			ViewBag.ActionText = Utility.ActionText(action);
			return View(model);
		}

		[Obsolete]
		public async Task<ActionResult> AjaxGridSelect(string? selected = null)
		{

			WebsiteModuleContentViewModel model = new()
			{
				ListModuleContent = await WebsiteModuleContentDa.GetAll(),
				//ListModuleProduct = await WebsiteModuleProductDa.GetAll(),
				selectedValue = selected
			};
			return View(model);
		}

		[Obsolete]
		public async Task<ActionResult> AjaxGridSelectContent(string? selected = null)
		{

			WebsiteModuleContentViewModel model = new()
			{
				ListModuleContent = await WebsiteModuleContentDa.GetAll(),
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
				WebsiteModuleContents obj = new();
				List<AlbumGalleryAdmin> albumGalleryItemList = new();
				AlbumGalleryAdmin albumGalleryItem = new();
				string? album = string.Empty;
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
							bool check = await WebsiteModuleContentDa.CheckModuleContent(obj.Name);
							if (check)
							{
								msg.Message = "Tên đăng nhập đã tồn tại";
								msg.Errors = true;
								return Ok(msg);
							}

							#region loadAlbumanh
							albumGalleryItemList = UpdateModelLst(albumGalleryItem, albumGalleryItemList);
							if (albumGalleryItemList != null && albumGalleryItemList.Count > 0)
							{
								albumGalleryItemList = albumGalleryItemList.OrderBy(c => c.AlbumOrderDisplay).ToList();
								album = JsonConvert.SerializeObject(albumGalleryItemList);
							}
							else
							{
								album = null;
							}
							obj.AlbumPictureJson = Utility.RemoveHTMLTag(album);
							#endregion
							var rs = await BaseDa.Insert(obj, "WebsiteModuleContents");
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
							var objOld = await WebsiteModuleContentDa.GetId(obj.Id);
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
							objOld.NameAscii = obj.NameAscii;
							objOld.LinkUrl = obj.LinkUrl;
							objOld.OrderDisplay = obj.OrderDisplay;
							objOld.Description = obj.Description;
							objOld.Content = obj.Content;
							objOld.ModuleIds = obj.ModuleIds;
							//objOld.IsShow = obj.IsShow;

							int rs = await BaseDa.Update(objOld, "WebsiteModuleContents");
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
							int rs = await BaseDa.Delete(obj.Id, "WebsiteModuleContents");
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
								int rs = await BaseDa.Delete(item, "WebsiteModuleContents");

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
