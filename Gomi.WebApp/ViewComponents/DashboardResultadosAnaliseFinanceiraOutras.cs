using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosAnaliseFinanceiraOutras")]
    public class DashboardResultadosAnaliseFinanceiraOutras : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosAnaliseFinanceiraOutras(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterAnaliseFinanceiraOutras();
            return View(resultado);
           
        }
    }
}
