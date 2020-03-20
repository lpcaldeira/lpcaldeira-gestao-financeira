using Dapper.Core.Attributes;
using Dapper.Core.Enums;
using Gomi.Infraestrutura.Enums;
using Gomi.Infraestrutura.Models.Pessoas;

namespace Gomi.Infraestrutura.Models.ControleAcesso
{
    [Table("perfil")]
    public class Perfil
    {
        [Key("perfilsequence")]
        [IsNotNull]
        public int Id { get; set; }

        [ReferenceKey(typeof(Usuario))]
        [IsNotNull]
        public int IdUsuario { get; set; }

        [Ignore]
        [Reference( typeof(Pessoa), RelationType.OneToOne)]
        public Pessoa Empresa { get; set; }

        [ReferenceKey(typeof(Pessoa))]
        [IsNotNull]
        public int IdEmpresa { get; set; }

        [Length(50)]
        [IsNotNull]
        public TipoPerfil TipoPerfil { get; set; }
    }
}
