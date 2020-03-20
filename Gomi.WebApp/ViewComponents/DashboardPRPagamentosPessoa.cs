using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRPagamentosPessoa")]
    public class DashboardPRPagamentosPessoa : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRPagamentosPessoa(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterTotalEmAbertoDeContasPagarNoPeriodoDetalhadoPessoa();
            return View(resultado);
           
        }
    }
}
