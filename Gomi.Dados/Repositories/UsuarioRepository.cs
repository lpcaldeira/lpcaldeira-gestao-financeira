using Dapper.Core.Contexts.Interfaces;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.ControleAcesso;

namespace Gomi.Dados.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDapperContext dapperContext)
            : base(dapperContext)
        { }

        public Usuario ObterUsuarioPorIdUsuario(string idUsuario)
        {
            return new Usuario();
            //using (var connection = Connection)
            //{
            //    var parametros = new DynamicParameters();
            //    parametros.Add("@idUsuario", "%" + idUsuario + "%", System.Data.DbType.String);

            //    connection.Open();
            //    return connection.Query<Usuario>("select * from usuario u           " +
            //                                     "where u.idusuario like @idUsuario ", parametros).Single();
            //}
        }
    }
}
