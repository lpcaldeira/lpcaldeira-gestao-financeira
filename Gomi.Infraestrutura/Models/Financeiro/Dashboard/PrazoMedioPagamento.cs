namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrazoMedioPagamento
    {
        public PrazoMedioPagamento( System.DateTime mes, decimal pmp)

        {
           
            Mes = mes;
            Pmp = pmp;
           
           
        }
        
        public System.DateTime Mes { get; set; }
       
        public decimal Pmp { get; set; }
       
    }
}
