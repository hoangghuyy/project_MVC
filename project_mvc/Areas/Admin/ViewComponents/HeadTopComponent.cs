using Microsoft.AspNetCore.Mvc;

namespace project_mvc.Areas.Admin.ViewComponents
{
    public class HeadTopViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}
