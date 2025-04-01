using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using project_mvc.Areas.Admin.Controllers;
using project_mvc.Dappers;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Client;
using project_mvc.ViewModels;
using System.Linq.Expressions;
using System.Reflection;

namespace project_mvc.Controllers
{
	public class ContentController : Controller
	{
		private readonly WebsiteModuleManager _webModuleManager;
		private readonly WebsiteContentManager _webContentManager;
		private readonly PositionManager _positionManager;
		private readonly ModuleProductManager _modulProductManager;
		public ContentController()
		{

			_webModuleManager = new WebsiteModuleManager(WebConfig.ConnectionString!);
			_webContentManager = new WebsiteContentManager(WebConfig.ConnectionString!);
			_positionManager = new PositionManager(WebConfig.ConnectionString!);
			_modulProductManager = new ModuleProductManager(WebConfig.ConnectionString!);
		}

		[Route("{nameAscii}")]
		[Route("{nameAscii}/{nameAsciiC?}")]
		[Route("{nameAscii}/{nameAsciiC?}/page/{p:int?}")]
		[Obsolete]
		//public async Task<IActionResult> Index(string nameAscii, string nameAsciiC, int? p)
		//{

		//	WebsiteModuleContentItem? moduleContent = !string.IsNullOrEmpty(nameAsciiC)
		//	? await _webModuleManager.GetByNameContentAscii(nameAsciiC)
		//	: await _webModuleManager.GetByNameContentAscii(nameAscii);

		//	WebsiteModuleProductItem? moduleProduct = !string.IsNullOrEmpty(nameAsciiC)
		//	? await _webModuleManager.GetByNameProductAscii(nameAsciiC)
		//	: await _webModuleManager.GetByNameProductAscii(nameAscii);

		//	if (moduleContent != null && moduleProduct != null)
		//	{
		//		return NotFound();
		//	}

		//	string moduleTypeContent = moduleContent.ModuleTypeCode!;
		//	string moduleTypeProduct = moduleProduct.ModuleTypeCode!;
		//	try
		//	{
		//		if (!string.IsNullOrEmpty(nameAscii) && !string.IsNullOrEmpty(nameAsciiC))
		//		{
		//			WebsiteContentItem content = new();
		//			SessionBase session = new(HttpContext);
		//			content = _webContentManager.GetByNameAscii(!string.IsNullOrEmpty(nameAsciiC) ? nameAsciiC : nameAscii);

		//			List<WebsiteModuleContentItem> modules = _webModuleManager.GetListByArrId(content.ModuleIds!);
		//			module = modules.Any() ? modules.FirstOrDefault() : new WebsiteModuleContentItem();
		//			module!.AlbumGalleryItems = !string.IsNullOrEmpty(module.AlbumPictureJson) ? JsonConvert.DeserializeObject<List<AlbumGalleryAdmin>>(module.AlbumPictureJson) : new List<AlbumGalleryAdmin>();
		//			moduleType = module != null ? module.ModuleTypeCode! : StaticEnum.News;
		//			SearchModel search = new()
		//			{
		//				Page = 0,
		//				Sort = 1
		//			};
		//			// chi tiet
		//			switch (moduleType)
		//			{

		//				#region Tin tức chi tiết
		//				case StaticEnum.News:
		//					{
		//						search.Sort = 1;
		//						ContentViewModel model = new()
		//						{
		//							ContentItem = content,
		//							ModuleItem = module ?? new WebsiteModuleContentItem(),
		//							//ListContentItem = await _webContentManager.GetListContent(search, 6, module != null ? module.Id : 0, "", content.Id.ToString()), //Bài viết khác
		//						};
		//						return View(@"~/Views/Detail/DetailNews.cshtml", model);
		//					}
		//					#endregion Tin tức chi tiết
		//			}
		//		}
		//		else
		//		{
		//			SearchModel search = new();
		//			await TryUpdateModelAsync(search);

		//			// module
		//			switch (moduleType)
		//			{
		//				#region Liên hệ

		//				case StaticEnum.Contact:
		//					{
		//						ModuleViewModel model = new()
		//						{
		//							ModuleContentItem = moduleContent,

		//						};
		//						return View(@"~/Views/Content/Contact.cshtml", model);
		//					}

		//				#endregion Liên hệ
		//				#region Sản phẩm

		//				case StaticEnum.Product:
		//					{
		//						ModuleViewModel model = new()
		//						{
		//							ModuleProductItem = moduleProduct,

		//						};
		//						return View(@"~/Views/Product/ListGridProduct.cshtml", model);
		//					}

		//				#endregion Sản phẩm
		//				//#region Tuyển dụng

		//				//case StaticEnum.Recuitment:
		//				//    {
		//				//        ModuleViewModels model = new()
		//				//        {
		//				//            ModuleContentItem = module,

		//				//        };
		//				//        return View(@"~/Views/Recuitment/ListRecuitment.cshtml", model);
		//				//    }

