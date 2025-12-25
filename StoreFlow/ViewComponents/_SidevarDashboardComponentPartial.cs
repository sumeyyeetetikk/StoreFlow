using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class _SidevarDashboardComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
