using Dapper.Core.Attributes;
using Dapper.Core.Enums;
using StringLength = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    [Table("transferenciaorigem")]
    public class TransferenciaOrigem
    {
        private Conta _conta;

        [Key("transferenciaorigemsequence")]
        [IsNotNull]
        public int Id { get; set; }

        [Ignore]
        [Reference(typeof(Conta), RelationType.OneToOne)]
        public Conta Conta
        {
            get => _conta;
            set
            {
                _conta = value;
                IdConta = _conta?.Id ?? 0;
                NomeConta = _conta?.Nome;
            }
        }

        [ReferenceKey(typeof(Conta))]
        [IsNotNull]
        public int IdConta { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Conta de origem é obrigatória")]
        [Ignore]
        public string NomeConta { get; set; }

        [Ignore]
        [Reference(typeof(Caixa), RelationType.OneToOne)]
        public Caixa Caixa { get; set; }

        [ReferenceKey(typeof(Caixa))]
        [IsNotNull]
        public int IdCaixa { get; set; }
    }
}