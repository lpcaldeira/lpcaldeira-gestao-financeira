namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrevisaoFinanceiraProximaSemana
    {
        public PrevisaoFinanceiraProximaSemana(decimal totalContasReceber, decimal totalContasPagar, decimal diferenca, System.DateTime dataInicialSemana, System.DateTime dataFinalSemana)
        {
            TotalContasReceber = totalContasReceber;
            TotalContasPagar = totalContasPagar;
            Diferenca = diferenca;
            DataInicialSemana = dataInicialSemana;
            DataFinalSemana = dataFinalSemana;

        }

        public decimal TotalContasReceber { get; protected set; }
        public decimal TotalContasPagar { get; protected set; }
        public decimal Diferenca { get; protected set; }
        public System.DateTime DataInicialSemana { get; protected set; }
        public System.DateTime DataFinalSemana { get; protected set; }

    }

}
