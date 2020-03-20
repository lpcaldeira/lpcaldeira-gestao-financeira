using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelFluxoDeCaixa")]
    public class RelatorioRelFluxoDeCaixa : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelFluxoDeCaixa(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelFluxoDeCaixa();
            return View(resultado);
           
        }
    }
}
