using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class RelPagamentosPorCategoria
    {

        public RelPagamentosPorCategoria( string categoria, decimal valor, decimal percentual)
        {
            Categoria = categoria;
            Valor = valor;
            Percentual = percentual;

        }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public decimal Percentual { get; set; }

    }
}
