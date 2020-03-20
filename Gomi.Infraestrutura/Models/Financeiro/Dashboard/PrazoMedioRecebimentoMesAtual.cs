namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrazoMedioRecebimentoMesAtual
    {
        public PrazoMedioRecebimentoMesAtual(  int dias)

        {
            
            Dias = dias;
           
           
        } 

        public int Dias { get; set; }
       
    }
}
