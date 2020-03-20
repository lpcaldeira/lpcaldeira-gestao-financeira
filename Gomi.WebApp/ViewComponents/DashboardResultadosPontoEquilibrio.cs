using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosPontoEquilibrio")]
    public class DashboardResultadosPontoEquilibrio : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosPontoEquilibrio(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterPontoEquilibrioFinanceiro();
            return View(resultado);
        }
    }
}
