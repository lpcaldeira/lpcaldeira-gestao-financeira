using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoResultadoLiquidoAno")]
    public class DashboardResumoResultadoLiquidoAno : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoResultadoLiquidoAno(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterResultadoLiquidoAno();
            return View(resultado);
           
        }
    }
}
