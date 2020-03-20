using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoResultadoLiquido")]
    public class DashboardResumoResultadoLiquido : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoResultadoLiquido(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterResultadoLiquido();
            return View(resultado);
           
        }
    }
}
