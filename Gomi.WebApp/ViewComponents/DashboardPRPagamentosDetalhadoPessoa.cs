using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRPagamentosDetalhadoPessoa")]
    public class DashboardPRPagamentosDetalhadoPessoa : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRPagamentosDetalhadoPessoa(IDashboardService dashboardService)
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
