using System.Collections.Generic;
using System.Linq;
using Dapper.Core.Attributes;
using Gomi.Infraestrutura.ValueObjects;

namespace Gomi.Infraestrutura.Models.Pessoas
{
    [Table("empresa")]
    public class Empresa
    {
        [Key("empresasequence")]
        [IsNotNull]
        public int Id { get; set; }

        [IsNotNull]
        public int IdOrigem { get; set; }

        [Length(255)]
        [IsNotNull]
        public string Razao { get; set; }

        [Length(255)]
        public string Fantasia { get; set; }

        [Length(14)]
        public string Cnpj { get; set; }

        [Length(20)]
        public string Telefone { get; set; }

        [Length(255)]
        public string Email { get; set; }

        [Length(255)]
        public string Endereco { get; set; }

        [Length(255)]
        public string Numero { get; set; }

        [Length(255)]
        public string Bairro { get; set; }

        [Length(8)]
        public string Cep { get; set; }

        [Length(255)]
        public string Complemento { get; set; }

        [Length(255)]
        public string Cidade { get; set; }

        [Length(2)]
        public string Uf { get; set; }

        [Length(255)]
        public string Pais { get; set; }

        [IsNotNull]
        public int OrigemInformacao { get; set; }

        [Ignore]
        public IEnumerable<Uf> ListaUFs => UFsValueObject.Dados.OrderBy(x => x.Sigla);
    }
}
