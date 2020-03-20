using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoPessoaSocioFicha")]
    public class CadastroConfiguracaoPessoaSocioFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var socio = ViewData["Socio"] as Pessoa ?? new Pessoa();
            ViewData["TituloSocio"] = socio.Id == 0 ? "Novo sócio" : "Modificar sócio";
            return View(socio);
        }
    }
}
