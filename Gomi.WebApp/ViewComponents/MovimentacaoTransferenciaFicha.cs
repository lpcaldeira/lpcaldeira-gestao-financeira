using Gomi.Infraestrutura.Models.Financeiro;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "MovimentacaoTransferenciaFicha")]
    public class MovimentacaoTransferenciaFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var transferencia = ViewData["Transferencia"] as Transferencia ?? new Transferencia();
            ViewData["TituloTransferencia"] = transferencia.Id == 0 ? "Nova transferência" : "Modificar transferência";
            return View(transferencia);
        }
    }
}