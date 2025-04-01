using Microsoft.AspNetCore.Mvc;
using project_mvc.Areas.Admin.Models;
using project_mvc.Areas.Admin.ViewModels;
using project_mvc.Helpers;
using project_mvc.Services.Admin;
using project_mvc.Services.Admin.Models;
using System.Buffers.Text;

namespace project_mvc.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserDa UserDa;
        private readonly BaseDa BaseDa;
        private readonly DepartmentDa DepartmentDa;


		public UserController()
        {
            UserDa = new UserDa(WebConfig.ConnectionString!);
            BaseDa = new BaseDa(WebConfig.ConnectionString!);
			DepartmentDa = new DepartmentDa(WebConfig.ConnectionString!);

		}
        //private readonly JsonMessage msg = new()
        //{
        //	Errors = true,
        //	Message = "Không có hành động nào được thực hiện."
        //};
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
            UserAdminViewModel model = new()
            {
                ListItems = await UserDa.ListSearch(search, search.Page, pageSize),
				ListDepartment = await DepartmentDa.GetAll(),
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
            UserAdminViewModel model = new()
            {
                Item = new(),
				ListDepartment = await DepartmentDa.GetAll(),
			};
            if (id > 0)
            {
                model.Item = await UserDa.GetId(id);
                action = "Edit";
            }
            ViewBag.Action = action;
            ViewBag.ActionText = Utility.ActionText(action);
            return View(model);
        }

        //public IActionResult AjaxFormAdd()
        //{
        //	return View();
        //}

        //[Obsolete]
        //public async Task<IActionResult> AjaxFormEdit(int id)
        //{
        //	var item = await UserDa.GetId(id);
        //	if (item == null) return BadRequest(msg);
        //	UserAdmins obj = new()
        //	{
        //		Id = item.Id,
        //		Name = item.Name,
        //		UserName = item.UserName,
        //	};

        //	UserAdminViewModel model = new()
        //	{
        //		Item = obj
        //	};
        //	return View(model);
        //}

        //      [HttpPost]
        //public async Task<IActionResult> ActionAdd()
        //{
        //	UserAdmins obj = new();
        //	await TryUpdateModelAsync(obj);
        //	obj.PasswordSalt = Utility.CreateSaltKey(8);
        //	obj.Password = Utility.CreatePasswordHash(obj.Password!, obj.PasswordSalt);
        //	var rs = await BaseDa.Insert(obj, "UserAdmins");
        //	if (rs == 0)
        //	{
        //		msg.Message = "Thêm mới thất bại";
        //		return BadRequest(msg);
        //	}
        //	msg.Message = "Thêm mới thành công";
        //	msg.Errors = false;
        //	return Ok(msg);
        //}
        //[Obsolete]
        //[HttpPost]
        //public async Task<IActionResult> ActionEdit()
        //{
        //	UserAdmins obj = new();
        //	await TryUpdateModelAsync(obj);
        //	var item = await UserDa.GetId(obj.Id);
        //	if (item == null) return BadRequest(msg);
        //	item.Name = obj.Name;
        //	var rs = await BaseDa.Update(item, "UserAdmins");
        //	if (rs == 0)
        //	{
        //		msg.Message = "Cập nhật thất bại";
        //		return BadRequest(msg);
        //	}
        //	msg.Message = "Cập nhật thành công";
        //	msg.Errors = false;
        //	return Ok(msg);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ActionDelete(int id)
        //{
        //	var rs = await BaseDa.Delete(id, "UserAdmins");
        //	if (rs == 0)
        //	{
        //		msg.Message = "Xóa thất bại";
        //		return BadRequest(msg);
        //	}
        //	msg.Message = "Xóa thành công";
        //	msg.Errors = false;
        //	return Ok(msg);
        //}
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
                UserAdmins obj = new();
                await TryUpdateModelAsync(obj);
                switch (action)
                {
                    case StaticEnum.Add:
                        {
                            if (string.IsNullOrEmpty(obj.UserName))
                            {
                                msg.Message = "Tên đăng nhập không được để trống";
                                return Ok(msg);
                            }
                            if (string.IsNullOrEmpty(obj.Password))
                            {
                                msg.Message = "Mật khẩu không được để trống";
                                return Ok(msg);
                            }
                            bool check = await UserDa.CheckUser(obj.UserName);
                            if (check)
                            {
                                msg.Message = "Tên đăng nhập đã tồn tại";
                                return Ok(msg);
                            }
                            obj.PasswordSalt = Utility.CreateSaltKey(8);
                            obj.Password = Utility.CreatePasswordHash(obj.Password!, obj.PasswordSalt);
                            var rs = await BaseDa.Insert(obj, "UserAdmins");
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
							var objOld = await UserDa.GetId(obj.Id);
							if (objOld == null)
							{
								msg.Message = "Không tìm thấy dữ liệu";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.UserName))
							{
								msg.Message = "Tên đăng nhập không được để trống";
								return Ok(msg);
							}
							if (string.IsNullOrEmpty(obj.Password))
							{
								msg.Message = "Mật khẩu không được để trống";
								return Ok(msg);
							}
							objOld.Name = obj.Name;
							objOld.RoleCode = obj.RoleCode;
							objOld.DepartmentId = obj.DepartmentId;

							int rs = await BaseDa.Update(objOld, "UserAdmins");
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
							int rs = await BaseDa.Delete(obj.Id, "UserAdmins");
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
								int rs = await BaseDa.Delete(item, "UserAdmins");

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
