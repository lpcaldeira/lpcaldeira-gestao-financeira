using System;
namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class AnaliseFinanceiraOutras
    {
        public AnaliseFinanceiraOutras(decimal mc_mediaano, decimal mc_acumulado, decimal mc_variacao, decimal ro_mediaano, decimal ro_acumulado, decimal ro_variacao, decimal rl_mediaano, decimal rl_acumulado, decimal rl_variacao)
        {
            MC_Mediaano = mc_mediaano.ToString("C2");
            MC_Acumulado = mc_acumulado.ToString("C2");
            MC_Variacao = mc_variacao.ToString("C2");
            RO_Mediaano = ro_mediaano.ToString("C2");
            RO_Acumulado = ro_acumulado.ToString("C2");
            RO_Variacao = ro_variacao.ToString("C2");
            RL_Mediaano = rl_mediaano.ToString("C2");
            RL_Acumulado = rl_acumulado.ToString("C2");
            RL_Variacao = rl_variacao.ToString("C2");
        }

        public string MC_Mediaano { get; set; }
        public string MC_Acumulado { get; set; }
        public string MC_Variacao { get; set; }
        public string RO_Mediaano { get; set; }
        public string RO_Acumulado { get; set; }
        public string RO_Variacao { get; set; }
        public string RL_Mediaano { get; set; }
        public string RL_Acumulado { get; set; }
        public string RL_Variacao { get; set; }

    }
}
