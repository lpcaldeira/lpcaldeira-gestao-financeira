using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosReceitaOperacionalGrid")]
    public class DashboardResultadosReceitaOperacionalGrid : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosReceitaOperacionalGrid(IDashboardService dashboardService)
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
