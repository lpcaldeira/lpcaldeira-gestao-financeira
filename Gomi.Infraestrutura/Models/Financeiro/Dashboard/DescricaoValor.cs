namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class DescricaoValor
    {
        public DescricaoValor(string descricao, decimal valor)
        {
            Descricao = descricao;
            Valor = valor;
        }

        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
