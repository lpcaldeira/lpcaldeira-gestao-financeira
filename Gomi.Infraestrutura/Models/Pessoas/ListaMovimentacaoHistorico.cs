using System.Collections.Generic;
using System;

namespace Gomi.Infraestrutura.Models.Pessoas
{
    public class ListaMovimentacaoHistorico
    {
        public ListaMovimentacaoHistorico(int id, string nome, DateTime vencimento, 
            DateTime emissao,  decimal valor, decimal emaberto, decimal vencido, decimal recebido, string tipo)
        {
            Id = id;
            Nome = nome;
            Vencimento = vencimento ;
            Emissao = emissao;
            Valor = valor;
            Emaberto = emaberto;
            Vencido = vencido;
            Recebido = recebido;
            Tipo = tipo;
            
        }

        public int Id { get; protected set; }
        public string Nome { get; protected set; }
        public DateTime Vencimento { get; protected set; }
        public DateTime Emissao { get; protected set; }
        public decimal Valor { get; protected set; }
        public decimal Emaberto { get; protected set; }
        public decimal Vencido { get; protected set; }
        public decimal Recebido { get; protected set; }
        public string Tipo { get; protected set; }


    }
}
