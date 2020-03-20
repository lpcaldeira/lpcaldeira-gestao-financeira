using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoResultadoPrevistoParaMesReceber")]
    public class DashboardResumoResultadoPrevistoParaMesReceber : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoResultadoPrevistoParaMesReceber(IDashboardService dashboardService)
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
