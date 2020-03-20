using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoResultadoPrevistoParaMesPagar")]
    public class DashboardResumoResultadoPrevistoParaMesPagar: ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoResultadoPrevistoParaMesPagar(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterResultadoPrevistoParaMes();
            return View(resultado);
        }
    }
}
