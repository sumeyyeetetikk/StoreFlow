using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class _RightSidevarDashboardComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
            {
                return View();
            }
      
    }
}
