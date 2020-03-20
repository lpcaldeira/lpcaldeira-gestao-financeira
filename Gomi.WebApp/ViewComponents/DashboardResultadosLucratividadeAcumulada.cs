using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResultadosLucratividadeAcumulada")]
    public class DashboardResultadosLucratividadeAcumulada : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResultadosLucratividadeAcumulada(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterAnaliseFinanceira();
            return View(resultado);
           
        }
    }
}
