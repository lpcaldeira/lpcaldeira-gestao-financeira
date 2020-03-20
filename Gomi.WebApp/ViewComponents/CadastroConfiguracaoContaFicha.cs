using Gomi.Infraestrutura.Models.Financeiro;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoContaFicha")]
    public class CadastroConfiguracaoContaFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var conta = ViewData["Conta"] as Conta ?? new Conta();
            ViewData["TituloConta"] = conta.Id == 0 ? "Nova conta" : "Modificar conta";
            return View(conta);
        }
    }
}
