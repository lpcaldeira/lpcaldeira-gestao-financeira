using System.Linq;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Enums;
using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaFornecedorLista")]
    public class CadastroConfiguracaoPessoaFornecedorLista : ViewComponent
    {
        private readonly IPessoaService _pessoaService;

        public CadastroConfiguracaoPessoaFornecedorLista(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _pessoaService.ObterListaPessoasDetalhado();
            return View(resultado);

        }
    }
}
