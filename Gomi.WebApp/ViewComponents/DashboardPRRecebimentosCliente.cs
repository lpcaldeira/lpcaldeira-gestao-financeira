using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRRecebimentosCliente")]
    public class DashboardPRRecebimentosCliente : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRRecebimentosCliente(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterTotalEmAbertoDeContasReceberNoPeriodoCliente();
            return View(resultado);
           
        }
    }
}
