using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGProjecaoFluxoCaixa")]
    public class DashboardCGProjecaoFluxoCaixa : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGProjecaoFluxoCaixa(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterProjecaoFluxoCaixa();
            return View(resultado);
        }
    }
}
