namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class AnaliseFinanceiraGrid
    {
        public AnaliseFinanceiraGrid( string mes, decimal margemcontribuicao, decimal resultadooperacional, decimal resultadofinanceiro)

        {
           
            Mes = mes;
            Margemcontribuicao = margemcontribuicao;
            Resultadooperacional = resultadooperacional;
            Resultadofinanceiro = resultadofinanceiro;
           
        }
        
        public string Mes { get; set; }
       
        public decimal Margemcontribuicao { get; set; }
        public decimal Resultadooperacional { get; set; }
        public decimal Resultadofinanceiro { get; set; }
    }
}
