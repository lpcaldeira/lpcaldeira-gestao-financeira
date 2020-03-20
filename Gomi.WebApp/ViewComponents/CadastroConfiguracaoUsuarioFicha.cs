using Gomi.Infraestrutura.Models.ControleAcesso;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoUsuarioFicha")]
    public class CadastroConfiguracaoUsuarioFicha : ViewComponent
    {
        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var usuario = ViewData["Usuario"] as Usuario ?? new Usuario();
            ViewData["TituloUsuario"] = usuario.Id == 0 ? "Novo usuário" : "Modificar usuário";
            return View(usuario);
        }
    }
}
