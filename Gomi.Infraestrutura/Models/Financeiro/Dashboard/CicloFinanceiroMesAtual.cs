namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class CicloFinanceiroMesAtual
    {
        public CicloFinanceiroMesAtual(  int dias)

        {
            
            Dias = dias;
           
           
        } 

        public int Dias { get; set; }
       
    }
}
