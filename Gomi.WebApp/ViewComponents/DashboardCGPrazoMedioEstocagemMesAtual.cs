using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGPrazoMedioEstocagemMesAtual")]
    public class DashboardCGPrazoMedioEstocagemMesAtual : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGPrazoMedioEstocagemMesAtual(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterPrazoMedioEstocagemMesAtual();
            return View(resultado);
        }
    }
}
