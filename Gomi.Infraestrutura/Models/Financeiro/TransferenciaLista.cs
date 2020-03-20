using System;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class TransferenciaLista
    {
        public TransferenciaLista()
        { }

        public TransferenciaLista(int id, DateTime dataTransferencia, decimal valor, string nomeContaTransferenciaOrigem, string nomeContaTransferenciaDestino)
        {
            Id = id;
            DataTransferencia = dataTransferencia;
            Valor = valor;
            NomeContaTransferenciaOrigem = nomeContaTransferenciaOrigem;
            NomeContaTransferenciaDestino = nomeContaTransferenciaDestino;
        }

        public int Id { get; set; }
        public DateTime DataTransferencia { get; set; }
        public decimal Valor { get; set; }
        public string NomeContaTransferenciaOrigem { get; set; }
        public string NomeContaTransferenciaDestino { get; set; }
    }
}