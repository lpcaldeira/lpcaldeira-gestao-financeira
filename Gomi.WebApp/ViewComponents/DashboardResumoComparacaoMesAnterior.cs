using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoComparacaoMesAnterior")]
    public class DashboardResumoComparacaoMesAnterior : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoComparacaoMesAnterior(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterComparacaoMesAnterior();
            return View(resultado);
        }
    }
}
