namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class RecebimentosCliente
    
    {
        public RecebimentosCliente(string pessoa, decimal valor)
        {
            Pessoa = pessoa;
            Valor = valor;
        }

        public string Pessoa { get; set; }
        public decimal Valor { get; set; }
    }
}
