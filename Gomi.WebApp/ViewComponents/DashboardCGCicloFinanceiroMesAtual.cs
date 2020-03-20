using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGCicloFinanceiroMesAtual")]
    public class DashboardCGCicloFinanceiroMesAtual : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGCicloFinanceiroMesAtual(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterCicloFinanceiroMesAtual();
            return View(resultado);
        }
    }
}
