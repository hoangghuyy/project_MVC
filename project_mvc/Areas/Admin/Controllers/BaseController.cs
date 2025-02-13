using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using project_mvc.Helpers;

namespace project_mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route(WebConfig.AdminAlias + "/[controller]/[action]")]
    [CheckLoginAdmin]
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

		public List<int> ArrID
		{
			get
			{
				if (!string.IsNullOrEmpty(Request.Form["itemID"]))
				{
					List<int> ltsTemp = [];
					if (Request.Form["ItemID"].ToString().Contains(','))
					{
						string[] lst = Request.Form["ItemID"].ToString().Trim().Split(',');
						foreach (string item in lst)
						{
							ltsTemp.Add(Convert.ToInt32(item));
						}
						return ltsTemp;
					}
					if (int.TryParse(Request.Form["ItemID"].ToString(), out int idTemp))
					{
						ltsTemp.Add(idTemp);
					}
					return ltsTemp;
				}
				return [];
			}
		}

		#region phân trang
		public string GetPage(int page, int totalRecord, int rowPerPage)//truyền vào trang hiện tại, tổng số bản ghi, tổng số bản ghi /trang
		{
			string html;
			if (totalRecord > rowPerPage)
			{
				int currentPage = page > 1 ? page : 1;
				html = "  <div class=\"total\"><span>Hiển thị " + Utility.FormatNumber(totalRecord < rowPerPage ? totalRecord : (page == 0 ? rowPerPage : (page * rowPerPage > totalRecord ? totalRecord : page * rowPerPage))) + " trên " + Utility.FormatNumber(totalRecord) + "</span>  </div>" +
					"<div class=\"pagi\"><ul>" +
					"<li>Trang [Current]/[Total]</li>" +
					"<li class='[disabledPrevious]'><a href='[LinkDoublePrevious]' class='pagingData'>&laquo;</a></li>" +
					"<li class='[disabledPrevious]'><a href='[LinkPrevious]' class='pagingData'>&lsaquo;</a></li>[LinkNumber]" +
					"<li class='[disabledNext]'><a href='[LinkNext]' class='pagingData'>&rsaquo;</a></li>" +
					"<li class='[disabledNext]'><a href='[LinkDoubleNext]' class='pagingData'>&raquo;</a></li>" +
					"</ul></div>";
				html = html.Replace("[Current]", currentPage.ToString());
				int total = currentPage * rowPerPage;
				bool disabledNext = total >= totalRecord;
				bool disablePrevious = total <= rowPerPage;
				if (disablePrevious && disabledNext)
				{
					html = html.Replace("[displayView]", "displayView hidden");
				}
				if (disablePrevious)
				{
					html = html.Replace("[disabledPrevious]", "disabled hidden");
					html = html.Replace("[LinkPrevious]", "javascript:");
				}
				if (disabledNext)
				{
					html = html.Replace("[disabledNext]", "disabled hidden");
					html = html.Replace("[LinkNext]", "javascript:");
				}
				int mode = totalRecord % rowPerPage;
				int doubleNext;
				if (mode == 0)
				{
					doubleNext = totalRecord / rowPerPage;
				}
				else
				{
					doubleNext = (totalRecord / rowPerPage) + 1;
				}
				html = html.Replace("[LinkPrevious]", "#page=" + (currentPage - 1));
				html = html.Replace("[LinkNext]", "#page=" + (currentPage + 1));
				html = html.Replace("[LinkDoublePrevious]", "#page=1");
				html = html.Replace("[LinkDoubleNext]", "#page=" + doubleNext);
				string link = string.Empty;
				int begin = 1;
				int end = 9;
				if (doubleNext < end)
				{
					end = doubleNext;
				}
				else
				{
					if (currentPage >= end)
					{
						begin = currentPage - (int)Math.Round((double)end / 2, 0);
						end = currentPage + (int)Math.Round((double)end / 2, 0);
					}
					if (end >= doubleNext)
					{
						begin = doubleNext - (9 - 1);
						end = doubleNext;

					}
				}
				for (int i = begin; i <= end; i++)
				{
					link += " <li " + (i == currentPage ? "class=\"active\"" : "") + "><a class=\"pagingData\" href=\"#page=" + i + "\">" + i + "</a></li>";
				}
				html = html.Replace("[Total]", end.ToString());
				html = html.Replace("[LinkNumber]", link);
			}
			else
			{
				html = "<span>Hiển thị " + Utility.FormatNumber(totalRecord < 50 ? totalRecord : (page == 0 ? 50 : page * 50)) + " trên " + Utility.FormatNumber(totalRecord) + "</span>";
			}
			return html;

		}
		public string GetPageAjax(int page, int totalRecord, int rowPerPage)
		{
			string html;
			if (totalRecord > rowPerPage)
			{
				int currentPage = page > 1 ? page : 1;
				html = "  <div class=\"total\"><span>Hiển thị " + Utility.FormatNumber(totalRecord < rowPerPage ? totalRecord : (page == 0 ? rowPerPage : (page * rowPerPage > totalRecord ? totalRecord : page * rowPerPage))) + " trên " + Utility.FormatNumber(totalRecord) + "</span>  </div>" +
					"<div class=\"pagi\"><ul>" +
					"<li>Trang [Current]/[Total]</li>" +
					"<li class='[disabledPrevious]'><a data-page='[LinkDoublePrevious]' class='pagingData'>&laquo;</a></li>" +
					"<li class='[disabledPrevious]'><a data-page='[LinkPrevious]' class='pagingData'>&lsaquo;</a></li>[LinkNumber]" +
					"<li class='[disabledNext]'><a data-page='[LinkNext]' class='pagingData'>&rsaquo;</a></li>" +
					"<li class='[disabledNext]'><a data-page='[LinkDoubleNext]' class='pagingData'>&raquo;</a></li>" +
					"</ul></div>";
				html = html.Replace("[Current]", currentPage.ToString());
				int total = currentPage * rowPerPage;
				bool disabledNext = total >= totalRecord;
				bool disablePrevious = total <= rowPerPage;
				if (disablePrevious && disabledNext)
				{
					html = html.Replace("[displayView]", "displayView hidden");
				}
				if (disablePrevious)
				{
					html = html.Replace("[disabledPrevious]", "disabled hidden");
					html = html.Replace("[LinkPrevious]", "javascript:");
				}
				if (disabledNext)
				{
					html = html.Replace("[disabledNext]", "disabled hidden");
					html = html.Replace("[LinkNext]", "javascript:");
				}
				int mode = totalRecord % rowPerPage;
				int doubleNext;
				if (mode == 0)
				{
					doubleNext = totalRecord / rowPerPage;
				}
				else
				{
					doubleNext = (totalRecord / rowPerPage) + 1;
				}
				html = html.Replace("[LinkPrevious]", (currentPage - 1).ToString());
				html = html.Replace("[LinkNext]", (currentPage + 1).ToString());
				html = html.Replace("[LinkDoublePrevious]", "1");
				html = html.Replace("[LinkDoubleNext]", doubleNext.ToString());
				string link = string.Empty;
				int begin = 1;
				int end = 9;
				if (doubleNext < end)
				{
					end = doubleNext;
				}
				else
				{
					if (currentPage >= end)
					{
						begin = currentPage - (int)Math.Round((double)end / 2, 0);
						end = currentPage + (int)Math.Round((double)end / 2, 0);
					}
					if (end >= doubleNext)
					{
						begin = doubleNext - (9 - 1);
						end = doubleNext;

					}
				}
				for (int i = begin; i <= end; i++)
				{
					link += " <li " + (i == currentPage ? "class=\"active\"" : "") + "><a class=\"pagingData\" data-page=\"" + i + "\">" + i + "</a></li>";
				}
				html = html.Replace("[Total]", end.ToString());
				html = html.Replace("[LinkNumber]", link);
			}
			else
			{
				html = "<span>Hiển thị " + Utility.FormatNumber(totalRecord < 50 ? totalRecord : (page == 0 ? 50 : page * 50)) + " trên " + Utility.FormatNumber(totalRecord) + "</span>";
			}
			return html;

		}
		#endregion
	}
}
