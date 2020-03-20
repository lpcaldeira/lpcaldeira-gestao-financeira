using System.Collections.Generic;
using System;

namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ResultadoLiquido
    {
        public ResultadoLiquido(decimal saldo, decimal percentual)
        {
            Saldo = saldo.ToString("C2");
            try
            {
                Percentual = Math.Round(percentual,2);
            }
            catch (Exception) { };
            Resultados = new List<DescricaoValor>
            {
                new DescricaoValor("Lucro", 100- percentual),
                new DescricaoValor("Prejuizo", percentual)

            };
        }

        public string Saldo { get; protected set; }
        public decimal Percentual { get; protected set; }
        public List<DescricaoValor> Resultados { get; protected set; }

    }
}
