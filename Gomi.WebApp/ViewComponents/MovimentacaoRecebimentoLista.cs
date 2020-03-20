using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "MovimentacaoRecebimentoLista")]
    public class MovimentacaoRecebimentoLista : ViewComponent
    {
        private readonly IReceberService _receberService;

        public MovimentacaoRecebimentoLista(IReceberService receberService)
        {
            _receberService = receberService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var resultado = await _receberService.ObterRecebimentos();
            return View(resultado);
        }

    }
}