using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRProjecaoMensalPagarReceberGrid")]
    public class DashboardPRProjecaoMensalPagarReceberGrid : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRProjecaoMensalPagarReceberGrid(IDashboardService dashboardService)
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
