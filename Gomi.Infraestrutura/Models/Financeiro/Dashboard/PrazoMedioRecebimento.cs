namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrazoMedioRecebimento
    {
        public PrazoMedioRecebimento(System.DateTime mes, decimal pmr)

        {
           
            Mes = mes;
            Pmr = pmr;
           
           
        }
        
        public System.DateTime Mes { get; set; }
       
        public decimal Pmr { get; set; }
       
    }
}
