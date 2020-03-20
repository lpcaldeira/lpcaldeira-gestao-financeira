using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelDRE")]
    public class RelatorioRelDRE : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelDRE(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelDRE();
            return View(resultado);
           
        }
    }
}
