using System.Collections.Generic;
using System.Linq;
using Dapper.Core.Attributes;
using Gomi.Infraestrutura.Enums;
using Gomi.Infraestrutura.Helpers;
using StringLength = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    [Table("planoconta")]
    public class PlanoConta
    {
        private string _tipoMovimento;

        [Key("planocontasequence")]
        [IsNotNull]
        public int Id { get; set; }

        [IsNotNull]
        public int IdOrigem { get; set; }

        [Length(3)]
        [IsNotNull]
        public GrupoPlanoConta Grupo { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Grupo é obrigatório")]
        [Length(255)]
        [IsNotNull]
        public string Descricao
        {
            get => Grupo.Description();
            set => Grupo = EnumHelper.GetValueFromDescription<GrupoPlanoConta>(value);
        }

        [Length(3)]
        [IsNotNull]
        public TipoMovimentoPlanoConta TipoMovimentoValor { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Tipo do movimento é obrigatório")]
        [Length(50)]
        [IsNotNull]
        public string TipoMovimento
        {
            get => _tipoMovimento;
            set
            {
                if (value == null) return;
                TipoMovimentoValor = EnumHelper.GetValueFromDescription<TipoMovimentoPlanoConta>(value);
                _tipoMovimento = TipoMovimentoValor.Description();
            }
        }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Categoria é obrigatória")]
        [Length(255)]
        [IsNotNull]
        public string Categoria { get; set; }

        [Ignore]
        public bool IsEntrada => TipoMovimentoValor == TipoMovimentoPlanoConta.Entrada;

        [Ignore]
        public bool RegistroNovo => Id == 0;

        [Ignore]
        public IEnumerable<string> TiposMovimento => EnumHelper.ToList<TipoMovimentoPlanoConta>().Select(x => x.Value);

        [Ignore]
        public IEnumerable<string> Grupos => EnumHelper.ToList<GrupoPlanoConta>().Select(x => x.Value);
    }
}
