using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelRecebimentosPorDia")]
    public class RelatorioRelRecebimentosPorDia : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelRecebimentosPorDia(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelRecebimentosPorDia();
            return View(resultado);
           
        }
    }
}
