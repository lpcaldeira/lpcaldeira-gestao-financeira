using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelPagamentosPorCategoria")]
    public class RelatorioRelPagamentosPorCategoria : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelPagamentosPorCategoria(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelPagamentosPorCategoria();
            return View(resultado);
           
        }
    }
}
