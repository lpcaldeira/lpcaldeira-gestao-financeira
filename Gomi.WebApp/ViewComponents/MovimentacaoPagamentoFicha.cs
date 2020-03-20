using Gomi.Infraestrutura.Models.Financeiro;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "MovimentacaoPagamentoFicha")]
    public class MovimentacaoPagamentoFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var pagamento = ViewData["Pagamento"] as Pagar ?? new Pagar();

            if (pagamento.Id == 0)
                ViewData["TituloPagamento"] = "Novo Pagamento";
            else if (pagamento.Id != 0 && pagamento.ValorPago > 0)
                ViewData["TituloPagamento"] = "Informações do Pagamento";
            else
                ViewData["TituloPagamento"] = "Modificar Pagamento";

            return View(pagamento);
        }
    }
}