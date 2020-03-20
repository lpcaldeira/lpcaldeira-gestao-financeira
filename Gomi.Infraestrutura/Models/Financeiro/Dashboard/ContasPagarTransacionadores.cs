namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ContasPagarTransacionadores

    {
        public ContasPagarTransacionadores(string transacionador, decimal valor)
        {
            Transacionador = transacionador;
            Valor = valor;
        }

        public string Transacionador { get; set; }
        public decimal Valor { get; set; }
        
    }

}
