using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGPrazoMedioRecebimento")]
    public class DashboardCGPrazoMedioRecebimento : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGPrazoMedioRecebimento(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterPrazoMedioRecebimento();
            return View(resultado);
        }
    }
}
