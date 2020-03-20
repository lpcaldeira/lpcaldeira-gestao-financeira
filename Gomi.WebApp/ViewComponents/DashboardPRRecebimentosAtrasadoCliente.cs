using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRRecebimentosAtrasadoCliente")]
    public class DashboardPRRecebimentosAtrasadoCliente : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRRecebimentosAtrasadoCliente(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterTotalAtrasadoDeContasReceberNoPeriodoCliente();
            return View(resultado);
           
        }
    }
}
