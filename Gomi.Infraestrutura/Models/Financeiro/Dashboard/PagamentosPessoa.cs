namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PagamentosPessoa
    
    {
        public PagamentosPessoa(string pessoa, decimal valor)
        {
            Pessoa = pessoa;
            Valor = valor;
        }

        public string Pessoa { get; set; }
        public decimal Valor { get; set; }
    }
}
