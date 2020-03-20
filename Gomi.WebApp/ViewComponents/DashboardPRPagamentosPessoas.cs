using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRPagamentosPessoas")]
    public class DashboardPRPagamentosPessoas : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRPagamentosPessoas(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterTotalEmAbertoDeContasPagarNoPeriodoPessoa();
            return View(resultado);
           
        }
    }
}
