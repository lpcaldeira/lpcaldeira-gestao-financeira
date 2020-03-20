using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosGastosOperacionaisOutras")]
    public class DashboardResultadosGastosOperacionaisOutras : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosGastosOperacionaisOutras(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterGastosOperacionaisOutras();
            return View(resultado);
           
        }
    }
}
