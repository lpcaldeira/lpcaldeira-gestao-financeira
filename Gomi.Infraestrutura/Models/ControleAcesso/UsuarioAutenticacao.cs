using System.ComponentModel.DataAnnotations;

namespace Gomi.Infraestrutura.Models.ControleAcesso
{
    public class UsuarioAutenticacao
    {
        public string IdUsuario { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
