using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaFinanciadorFicha")]
    public class CadastroConfiguracaoPessoaFinanciadorFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var financiador = ViewData["Financiador"] as Pessoa ?? new Pessoa();
            ViewData["TituloFinanciador"] = financiador.Id == 0 ? "Novo financiador" : "Modificar financiador";
            return View(financiador);
        }
    }
}
