using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoContaExtrato")]
    public class CadastroConfiguracaoContaExtrato : ViewComponent
    {
        private readonly IContaService _contaService;

        public CadastroConfiguracaoContaExtrato(IContaService contaService)
        {
            _contaService = contaService;
        }
        
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _contaService.ObterContaExtrato();
            return View(resultado);
        }
    }
}
