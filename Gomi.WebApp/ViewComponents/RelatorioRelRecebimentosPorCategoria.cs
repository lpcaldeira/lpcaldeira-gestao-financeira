using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelRecebimentosPorCategoria")]
    public class RelatorioRelRecebimentosPorCategoria : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelRecebimentosPorCategoria(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelRecebimentosPorCategoria();
            return View(resultado);
           
        }
    }
}
