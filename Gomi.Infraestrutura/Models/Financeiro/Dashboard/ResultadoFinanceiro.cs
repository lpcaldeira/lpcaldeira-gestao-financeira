using System.Collections.Generic;

namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ResultadoFinanceiro
    {
        public ResultadoFinanceiro(decimal saldo, decimal percentualLucro)
        {
            SaldoFormatado = saldo.ToString("c2"); 
            PercentualLucro = percentualLucro;
            Resultados = new List<DescricaoValor>
            {
                new DescricaoValor("Lucro", percentualLucro),
                new DescricaoValor("Prejuizo", 100 - percentualLucro)

            };
        }

        public string SaldoFormatado { get; protected set; }
        public decimal PercentualLucro { get; protected set; }
        public List<DescricaoValor> Resultados { get; protected set; }
    }
}
