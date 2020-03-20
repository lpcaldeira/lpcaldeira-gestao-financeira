using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosReceitaOperacionalOutras")]
    public class DashboardResultadosReceitaOperacionalOutras : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosReceitaOperacionalOutras(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterReceitaOperacionalOutras();
            return View(resultado);
           
        }
    }
}
