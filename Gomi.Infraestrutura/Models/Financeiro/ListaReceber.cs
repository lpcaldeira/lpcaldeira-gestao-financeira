using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class ListaReceber
    {
        public ListaReceber(int id, DateTime vencimento, DateTime datarecebimento, int idpessoa,  string pessoa, string categoria, string descricao, decimal valor, decimal valorrecebido)
        {
            Id = id;
            Vencimento = vencimento;
            Datarecebimento = datarecebimento;
            Idpessoa = idpessoa;
            Pessoa = pessoa;
            Categoria = categoria;
            Descricao = descricao;
            Valor = valor;
            Valorrecebido = valorrecebido;

        }

        public int Id { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Datarecebimento { get; set; }
        public int Idpessoa { get; set; }
        public string Pessoa { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Valorrecebido { get; set; }
        
    }
}
