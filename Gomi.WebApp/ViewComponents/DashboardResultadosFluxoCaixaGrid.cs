using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosFluxoCaixaGrid")]
    public class DashboardResultadosFluxoCaixaGrid : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosFluxoCaixaGrid(IDashboardService dashboardService)
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
