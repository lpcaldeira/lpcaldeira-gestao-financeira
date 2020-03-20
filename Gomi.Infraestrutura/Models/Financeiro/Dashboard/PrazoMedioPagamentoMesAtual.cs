namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrazoMedioPagamentoMesAtual
    {
        public PrazoMedioPagamentoMesAtual(  int dias)

        {
            
            Dias = dias;
           
           
        } 

        public int Dias { get; set; }
       
    }
}
