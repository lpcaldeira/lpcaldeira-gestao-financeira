namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class GastosOperacionaisOutras
    {
        public GastosOperacionaisOutras(decimal ded_mediaano, decimal ded_acumulado, decimal ded_variacao, decimal c_mediaano, decimal c_acumulado, decimal c_variacao, decimal des_mediaano, decimal des_acumulado, decimal des_variacao)
        {
            
            Ded_Mediaano = ded_mediaano.ToString("C2"); 
            Ded_Acumulado = ded_acumulado.ToString("C2");
            Ded_Variacao = ded_variacao.ToString("C2");
            C_Mediaano = c_mediaano.ToString("C2");
            C_Acumulado = c_acumulado.ToString("C2");
            C_Variacao = c_variacao.ToString("C2");
            Des_Mediaano = des_mediaano.ToString("C2");
            Des_Acumulado = des_acumulado.ToString("C2");
            Des_Variacao = des_variacao.ToString("C2");
        }

        public string Ded_Mediaano { get; set; }
        public string Ded_Acumulado { get; set; }
        public string Ded_Variacao { get; set; }
        public string C_Mediaano { get; set; }
        public string C_Acumulado { get; set; }
        public string C_Variacao { get; set; }
        public string Des_Mediaano { get; set; }
        public string Des_Acumulado { get; set; }
        public string Des_Variacao { get; set; }

    }
}
