using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoLiquidoMesAtual")]
    public class DashboardResumoLiquidoMesAtual : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoLiquidoMesAtual(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterResultadoFinanceiroLiquidoMesAtual();
            return View(resultado);
        }
    }
}
