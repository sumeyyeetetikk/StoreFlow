using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.LayoutComponents
{
    public class _LayoutFooterComponentpartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}