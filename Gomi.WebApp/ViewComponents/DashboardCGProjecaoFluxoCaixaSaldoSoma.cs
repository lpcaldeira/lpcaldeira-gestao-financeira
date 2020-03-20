using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGProjecaoFluxoCaixaSaldoSoma")]
    public class DashboardCGProjecaoFluxoCaixaSaldoSoma : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGProjecaoFluxoCaixaSaldoSoma(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterSaldoDisponibilidadeSoma();
            return View(resultado);
           
        }
    }
}
