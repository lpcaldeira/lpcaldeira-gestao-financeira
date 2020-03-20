using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaFuncionarioFicha")]
    public class CadastroConfiguracaoPessoaFuncionarioFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var funcionario = ViewData["Funcionario"] as Pessoa ?? new Pessoa();
            ViewData["TituloFuncionario"] = funcionario.Id == 0 ? "Novo funcionário" : "Modificar funcionário";
            return View(funcionario);
        }
    }
}
