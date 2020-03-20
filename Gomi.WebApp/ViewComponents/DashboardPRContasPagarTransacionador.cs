using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRContasPagarTransacionador")]
    public class DashboardPRContasPagarTransacionador : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRContasPagarTransacionador(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterContasPagarTransacionadores();
            return View(resultado);
           
        }
    }
}
