using Gomi.Infraestrutura.Models.Financeiro;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPlanoContaFicha")]
    public class CadastroConfiguracaoPlanoContaFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var planoConta = ViewData["PlanoConta"] as PlanoConta ?? new PlanoConta();
            ViewData["TituloPlanoConta"] = planoConta.Id == 0 ? "Nova categoria" : "Modificar categoria";
            return View(planoConta);
        }
    }
}
