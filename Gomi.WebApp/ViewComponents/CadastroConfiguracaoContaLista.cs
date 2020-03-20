using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoContaLista")]
    public class CadastroConfiguracaoContaLista : ViewComponent
    {
        private readonly IContaService _contaService;

        public CadastroConfiguracaoContaLista(IContaService contaService)
        {
            _contaService = contaService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var resultado = await _contaService.ObterContas();
            return View(resultado);
        }
    }
}
