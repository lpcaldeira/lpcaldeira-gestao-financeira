using Gomi.Infraestrutura.Models.ControleAcesso;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IUsuarioService
    {
        void AdicionarUsuarioSuporte();
        Usuario ObterUsuarioLogado();
        Task<IEnumerable<Usuario>> ObterUsuarios();
        void Salvar(Usuario usuario);
        void Autenticar(string idUSuario, string senha);
        void Sair();
    }
}
