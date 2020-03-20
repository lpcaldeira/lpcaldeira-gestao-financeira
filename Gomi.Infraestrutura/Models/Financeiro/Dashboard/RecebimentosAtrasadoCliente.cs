namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class RecebimentosAtrasadoCliente
    
    {
        public RecebimentosAtrasadoCliente(string pessoa, decimal valor)
        {
            Pessoa = pessoa;
            Valor = valor;
        }

        public string Pessoa { get; set; }
        public decimal Valor { get; set; }
    }
}
