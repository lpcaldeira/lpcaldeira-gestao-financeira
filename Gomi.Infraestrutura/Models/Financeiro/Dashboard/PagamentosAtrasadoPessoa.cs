namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PagamentosAtrasadoPessoa
    
    {
        public PagamentosAtrasadoPessoa(string pessoa, decimal valor)
        {
            Pessoa = pessoa;
            Valor = valor;
        }

        public string Pessoa { get; set; }
        public decimal Valor { get; set; }
    }
}
