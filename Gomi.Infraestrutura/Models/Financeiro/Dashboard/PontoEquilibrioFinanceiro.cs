using System;
namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class PontoEquilibrioFinanceiro
    {
        public PontoEquilibrioFinanceiro(System.DateTime mes, decimal receitaoperacional, decimal despesasoperacionais, decimal deducoes, decimal custos, decimal ativfinancimentodebito, decimal ativfinancimentocredito, decimal ativinvestimentodebito, decimal ativinvestimentoCredito)

        {

            Mes = mes;
            Receitaoperacional = receitaoperacional;
            try
            {
                PontoEquilibrio = Math.Round((despesasoperacionais + ativfinancimentodebito) / ((receitaoperacional - deducoes - custos) / (receitaoperacional)),2);
            }
            catch (Exception) { };

            try
            {
                PME = Math.Round((( receitaoperacional - deducoes - custos) / ( receitaoperacional)),2);
            }
            catch (Exception) { };
        }
        public System.DateTime Mes { get; set; }
        public decimal Receitaoperacional { get; set; }
        public decimal Despesasoperacionais { get; set; }
        public decimal Deducoes { get; set; }
        public decimal Custos { get; set; }
        public decimal Ativfinancimentodebito { get; set; }
        public decimal Ativfinancimentocredito { get; set; }
        public decimal Ativinvestimentodebito { get; set; }
        public decimal AtivinvestimentoCredito { get; set; }
        public decimal PontoEquilibrio { get; set; }
        public decimal PME { get; set; }
    }
}
