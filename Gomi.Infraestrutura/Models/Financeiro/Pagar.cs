using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Core.Attributes;
using Dapper.Core.Enums;
using Gomi.Infraestrutura.Enums;
using Gomi.Infraestrutura.Helpers;
using Gomi.Infraestrutura.Models.Pessoas;
using Required = System.ComponentModel.DataAnnotations.RequiredAttribute;
using Range = System.ComponentModel.DataAnnotations.RangeAttribute;
using StringLength = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    [Table("pagar")]
    public class Pagar
    {
        private Pessoa _pessoa;
        private Conta _contaPagamento;

        public Pagar()
        {
            Emissao = DateTime.Now;
            Vencimento = DateTime.Now;
        }

        [Key("pagarsequence")]
        [IsNotNull]
        public int Id { get; set; }

        [Ignore]
        [Reference(typeof(Empresa), RelationType.OneToOne)]
        public Empresa Empresa { get; set; }

        [ReferenceKey(typeof(Empresa))]
        [IsNotNull]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Data de emissão é obrigatória")]
        [IsNotNull]
        public DateTime Emissao { get; set; }

        [Required(ErrorMessage = "Data de vencimento é obrigatória")]
        public DateTime Vencimento { get; set; }

        [Ignore]
        [Reference(typeof(Pessoa), RelationType.OneToOne)]
        public Pessoa Pessoa
        {
            get => _pessoa;
            set
            {
                _pessoa = value;
                IdPessoa = _pessoa?.Id ?? 0;
                NomePessoa = _pessoa?.Nome;
                TransacionadorValor = _pessoa?.TransacionadorValor ?? Enums.Transacionador.Fornecedor;
            }
        }

        [ReferenceKey(typeof(Pessoa))]
        [IsNotNull]
        public int IdPessoa { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Pessoa é obrigatória")]
        [Ignore]
        public string NomePessoa { get; set; }

        [Length(3)]
        [IsNotNull]
        public Transacionador TransacionadorValor { get; set; }

        [Length(50)]
        [IsNotNull]
        public string Transacionador
        {
            get => TransacionadorValor.Description();
            set => TransacionadorValor = EnumHelper.GetValueFromDescription<Transacionador>(value);
        }

        [Range((double)0.01m, (double)decimal.MaxValue, ErrorMessage = @"Valor é obrigatório.")]
        [Precision]
        public decimal Valor { get; set; }

        [Length(500)]
        public string Descricao { get; set; }

        [Ignore]
        [Reference(typeof(PlanoConta), RelationType.OneToOne)]
        public PlanoConta PlanoConta { get; set; }

        [ReferenceKey(typeof(PlanoConta))]
        public int? IdPlanoConta { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Categoria é obrigatória")]
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
        public Conta ContaPagamento
        {
            get => _contaPagamento;
            set
            {
                _contaPagamento = value;
                IdContaPagamento = _contaPagamento?.Id ?? 0;
                NomeContaPagamento = _contaPagamento?.Nome;
            }
        }

        [ReferenceKey(typeof(Conta))]
        public int? IdContaPagamento { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Conta é obrigatória")]
        [Length(255)]
        public string NomeContaPagamento { get; set; }

        public DateTime? DataPagamento { get; set; }

        [Range((double)0.01m, (double)decimal.MaxValue, ErrorMessage = @"Valor é obrigatório.")]
        [Precision]
        public decimal ValorPago { get; set; }

        [Ignore]
        [Reference(typeof(Caixa), RelationType.OneToOne)]
        public Caixa Caixa { get; set; }

        [ReferenceKey(typeof(Caixa))]
        public int? IdCaixa { get; set; }

        [Ignore]
        public IEnumerable<PlanoConta> PlanosContas { get; protected set; }

        [Ignore]
        public IEnumerable<CentroCusto> CentrosCustos { get; protected set; }

        [Ignore]
        public IEnumerable<string> TiposTransacionadores => EnumHelper.ToList<Transacionador>().Select(x => x.Value);

        [Ignore]
        public bool RegistroNovo => Id == 0;

        [Ignore]
        public bool RegistroAPagar => ValorPago == 0;

        public Pagar SetarIdEmpresa(int idEmpresa)
        {
            IdEmpresa = idEmpresa;
            return this;
        }

        public Pagar SetarPlanosContas(IEnumerable<PlanoConta> planosContas)
        {
            PlanosContas = planosContas;
            return this;
        }

        public Pagar SetarCentrosCustos(IEnumerable<CentroCusto> centrosCustos)
        {
            CentrosCustos = centrosCustos;
            return this;
        }

        public Pagar SetarIdPlanoConta(int? idPlanoConta)
        {
            IdPlanoConta = idPlanoConta;
            return this;
        }

        public Pagar SetarDescricaoPlanoConta(string descricaoPlanoConta)
        {
            DescricaoPlanoConta = descricaoPlanoConta;
            return this;
        }

        public Pagar SetarDescricao(string descricao)
        {
            Descricao = descricao;
            return this;
        }
    }
}
