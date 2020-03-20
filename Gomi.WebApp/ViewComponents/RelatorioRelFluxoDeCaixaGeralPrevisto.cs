using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelFluxoDeCaixaGeralPrevisto")]
    public class RelatorioRelFluxoDeCaixaGeralPrevisto : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelFluxoDeCaixaGeralPrevisto(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelFluxoDeCaixaGeralPrevisto();
            return View(resultado);
           
        }
    }
}
