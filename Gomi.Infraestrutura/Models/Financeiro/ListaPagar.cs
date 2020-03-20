using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class ListaPagar
    {
        public ListaPagar(int id, DateTime vencimento, DateTime datapagamento, int idpessoa,  string pessoa, string categoria, string descricao, decimal valor, decimal valorpago)
        {
            Id = id;
            Vencimento = vencimento;
            Datapagamento = datapagamento;
            Idpessoa = idpessoa;
            Pessoa = pessoa;
            Categoria = categoria;
            Descricao = descricao;
            Valor = valor;
            Valorpago = valorpago;

        }

        public int Id { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Datapagamento { get; set; }
        public int Idpessoa { get; set; }
        public string Pessoa { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Valorpago { get; set; }
        
    }
}
