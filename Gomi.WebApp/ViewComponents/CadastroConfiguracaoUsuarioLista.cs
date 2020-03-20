using System.Linq;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.ControleAcesso;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "CadastroConfiguracaoUsuarioLista")]
    public class CadastroConfiguracaoUsuarioLista : ViewComponent
    {
        private readonly IUsuarioService _usuarioService;

        public CadastroConfiguracaoUsuarioLista(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var resultado = await Task.Factory.StartNew(Enumerable.Empty<Usuario>);// _usuarioService.ObterUsuarios();
            return View(resultado);
        }
    }
}
