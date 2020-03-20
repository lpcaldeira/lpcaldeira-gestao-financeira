using System;
using System.Collections.Generic;
using Dapper.Core.Attributes;
using Dapper.Core.Enums;
using Required = System.ComponentModel.DataAnnotations.RequiredAttribute;
using Range = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    [Table("transferencia")]
    public class Transferencia
    {
        public Transferencia()
        {
            DataTransferencia = DateTime.Now;
            TransferenciaOrigem = new TransferenciaOrigem();
            TransferenciaDestino = new TransferenciaDestino();
        }

        [Key("transferenciasequence")]
        [IsNotNull]
        public int Id { get; set; }

        [Ignore]
        [Reference(typeof(TransferenciaOrigem), RelationType.OneToOne, CascadeType.CascadeAll)]
        public TransferenciaOrigem TransferenciaOrigem { get; set; }

        [ReferenceKey(typeof(TransferenciaOrigem))]
        [IsNotNull]
        public int IdTransferenciaOrigem { get; set; }

        [Ignore]
        [Reference(typeof(TransferenciaDestino), RelationType.OneToOne, CascadeType.CascadeAll)]
        public TransferenciaDestino TransferenciaDestino { get; set; }

        [ReferenceKey(typeof(TransferenciaDestino))]
        [IsNotNull]
        public int IdTransferenciaDestino { get; set; }

        [Required(ErrorMessage = "Data da transferência é obrigatória")]
        [IsNotNull]
        public DateTime DataTransferencia { get; set; }

        [Range((double)0.01m, (double)decimal.MaxValue, ErrorMessage = @"Valor é obrigatório.")]
        [Precision]
        public decimal Valor { get; set; }

        [Ignore]
        public bool RegistroNovo => Id == 0;

        [Ignore]
        public IEnumerable<Conta> Contas { get; protected set; }

        public Transferencia SetarContas(IEnumerable<Conta> contas)
        {
            Contas = contas;
            return this;
        }
    }
}