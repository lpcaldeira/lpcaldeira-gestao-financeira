using Gomi.Infraestrutura.Models.ControleAcesso;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObterUsuarioPorIdUsuario(string idUsuario);
    }
}
