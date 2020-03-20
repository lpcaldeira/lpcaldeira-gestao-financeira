namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class CicloFinanceiro
    {
        public CicloFinanceiro( System.DateTime mes, decimal?  pmp, decimal? pmr, decimal? pme)

        {
            Mes = mes;
            Pmp = pmp;
            Pmr = pmr;
            Pme = pme;
            Ciclo = Pmr + Pme - Pmp;  
        }
        
        public System.DateTime Mes { get; set; }
        public decimal? Pmp { get; set; }
        public decimal? Pmr { get; set; }
        public decimal? Pme { get; set; }
        public decimal? Ciclo { get; set; }

    }
}
