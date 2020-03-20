namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ContasReceberTransacionadores

    {
        public ContasReceberTransacionadores(string transacionador, decimal valor)
        {
            Transacionador = transacionador;
            Valor = valor;
        }

        public string Transacionador { get; set; }
        public decimal Valor { get; set; }
        
    }

}
