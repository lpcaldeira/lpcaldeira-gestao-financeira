using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGPrazoMedioRecebimentosMesAtual")]
    public class DashboardCGPrazoMedioRecebimentosMesAtual : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGPrazoMedioRecebimentosMesAtual(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterPrazoMedioRecebimentoMesAtual();
            return View(resultado);
        }
    }
}
