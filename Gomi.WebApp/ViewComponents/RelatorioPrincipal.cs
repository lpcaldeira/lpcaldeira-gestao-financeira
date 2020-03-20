using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioPrincipal")]
    public class RelatorioPrincipal : ViewComponent
    {
       

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
       
            return View();
        }
    }
}