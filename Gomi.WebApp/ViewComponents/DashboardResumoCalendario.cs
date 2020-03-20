using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoCalendario")]
    public class DashboardResumoCalendario : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoCalendario(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = 0;
            return View(resultado);
           
        }
    }
}
