using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaClienteFicha")]
    public class CadastroConfiguracaoPessoaClienteFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var cliente = ViewData["Cliente"] as Pessoa ?? new Pessoa();
            ViewData["TituloCliente"] = cliente.Id == 0 ? "Novo cliente" : "Modificar cliente";
            return View(cliente);
        }
    }
}
