using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGPrazoMedioPagamento")]
    public class DashboardCGPrazoMedioPagamento : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGPrazoMedioPagamento(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterPrazoMedioPagamento();
            return View(resultado);
        }
    }
}
