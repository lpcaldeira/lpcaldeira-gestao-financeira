using Gomi.Infraestrutura.Models.Financeiro;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "MovimentacaoRecebimentoFicha")]
    public class MovimentacaoRecebimentoFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var recebimento = ViewData["Recebimento"] as Receber ?? new Receber();

            if (recebimento.Id == 0)
                ViewData["TituloRecebimento"] = "Novo Recebimento";
            else if (recebimento.Id != 0 && recebimento.ValorRecebido > 0)
                ViewData["TituloRecebimento"] = "Informações do Recebimento";
            else
                ViewData["TituloRecebimento"] = "Modificar Recebimento";

            return View(recebimento);
        }
    }
}