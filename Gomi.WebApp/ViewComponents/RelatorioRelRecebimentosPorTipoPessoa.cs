using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelRecebimentosPorTipoPessoa")]
    public class RelatorioRelRecebimentosPorTipoPessoa : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelRecebimentosPorTipoPessoa(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelRecebimentosPorTipoPessoa();
            return View(resultado);
           
        }
    }
}
