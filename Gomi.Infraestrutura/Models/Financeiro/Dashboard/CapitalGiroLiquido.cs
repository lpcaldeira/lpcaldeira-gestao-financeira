namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class CapitalGiroLiquido
    {
        public CapitalGiroLiquido( System.DateTime mes, decimal previsao, decimal realizado)

        {
           
            Mes = mes;
            Previsao = previsao;
            Realizado = realizado;
           
           
        }
        
        public System.DateTime Mes { get; set; }
       
        public decimal Previsao { get; set; }
        public decimal Realizado { get; set; }

    }
}
