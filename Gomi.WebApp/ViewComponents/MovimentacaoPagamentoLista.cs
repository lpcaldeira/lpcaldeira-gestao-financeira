using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "MovimentacaoPagamentoLista")]
    public class MovimentacaoPagamentoLista : ViewComponent
    {
        private readonly IPagarService _pagarService;

        public MovimentacaoPagamentoLista(IPagarService pagarService)
        {
            _pagarService = pagarService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var resultado = await _pagarService.ObterPagamentos();
            return View(resultado);
        }

    }
}