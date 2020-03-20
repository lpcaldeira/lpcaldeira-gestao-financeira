using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGCapitalGiroLiquido")]
    public class DashboardCGCapitalGiroLiquido : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGCapitalGiroLiquido(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterCapitalGiroLiquido();
            return View(resultado);
        }
    }
}
