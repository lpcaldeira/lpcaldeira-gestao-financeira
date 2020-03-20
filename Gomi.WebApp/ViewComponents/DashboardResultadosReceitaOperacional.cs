using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosReceitaOperacional")]
    public class DashboardResultadosReceitaOperacional : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosReceitaOperacional(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterReceitaOperacional();
            return View(resultado);
           
        }
    }
}
