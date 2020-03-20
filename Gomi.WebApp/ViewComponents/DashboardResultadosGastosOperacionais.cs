using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosGastosOperacionais")]
    public class DashboardResultadosGastosOperacionais : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosGastosOperacionais(IDashboardService dashboardService)
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
