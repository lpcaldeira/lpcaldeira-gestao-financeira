using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoSaldoDisponibilidadeSoma")]
    public class DashboardResumoSaldoDisponibilidadeSoma : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoSaldoDisponibilidadeSoma(IDashboardService dashboardService)
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
