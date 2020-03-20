using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelPagamentosPorDia")]
    public class RelatorioRelPagamentosPorDia : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelPagamentosPorDia(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelPagamentosPorDia();
            return View(resultado);
           
        }
    }
}
