using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosGastosOperacionaisGrid")]
    public class DashboardResultadosGastosOperacionaisGrid : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosGastosOperacionaisGrid(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterGastosOperacionais();
            return View(resultado);
           
        }
    }
}
