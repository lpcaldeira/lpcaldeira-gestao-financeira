using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoSaldoDisponibilidade")]
    public class DashboardResumoSaldoDisponibilidade : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoSaldoDisponibilidade(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterSaldosDisponibilidade();
            return View(resultado);
           
        }
    }
}
