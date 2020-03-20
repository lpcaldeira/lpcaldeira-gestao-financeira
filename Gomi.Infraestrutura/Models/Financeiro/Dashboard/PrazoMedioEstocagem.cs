namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrazoMedioEstocagem
    {
        public PrazoMedioEstocagem( System.DateTime mes, int dias)

        {
           
            Mes = mes;
            Dias = dias;
           
           
        }
        
        public System.DateTime Mes { get; set; }
       
        public int Dias { get; set; }
       
    }
}
