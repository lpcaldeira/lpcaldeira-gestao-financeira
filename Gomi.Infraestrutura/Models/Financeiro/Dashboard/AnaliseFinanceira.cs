using System;
namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class AnaliseFinanceira
    {
        public AnaliseFinanceira( System.DateTime mes, decimal receitasoperacionais, decimal margemcontribuicao, decimal resultadooperacional, decimal resultadofinanceiro)

        {
           
            Mes = mes;
            Receitasoperacionais = receitasoperacionais;
            Margemcontribuicao = margemcontribuicao;
            Resultadooperacional = resultadooperacional;
            Resultadofinanceiro = resultadofinanceiro;

            try
            {
                MC_per =  Math.Round((margemcontribuicao * 100) / receitasoperacionais,2);
            }
            catch (Exception) { };

            try
            {
                RO_per = Math.Round((Resultadooperacional * 100) / receitasoperacionais,2);
            }
            catch (Exception) { };

            try
            {
                RF_per = Math.Round((resultadofinanceiro * 100) / receitasoperacionais,2);
            }
            catch (Exception) { };


           
        }
        
        public System.DateTime Mes { get; set; }
        public decimal Receitasoperacionais { get; set; }
        public decimal Margemcontribuicao { get; set; }
        public decimal Resultadooperacional { get; set; }
        public decimal Resultadofinanceiro { get; set; }
        public decimal MC_per { get; set; }
        public decimal RO_per { get; set; }
        public decimal RF_per { get; set; }

    }
}
