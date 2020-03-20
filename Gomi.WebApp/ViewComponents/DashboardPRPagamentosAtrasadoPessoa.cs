using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRPagamentosAtrasadoPessoa")]
    public class DashboardPRPagamentosAtrasadoPessoa : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRPagamentosAtrasadoPessoa(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterTotalAtrasadoDeContasPagarNoPeriodoPessoa();
            return View(resultado);
           
        }
    }
}
