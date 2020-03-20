namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrazoMedioEstocagemMesAtual
    {
        public PrazoMedioEstocagemMesAtual(  int dias)

        {
            
            Dias = dias;
           
           
        } 

        public int Dias { get; set; }
       
    }
}
