using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoCentroCustoLista")]
    public class CadastroConfiguracaoCentroCustoLista : ViewComponent
    {
        private readonly ICentroCustoService _centroCustoService;

        public CadastroConfiguracaoCentroCustoLista(ICentroCustoService centroCustoService)
        {
            _centroCustoService = centroCustoService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var resultado = await _centroCustoService.ObterCentrosCustos();
            return View(resultado);
        }
    }
}
