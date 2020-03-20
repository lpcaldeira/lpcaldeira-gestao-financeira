using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "MovimentacaoResumo")]
    public class MovimentacaoResumo : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            return View();
        }
    }
}