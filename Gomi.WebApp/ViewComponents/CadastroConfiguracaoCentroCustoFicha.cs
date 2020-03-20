using Gomi.Infraestrutura.Models.Financeiro;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoCentroCustoFicha")]
    public class CadastroConfiguracaoCentroCustoFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var centroCusto = ViewData["CentroCusto"] as CentroCusto ?? new CentroCusto();
            ViewData["TituloCentroCusto"] = centroCusto.Id == 0 ? "Novo centro de custo" : "Modificar centro de custo";
            return View(centroCusto);
        }
    }
}
