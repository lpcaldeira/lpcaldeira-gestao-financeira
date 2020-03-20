using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoPrevisaoFinanceiraSemana")]
    public class DashboardResumoPrevisaoFinanceiraSemana : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoPrevisaoFinanceiraSemana(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterPrevisaoFinanceiraDaSemana();
            return View(resultado);
        }
    }
}
