using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class RelDRE
    {
    
        public RelDRE( int seq, string descricao, string categoria, decimal valor, decimal percentual)
        {
            Seq = seq;
            Descricao = descricao;
            Categoria = categoria;
            Valor = valor;
            Percentual = percentual;

        }

        public int Seq { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public decimal Percentual { get; set; }

    }
}
