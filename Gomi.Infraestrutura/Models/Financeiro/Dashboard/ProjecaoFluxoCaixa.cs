using System;


namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ProjecaoFluxoCaixa
    {
        public ProjecaoFluxoCaixa( decimal seq, DateTime  dia  , DateTime diaprev, decimal pagar, decimal receber, decimal diferenca, decimal saldodia, decimal diferencaacumuladadia, decimal diferencaacumuladaprev)

        {
            Seq = seq;
            Dia = dia;
            Diaprev = diaprev;
            Pagar = pagar;
            Receber = receber;
            Diferenca = diferenca;
            Saldodia = saldodia;
            Diferencaacumuladadia = diferencaacumuladadia;
            Diferencaacumuladaprev = diferencaacumuladaprev;
        }

        public decimal Seq { get; set; }
        public DateTime Dia { get; set; }
        public DateTime Diaprev { get; set; }
        public decimal Pagar  { get; set; }
        public decimal Receber  { get; set; }
        public decimal Diferenca { get; set; }
        public decimal Saldodia { get; set; }
        public decimal Diferencaacumuladadia { get; set; }
        public decimal Diferencaacumuladaprev { get; set; }
    }
}