		//				//#endregion Tuyển dụng
		//				#region Giới thiệu
		//				case StaticEnum.Introduce:
		//					{
		//						ModuleViewModel model = new()
		//						{
		//							ModuleContentItem = moduleContent,
		//						};
		//						return View(@"~/Views/Content/Introduce.cshtml", model);
		//					}
		//				#endregion Giới thiệu
		//				#region Tin tức
		//				case StaticEnum.News:
		//					{
		//						ModuleViewModel model = new()
		//						{
		//							ModuleItem = module,
		//						};
		//						return View(@"~/Views/News/ListNews.cshtml", model);
		//					}
		//					#endregion Tin tức
		//			}
		//		}


		//	}
		//	catch (Exception ex)
		//	{
		//		return View(@"~/Views/Error/Error404.cshtml");
		//	}

		//	return View(@"~/Views/Error/Error404.cshtml");
		//}
		public async Task<IActionResult> Index(string nameAscii, string nameAsciiC, int? p)
		{
			// Get modules based on whether nameAsciiC is provided
			WebsiteModuleContentItem? moduleContent = !string.IsNullOrEmpty(nameAsciiC)
				? await _webModuleManager.GetByNameContentAscii(nameAsciiC)
				: await _webModuleManager.GetByNameContentAscii(nameAscii);

			WebsiteModuleProductItem? moduleProduct = !string.IsNullOrEmpty(nameAsciiC)
				? await _webModuleManager.GetByNameProductAscii(nameAsciiC)
				: await _webModuleManager.GetByNameProductAscii(nameAscii);

			if (moduleContent == null && moduleProduct == null)
			{
				return NotFound();
			}

			try
			{
				if (!string.IsNullOrEmpty(nameAscii) && !string.IsNullOrEmpty(nameAsciiC))
				{
					// Handle detail pages
					return await HandleDetailPage(nameAscii, nameAsciiC, moduleContent, moduleProduct);
				}
				else
				{
					// Handle list pages
					return await HandleListPage(moduleContent, moduleProduct);
				}
			}
			catch (Exception ex)
			{
				return View(@"~/Views/Error/Error404.cshtml");
			}
		}

		[Obsolete]
		private async Task<IActionResult> HandleDetailPage(string nameAscii, string nameAsciiC,
			WebsiteModuleContentItem? moduleContent, WebsiteModuleProductItem? moduleProduct)
		{
			WebsiteContentItem content = await _webContentManager.GetByNameAscii(!string.IsNullOrEmpty(nameAsciiC) ? nameAsciiC : nameAscii);
			if (content == null)
			{
				return NotFound();
			}

			// Determine if we're dealing with content or product module
			if (moduleContent != null)
			{
				List<WebsiteModuleContentItem> modules = _webModuleManager.GetListByArrId(content.ModuleIds!);
				var module = modules.Any() ? modules.FirstOrDefault() : new WebsiteModuleContentItem();
				module!.AlbumGalleryItems = !string.IsNullOrEmpty(module.AlbumPictureJson)
					? JsonConvert.DeserializeObject<List<AlbumGalleryAdmin>>(module.AlbumPictureJson)
					: new List<AlbumGalleryAdmin>();

				switch (moduleContent.ModuleTypeCode)
				{
					case StaticEnum.News:
						ContentViewModel newsModel = new()
						{
							ContentItem = content,
							ModuleItem = module ?? new WebsiteModuleContentItem(),
						};
						return View(@"~/Views/Detail/DetailNews.cshtml", newsModel);

						// Add other content module types here if needed
				}
			}
			else if (moduleProduct != null)
			{
				// Handle product detail pages here if needed
				// ...
			}

			return View(@"~/Views/Error/Error404.cshtml");
		}

		[Obsolete]
		private async Task<IActionResult> HandleListPage(WebsiteModuleContentItem? moduleContent,
			WebsiteModuleProductItem? moduleProduct)
		{
			SearchModel search = new();
			await TryUpdateModelAsync(search);

			// Handle content modules
			if (moduleContent != null)
			{
				switch (moduleContent.ModuleTypeCode)
				{
					case StaticEnum.Contact:
						ModuleViewModel contactModel = new()
						{
							ModuleContentItem = moduleContent,
						};
						return View(@"~/Views/Content/Contact.cshtml", contactModel);

					case StaticEnum.Introduce:
						ModuleViewModel introModel = new()
						{
							ModuleContentItem = moduleContent,
						};
						return View(@"~/Views/Content/Introduce.cshtml", introModel);

					case StaticEnum.News:
						ModuleViewModel newsModel = new()
						{
							ModuleContentItem = moduleContent,
						};
						return View(@"~/Views/News/ListNews.cshtml", newsModel);
				}
			}

			if (moduleProduct != null)
			{
				switch (moduleProduct.ModuleTypeCode)
				{
					case StaticEnum.Product:
						ModuleViewModel productModel = new()
						{
							ModuleProductItem = moduleProduct,
							ListPositionProduct = await _positionManager.GetPositionToList(StaticEnum.ModuleMain),
							//ListModuleTrademark = []

						};
						//var moduleProductList = _webModuleManager.GetByListModuleTypeCode(StaticEnum.Product);
						//productModel.ListModuleProducts = moduleProductList;
						//ListItems = await ProductDa.ListSearch(search, search.Page, pageSize)

						//productModel.ListProductItem = listProduct;

						return View(@"~/Views/Product/ListGridProduct.cshtml", productModel);
				}
			}

			return View(@"~/Views/Error/Error404.cshtml");
		}

	}
}
