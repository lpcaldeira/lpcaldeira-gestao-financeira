using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosAnaliseFinanceiraGrid")]
    public class DashboardResultadosAnaliseFinanceiraGrid : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosAnaliseFinanceiraGrid(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterAnaliseFinanceira();
            return View(resultado);
           
        }
    }
}
