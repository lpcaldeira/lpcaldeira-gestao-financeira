using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "RelatorioRelDREDetalhado")]
    public class RelatorioRelDREDetalhado : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioRelDREDetalhado(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterRelDREDetalhado();
            return View(resultado);
           
        }
    }
}
