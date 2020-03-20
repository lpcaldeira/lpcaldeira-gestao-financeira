namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class SaldoDisponibilidade
    {
        public SaldoDisponibilidade(int id, string conta, decimal saldo)
        {
            Id = id;
            Conta = conta;
            Saldo = saldo.ToString("C2");
        }

        public int Id { get; set; }
        public string Conta { get; set; }
        public string Saldo { get; set; }
    }
}
