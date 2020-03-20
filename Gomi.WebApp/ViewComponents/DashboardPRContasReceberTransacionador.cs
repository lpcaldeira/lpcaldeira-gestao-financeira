using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "DashboardPRContasReceberTransacionador")]
    public class DashboardPRContasReceberTransacionador : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public DashboardPRContasReceberTransacionador(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService; 
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _dashboardService.ObterContasReceberTransacionadores();
            return View(resultado);
           
        }
    }
}
