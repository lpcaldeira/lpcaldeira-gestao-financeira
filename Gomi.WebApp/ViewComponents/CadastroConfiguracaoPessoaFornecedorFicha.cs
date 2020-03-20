using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaFornecedorFicha")]
    public class CadastroConfiguracaoPessoaFornecedorFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var fornecedor = ViewData["Fornecedor"] as Pessoa ?? new Pessoa();
            ViewData["TituloFornecedor"] = fornecedor.Id == 0 ? "Novo fornecedor" : "Modificar fornecedor";
            return View(fornecedor);
        }
    }
}
