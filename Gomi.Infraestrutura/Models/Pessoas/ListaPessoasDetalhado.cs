using System.Collections.Generic;
using System;

namespace Gomi.Infraestrutura.Models.Pessoas
{
    public class ListaPessoasDetalhado
    {
        public ListaPessoasDetalhado(int id, string nome, string categoria, string descricao, DateTime vencimento, 
            DateTime emissao, string situacao, decimal valor, decimal emaberto, decimal vencido, decimal recebido, string tipo)
        {
            Id = id;
            Nome = nome;
            Categoria = categoria;
            Descricao = descricao;
            Vencimento = vencimento;
            Emissao = emissao;
            Situacao = situacao;
            Valor = valor;
            Emaberto = emaberto;
            Vencido = vencido;
            Recebido = recebido;
            Tipo = tipo;
        }

        public int Id { get; protected set; }
        public string Nome { get; protected set; }
        public string Categoria  { get; protected set; }
        public string Descricao { get; protected set; }
        public DateTime Vencimento { get; protected set; }
        public DateTime Emissao { get; protected set; }
        public string Situacao { get; protected set; }
        public decimal Valor { get; protected set; }
        public decimal Emaberto { get; protected set; }
        public decimal Vencido { get; protected set; }
        public decimal Recebido { get; protected set; }
        public string Tipo { get; protected set; }


    }
}
