using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace StoreFlow.Views.Shared.Components
{
    public class _FooterDashboardComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
