using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoResultadoTrimestre")]
    public class DashboardResumoResultadoTrimestre : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoResultadoTrimestre(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterResultadoParaTrimestreRP();
            return View(resultado);
        }
    }
}
