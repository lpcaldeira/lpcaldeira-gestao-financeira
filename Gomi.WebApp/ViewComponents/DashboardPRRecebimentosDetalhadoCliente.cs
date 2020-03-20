using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRRecebimentosDetalhadoCliente")]
    public class DashboardPRRecebimentosDetalhadoCliente : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRRecebimentosDetalhadoCliente(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterTotalEmAbertoDeContasReceberNoPeriodoDetalhadoCliente();
            return View(resultado);
           
        }
    }
}
