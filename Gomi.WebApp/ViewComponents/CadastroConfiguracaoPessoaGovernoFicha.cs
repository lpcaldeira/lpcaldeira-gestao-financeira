using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaGovernoFicha")]
    public class CadastroConfiguracaoPessoaGovernoFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var governo = ViewData["Governo"] as Pessoa ?? new Pessoa();
            ViewData["TituloGoverno"] = governo.Id == 0 ? "Novo governo" : "Modificar governo";
            return View(governo);
        }
    }
}
