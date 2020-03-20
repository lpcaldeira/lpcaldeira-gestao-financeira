using System;
namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class DistribuicaoResultado
    {
        public DistribuicaoResultado(string categoria, decimal mesatual, decimal mesanterior , decimal diferenca)
        {
            Categoria = categoria;
            Mesatual = mesatual;
            Mesanterior = mesanterior;
            Diferenca = diferenca;
            try
            {
                Percentual = Math.Round((diferenca * 100) / mesanterior,2);
            }
            catch (Exception) { };

        }

        public string Categoria { get; set; }
        public decimal Mesatual { get; set; }
        public decimal Mesanterior { get; set; }
        public decimal Diferenca { get; set; }
        public decimal Percentual { get; set; }

    }
}
