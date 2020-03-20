using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelRelacaoPagamentos")]
    public class RelatorioRelRelacaoPagamentos : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelRelacaoPagamentos(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelRelacaoPagamentos();
            return View(resultado);
           
        }
    }
}
