using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRPrevisaoFinanceira")]
    public class DashboardPRPrevisaoFinanceira : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRPrevisaoFinanceira(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterPrevisaoFinanceiraDeHoje();
            return View(resultado);
        }
    }
}
