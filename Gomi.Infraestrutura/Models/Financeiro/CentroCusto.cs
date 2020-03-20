using Dapper.Core.Attributes;
using StringLength = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    [Table("centrocusto")]
    public class CentroCusto
    {
        [Key("centrocustosequence")]
        [IsNotNull]
        public int Id { get; set; }

        [IsNotNull]
        public int IdOrigem { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Descrição é obrigatória")]
        [Length(255)]
        [IsNotNull]
        public string Descricao { get; set; }

        [Ignore]
        public bool RegistroNovo => Id == 0;
    }
}
