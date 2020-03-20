namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class SaldoDisponibilidadeSoma
    {
        public SaldoDisponibilidadeSoma(decimal saldo)
        {
            
            Saldo =  saldo.ToString("C2");
        }


        public string Saldo { get; set; }
    }
}
