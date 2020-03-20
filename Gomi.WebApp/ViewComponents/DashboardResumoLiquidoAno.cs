using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoLiquidoAno")]
    public class DashboardResumoLiquidoAno : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoLiquidoAno(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterResultadoFinanceiroLiquidoAcumuladoAno();
            return View(resultado);
        }
    }
}
