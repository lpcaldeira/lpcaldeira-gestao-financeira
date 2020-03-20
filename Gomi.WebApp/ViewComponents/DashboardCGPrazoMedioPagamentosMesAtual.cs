using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardCGPrazoMedioPagamentosMesAtual")]
    public class DashboardCGPrazoMedioPagamentosMesAtual : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardCGPrazoMedioPagamentosMesAtual(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado =  _dashboardService.ObterPrazoMedioPagamentoMesAtual ();
            return View(resultado);
        }
    }
}
