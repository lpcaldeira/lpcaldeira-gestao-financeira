using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoEmpresaLista")]
    public class CadastroConfiguracaoEmpresaLista : ViewComponent
    {
        private readonly IEmpresaService _empresaService;

        public CadastroConfiguracaoEmpresaLista(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var resultado = await _empresaService.ObterEmpresas();
            return View(resultado);
        }
    }
}