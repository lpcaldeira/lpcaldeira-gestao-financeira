using Gomi.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Models.ControleAcesso;

namespace Gomi.Infraestrutura.Services
{
    public class UsuarioService : IUsuarioService
    {
       // private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(//IHttpContextAccessor httpContextAccessor,
            IUsuarioRepository usuarioRepository)
        {
            //_httpContextAccessor = httpContextAccessor;
            _usuarioRepository = usuarioRepository;
        }

        public void AdicionarUsuarioSuporte()
        {
            var usuarioSuporte = new Usuario();
            //_usuarioRepository.SalvarUsuario(usuarioSuporte);
        }

        public Usuario ObterUsuarioLogado()
        {
            return new Usuario { NomeAcesso = "admin", Senha = "admin" };
            //var idUsuario = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //return string.IsNullOrEmpty(idUsuario) ? null : _usuarioRepository.ObterUsuarioPorIdUsuario(idUsuario);
        }

        public void Autenticar(string idUsuario, string senha)
        {
            var usuario = _usuarioRepository.ObterUsuarioPorIdUsuario(idUsuario);
            if (usuario == null)
                throw new Exception("Nome de usuário inválido");

            if (!HashService.Testar(senha, usuario.Senha))
                throw new Exception("Senha inválida");

            // ver https://dotnetthoughts.net/cookie-authentication-in-asp-net-5/
            //FormsAuthentication.SetAuthCookie(nomeUsuario, false);
        }

        public void Sair()
        {
            //FormsAuthentication.SignOut();
        }

        public async Task<IEnumerable<Usuario>> ObterUsuarios()
        {
            return await _usuarioRepository.GetAll();
        }

        public void Salvar(Usuario usuario)
        {
            if (usuario.Id == 0)
                _usuarioRepository.Add(usuario);
            else
                _usuarioRepository.Update(usuario);
        }
    }
}
