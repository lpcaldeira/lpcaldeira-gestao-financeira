using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class RelPagamentosPorTipoPessoa
    {

        public RelPagamentosPorTipoPessoa(int id, int transacionadorvalor, string transacionador, DateTime datapagamento, string nome, string categoria, string descricao, decimal valor)
        {
            Id = id;
            Transacionadorvalor = transacionadorvalor;
            Transacionador = transacionador;
            Datapagamento = datapagamento;
            Nome = nome;
            Categoria = categoria;
            Descricao = descricao;
            Valor = valor;

        }
        public int Id { get; set; }
        public int Transacionadorvalor { get; set; }
        public string Transacionador { get; set; }
        public DateTime Datapagamento { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        
    }
}
