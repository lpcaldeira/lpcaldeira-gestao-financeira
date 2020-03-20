using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelPagamentosPorTipoPessoa")]
    public class RelatorioRelPagamentosPorTipoPessoa : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelPagamentosPorTipoPessoa(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelPagamentosPorTipoPessoa();
            return View(resultado);
           
        }
    }
}
