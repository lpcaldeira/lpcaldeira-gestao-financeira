using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Core.Attributes;
using Gomi.Infraestrutura.Enums;
using Gomi.Infraestrutura.Helpers;
using Gomi.Infraestrutura.ValueObjects;
using Required = System.ComponentModel.DataAnnotations.RequiredAttribute;
using StringLength = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    [Table("conta")]
    public class Conta
    {
        public Conta()
        {
            DataSaldoInicial = DateTime.Now;
        }

        [Key("contasequence")]
        [IsNotNull]
        public int Id { get; set; }

        [IsNotNull]
        public int IdOrigem { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nome é obrigatório")]
        [Length(255)]
        [IsNotNull]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data do saldo é obrigatório")]
        [IsNotNull]
        public DateTime DataSaldoInicial { get; set; }

        [Precision]
        [IsNotNull]
        public decimal SaldoInicial { get; set; }

        public int? IdCaixa { get; set; }

        [Length(3)]
        [IsNotNull]
        public TipoConta TipoContaValor { get; set; }

        [Length(50)]
        [IsNotNull]
        public string TipoConta
        {
            get => TipoContaValor.Description();
            set => TipoContaValor = EnumHelper.GetValueFromDescription<TipoConta>(value);
        }

        [Length(20)]
        public string NumeroAgencia { get; set; }

        [Length(20)]
        public string NumeroConta { get; set; }

        [Length(2)]
        public string DigitoVerificadorConta { get; set; }

        [IsNotNull]
        public int IdBanco { get; set; }

        [Length(255)]
        public string NomeBanco { get; set; }

        [Ignore]
        public bool RegistroNovo => Id == 0;

        [Ignore]
        public IEnumerable<Banco> ListaBancos => BancosValueObject.Dados.OrderBy(x => x.Nome);

        [Ignore]
        public IEnumerable<string> TiposConta => EnumHelper.ToList<TipoConta>().Select(x => x.Value);
    }
}
