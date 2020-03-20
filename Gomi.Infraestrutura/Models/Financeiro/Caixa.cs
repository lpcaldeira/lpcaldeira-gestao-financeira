using System;
using Dapper.Core.Attributes;
using Dapper.Core.Enums;
using Gomi.Infraestrutura.Models.Pessoas;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    [Table("caixa")]
    public class Caixa
    {
        [Key("caixasequence")]
        [IsNotNull]
        public int Id { get; set; }

        [Ignore]
        [Reference(typeof(Empresa), RelationType.OneToOne)]
        public Empresa Empresa { get; set; }

        [ReferenceKey(typeof(Empresa))]
        [IsNotNull]
        public int IdEmpresa { get; set; }

        [Ignore]
        [Reference(typeof(PlanoConta), RelationType.OneToOne)]
        public PlanoConta PlanoConta { get; set; }

        [ReferenceKey(typeof(PlanoConta))]
        public int? IdPlanoConta { get; set; }

        [Length(255)]
        public string DescricaoPlanoConta { get; set; }

        [Ignore]
        [Reference(typeof(CentroCusto), RelationType.OneToOne)]
        public CentroCusto CentroCusto { get; set; }

        [ReferenceKey(typeof(CentroCusto))]
        public int? IdCentroCusto { get; set; }

        [Length(255)]
        public string DescricaoCentroCusto { get; set; }

        [Ignore]
        [Reference(typeof(Conta), RelationType.OneToOne)]
        public Conta Conta { get; set; }

        [ReferenceKey(typeof(Conta))]
        [IsNotNull]
        public int IdConta { get; set; }

        [Length(255)]
        [IsNotNull]
        public string NomeConta { get; set; }

        [IsNotNull]
        public DateTime DataCompetencia { get; set; }

        [Length(1000)]
        [IsNotNull]
        public string Descricao { get; set; }

        [Precision]
        [IsNotNull]
        public decimal Credito { get; set; }

        [Precision]
        [IsNotNull]
        public decimal Debito { get; set; }
    }
}
