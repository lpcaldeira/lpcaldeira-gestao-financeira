using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelFluxoDeCaixaGeralRealizado")]
    public class RelatorioRelFluxoDeCaixaGeralRealizado : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelFluxoDeCaixaGeralRealizado(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelFluxoDeCaixaGeralRealizado();
            return View(resultado);
           
        }
    }
}
