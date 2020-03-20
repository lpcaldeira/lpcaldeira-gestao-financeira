using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoDistribuicaoResultados")]
    public class DashboardResumoDistribuicaoResultados : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoDistribuicaoResultados(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterDistribuicaoResultados();
            return View(resultado);
           
        }
    }
}
