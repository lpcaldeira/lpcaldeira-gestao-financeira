using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoPrevisaoFinanceiraHoje")]
    public class DashboardResumoPrevisaoFinanceiraHoje : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoPrevisaoFinanceiraHoje(IDashboardService dashboardService)
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
