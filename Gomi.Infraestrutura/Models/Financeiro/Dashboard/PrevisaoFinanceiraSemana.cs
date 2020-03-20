
namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PrevisaoFinanceiraSemana
    {
        public PrevisaoFinanceiraSemana(decimal receber, decimal pagar, decimal diferenca, System.DateTime dataIncialSemana, System.DateTime dataFinalSemana)
        {
            Receber = receber.ToString("C2");
            Pagar = pagar.ToString("C2");
            Diferenca = diferenca.ToString("C2");
            DataIncialSemana = dataIncialSemana;
            DataFinalSemana = dataFinalSemana;

        }

        public string Receber { get; protected set; }
        public string Pagar { get; protected set; }
        public string Diferenca { get; protected set; }
        public  System.DateTime DataIncialSemana { get; protected set; }
        public  System.DateTime DataFinalSemana { get; protected set; }

    }
}
