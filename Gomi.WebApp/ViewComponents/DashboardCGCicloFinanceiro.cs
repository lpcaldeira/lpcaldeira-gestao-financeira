using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGCicloFinanceiro")]
    public class DashboardCGCicloFinanceiro : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGCicloFinanceiro(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterCicloFinanceiro();
            return View(resultado);
        }
    }
}
