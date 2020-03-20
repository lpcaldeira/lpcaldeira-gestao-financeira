namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class TotaisporCategoria
    {
        public TotaisporCategoria( string mes, decimal receitaoperacional, decimal despesasoperacionais, decimal deducoes, decimal custos, decimal ativfinancimentodebito, decimal ativfinancimentocredito, decimal ativinvestimentodebito, decimal ativinvestimentoCredito)

        {
           
            Mes = mes;
            Receitaoperacional = receitaoperacional.ToString("C2"); 
            Despesasoperacionais = despesasoperacionais.ToString("C2");
            Deducoes = deducoes.ToString("C2");
            Custos = custos.ToString("C2");
            Ativfinancimentodebito = ativfinancimentodebito.ToString("C2");
            Ativfinancimentocredito = ativfinancimentocredito.ToString("C2");
            Ativinvestimentodebito = ativinvestimentodebito.ToString("C2");
            AtivinvestimentoCredito = ativinvestimentoCredito.ToString("C2");

        }
        public string Mes { get; set; }
        public string Receitaoperacional { get; set; }
        public string Despesasoperacionais { get; set; }
        public string Deducoes { get; set; }
        public string Custos { get; set; }
        public string Ativfinancimentodebito { get; set; }
        public string Ativfinancimentocredito { get; set; }
        public string Ativinvestimentodebito { get; set; }
        public string AtivinvestimentoCredito { get; set; }
    }
}
