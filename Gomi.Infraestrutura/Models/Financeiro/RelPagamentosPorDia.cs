using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class RelPagamentosPorDia
    {

        public RelPagamentosPorDia(int seq, DateTime competencia, string diasemana, decimal valor,  decimal percentual, decimal valoracumulado, decimal percentualacumulado)
        {
            Seq = seq ;
            Competencia = competencia;
            Diasemana = diasemana;
            Valor = valor;
            Percentual = percentual;
            Valoracumulado = valoracumulado;
            Percentualacumulado = percentualacumulado;

        }

        public int Seq { get; set; }
        public DateTime Competencia { get; set; }
        public string Diasemana { get; set; }
        public decimal Valor { get; set; }
        public decimal Percentual { get; set; }
        public decimal Valoracumulado { get; set; }
        public decimal Percentualacumulado { get; set; }
        
    }
}
