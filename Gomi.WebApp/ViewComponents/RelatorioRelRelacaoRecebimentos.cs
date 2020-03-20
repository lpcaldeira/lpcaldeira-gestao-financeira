using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelRelacaoRecebimentos")]
    public class RelatorioRelRelacaoRecebimentos : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelRelacaoRecebimentos(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelRelacaoRecebimentos();
            return View(resultado);
           
        }
    }
}
