using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Admin;
using Newtonsoft.Json;

namespace project_mvc.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductDa ProductDa;
		private readonly WebsiteModuleProductDa WebsiteModuleProductDa;
		private readonly TradeMarkDa TradeMarkDa;
		private readonly BaseDa BaseDa;


		public ProductController()
        {
            ProductDa = new ProductDa(WebConfig.ConnectionString!);
			WebsiteModuleProductDa = new WebsiteModuleProductDa(WebConfig.ConnectionString!);
			TradeMarkDa = new TradeMarkDa(WebConfig.ConnectionString!);
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
            ProductViewModel model = new()
            {
                ListItems = await ProductDa.ListSearch(search, search.Page, pageSize),
                
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
			SessionBase session = new(HttpContext);
			var username = session.GetAdminUserName();
			var userId = session.GetAdminUserId();
			ProductViewModel model = new()
            {
                Item = new(),
				ListModuleProduct = await WebsiteModuleProductDa.GetAll(),
				ListTradeMark = await TradeMarkDa.GetAll()

			};
            if (id > 0)
            {
                model.Item = await ProductDa.GetId(id);
                action = "Edit";
				Products Products = new();
				model.Products = Products;

				if (!string.IsNullOrEmpty(Products.AlbumPictureJson))
				{
					model.ListAlbumGalleryAdmin = JsonConvert.DeserializeObject<List<AlbumGalleryAdmin>>(Products.AlbumPictureJson);
				}
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
                Products obj = new();
				List<AlbumGalleryAdmin> albumGalleryItemList = new();
				AlbumGalleryAdmin albumGalleryItem = new();
				string? album = string.Empty;
				await TryUpdateModelAsync(obj);
				SessionBase session = new(HttpContext);
				var username = session.GetAdminUserName();
				var userId = session.GetAdminUserId();
				switch (action)
                {
					case StaticEnum.Add:
						{
							if (string.IsNullOrEmpty(obj.Name))
							{
								msg.Message = "Tên sản phẩm không được để trống";
								return Ok(msg);
							}

							bool check = await ProductDa.CheckProduct(obj.Name);
							if (check)
							{
								msg.Message = "Tên sản phẩm đã tồn tại";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Price.ToString()))
							{
								msg.Message = "Giá sản phẩm không được để trống";
								return Ok(msg);
							}

							// Chuyển đổi giá trị Price từ string sang decimal
							decimal price;
							if (!decimal.TryParse(obj.Price.ToString(), out price))
							{
								msg.Message = "Giá sản phẩm không hợp lệ";
								return Ok(msg);
							}

							if (price <= 0)
							{
								msg.Message = "Giá sản phẩm phải lớn hơn 0";
								return Ok(msg);
							}

							// Gán giá trị đã xử lý
							obj.Price = price;

							// Chuyển đổi giá trị Price từ string sang decimal
							decimal priceOld;
							if (!decimal.TryParse(obj.PriceOld.ToString(), out priceOld))
							{
								msg.Message = "Giá sản phẩm không hợp lệ";
								return Ok(msg);
							}

							if (price <= 0)
							{
								msg.Message = "Giá sản phẩm phải lớn hơn 0";
								return Ok(msg);
							}

							// Gán giá trị đã xử lý
							obj.PriceOld = priceOld;

							obj.CreatorName = username ?? "System";
							obj.CreatorID = Convert.ToInt32(userId != null ? userId : 0);

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
							obj.AlbumPictureJson = Utility.RemoveHTMLTag(album!);
							#endregion

							var rs = await BaseDa.Insert(obj, "Products");
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
                            var objOld = await ProductDa.GetId(obj.Id);
                            if (objOld == null)
                            {
                                msg.Message = "Không tìm thấy dữ liệu";
                                return Ok(msg);
                            }
                            if (string.IsNullOrEmpty(obj.Name))
                            {
                                msg.Message = "Tên sản phẩm không được để trống";
                                return Ok(msg);
                            }
                            objOld.Name = obj.Name;
                            objOld.NameAscii = obj.NameAscii;
                            objOld.Price = obj.Price;
                            objOld.ProductCode = obj.ProductCode;
                            objOld.CreatedDate = obj.CreatedDate;
                            objOld.PublishDate = obj.PublishDate;
                            objOld.Content = obj.Content;
                            objOld.Description = obj.Description;
                            objOld.UrlPicture = obj.UrlPicture;
                            objOld.ModuleIds = obj.ModuleIds;

							int rs = await BaseDa.Update(objOld, "Products");
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
                            int rs = await BaseDa.Delete(obj.Id, "Products");
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
                                int rs = await BaseDa.Delete(item, "Products");

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

