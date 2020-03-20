using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoResultadoOperacional")]
    public class DashboardResumoResultadoOperacional : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoResultadoOperacional(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterResultadoOperacional();
            return View(resultado);
           
        }
    }
}
