using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoEmpresaFicha")]
    public class CadastroConfiguracaoEmpresaFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var empresa = ViewData["Empresa"] as Empresa ?? new Empresa();
            ViewData["TituloEmpresa"] = empresa.Id == 0 ? "Nova empresa" : "Modificar empresa";
            return View(empresa);
        }
    }
}
