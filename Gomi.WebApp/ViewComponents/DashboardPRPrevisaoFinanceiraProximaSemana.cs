using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRPrevisaoFinanceiraProximaSemana")]
    public class DashboardPRPrevisaoFinanceiraProximaSemana : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRPrevisaoFinanceiraProximaSemana(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterPrevisaoFinanceiraProximaSemana();
            return View(resultado);
        }
    }
}
