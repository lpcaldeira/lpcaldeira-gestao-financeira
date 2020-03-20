using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaBancoFicha")]
    public class CadastroConfiguracaoPessoaBancoFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var banco = ViewData["Banco"] as Pessoa ?? new Pessoa();
            ViewData["TituloBanco"] = banco.Id == 0 ? "Novo banco" : "Modificar banco";
            return View(banco);
        }
    }
}
