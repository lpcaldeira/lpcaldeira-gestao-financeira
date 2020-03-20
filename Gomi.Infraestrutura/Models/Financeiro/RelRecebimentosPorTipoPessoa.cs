using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class RelRecebimentosPorTipoPessoa
    {

        public RelRecebimentosPorTipoPessoa(int id, int transacionadorvalor, string transacionador, DateTime datarecebimento, string nome, string categoria, string descricao, decimal valor)
        {
            Id = id;
            Transacionadorvalor = transacionadorvalor;
            Transacionador = transacionador;
            Datarecebimento = datarecebimento;
            Nome = nome;
            Categoria = categoria;
            Descricao = descricao;
            Valor = valor;

        }
        public int Id { get; set; }
        public int Transacionadorvalor { get; set; }
        public string Transacionador { get; set; }
        public DateTime Datarecebimento { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        
    }
}
