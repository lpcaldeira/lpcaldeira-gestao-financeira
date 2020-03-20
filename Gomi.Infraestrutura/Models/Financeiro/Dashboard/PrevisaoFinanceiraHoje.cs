namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrevisaoFinanceiraHoje
    {
        public PrevisaoFinanceiraHoje(decimal totalContasReceber, decimal totalContasPagar, decimal diferenca, System.DateTime hoje)
        {
            TotalContasReceber = totalContasReceber.ToString("C2");
            TotalContasPagar = totalContasPagar.ToString("C2");
            Diferenca = diferenca.ToString("C2");
            Hoje = hoje;
        }

        public string TotalContasReceber { get; protected set; }
        public string TotalContasPagar { get; protected set; }
        public string Diferenca { get; protected set; }
        public System.DateTime Hoje { get; protected set; }
    }
}
