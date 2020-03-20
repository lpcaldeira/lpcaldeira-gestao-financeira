namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ReceitaOperacionalMediaAno
    {
        public ReceitaOperacionalMediaAno( decimal mediaano)

        {
           
            Mediaano = mediaano;
            
        }
        
        public decimal Mediaano { get; set; }
    }
}
