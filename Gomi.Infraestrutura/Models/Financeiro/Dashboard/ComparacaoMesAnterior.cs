namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ComparacaoMesAnterior
    {
        public ComparacaoMesAnterior(string planoConta, decimal saldo, string mes)
        {
            PlanoConta = planoConta;
            Saldo = saldo;
            Mes = mes;
        }

        public string PlanoConta { get; set; }
        public decimal Saldo { get; set; }
        public string Mes { get; set; }
    }
}
