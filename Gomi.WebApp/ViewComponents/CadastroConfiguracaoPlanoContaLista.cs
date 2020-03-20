using System.Linq;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPlanoContaLista")]
    public class CadastroConfiguracaoPlanoContaLista : ViewComponent
    {
        private readonly IPlanoContaService _planoContaService;

        public CadastroConfiguracaoPlanoContaLista(IPlanoContaService planoContaService)
        {
            _planoContaService = planoContaService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var resultado = await _planoContaService.ObterPlanosContas();
            return View(resultado.OrderBy(x => x.Categoria));
        }
    }
}
