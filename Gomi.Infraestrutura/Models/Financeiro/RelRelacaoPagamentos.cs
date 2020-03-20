using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class RelRelacaoPagamentos
    {
    
        public RelRelacaoPagamentos(DateTime data, string nome, string categoria, string descricao, decimal valor)
        {
            Data = data;
            Nome = nome;
            Categoria = categoria;
            Descricao = descricao;
            Valor = valor;

        }
        
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        
    }
}
