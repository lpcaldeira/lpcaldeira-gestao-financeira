using System.Collections.Generic;
using System;

namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ResultadoLiquidoAno
    {
        public ResultadoLiquidoAno(decimal saldo, decimal percentual)
        {
            Saldo = saldo.ToString("C2");
            Percentual = Math.Round(percentual,2);
            Resultados = new List<DescricaoValor>
            {
                new DescricaoValor("Lucro", 100- Math.Round(percentual,2)),
                new DescricaoValor("Prejuizo", Math.Round(percentual,2))

            };
        }

        public string Saldo { get; protected set; }
        public decimal Percentual { get; protected set; }
        public List<DescricaoValor> Resultados { get; protected set; }

    }
}
