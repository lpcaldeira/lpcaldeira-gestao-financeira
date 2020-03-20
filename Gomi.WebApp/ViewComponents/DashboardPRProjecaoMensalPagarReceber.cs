using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRProjecaoMensalPagarReceber")]
    public class DashboardPRProjecaoMensalPagarReceber : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRProjecaoMensalPagarReceber(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterProjecaoMensalDeContasPagarReceberNoPeriodo();
            return View(resultado);
           
        }
    }
}
