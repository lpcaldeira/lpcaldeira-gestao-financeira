using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardResumoBrutoMesAtual")]
    public class DashboardResumoBrutoMesAtual : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardResumoBrutoMesAtual(IDashboardService dashboardService)
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
