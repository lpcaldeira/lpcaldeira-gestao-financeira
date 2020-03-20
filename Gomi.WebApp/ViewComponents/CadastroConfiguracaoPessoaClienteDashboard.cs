using System.Linq;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaClienteDashboard")]
    public class CadastroConfiguracaoPessoaClienteDashboard : ViewComponent
    {
        private readonly IPessoaService _pessoaService;

        public CadastroConfiguracaoPessoaClienteDashboard(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }
        
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _pessoaService.ObterListaPessoas();
            return View(resultado);

        }
        
    }
}
