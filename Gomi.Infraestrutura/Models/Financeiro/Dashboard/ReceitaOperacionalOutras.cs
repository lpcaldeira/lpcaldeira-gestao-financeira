namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ReceitaOperacionalOutras
    {
        public ReceitaOperacionalOutras(decimal mediaano, decimal acumulado, decimal variacao )
        {   
            Mediaano = mediaano.ToString("C2");
            Acumulado = acumulado.ToString("C2");
            Variacao = variacao.ToString("C2");
        }

        public string Mediaano { get; set; }
        public string Acumulado { get; set; }
        public string Variacao { get; set; }

    }
}
