using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosFluxoCaixa")]
    public class DashboardResultadosFluxoCaixa : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosFluxoCaixa(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterFluxoDeResultados();
            return View(resultado);
        }
    }
}
