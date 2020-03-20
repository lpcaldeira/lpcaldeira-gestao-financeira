using System.ComponentModel;

namespace Gomi.Infraestrutura.Enums
{
    public enum GrupoPlanoConta
    {
        [Description("Receitas operacionais")]
        ReceitasOperacionais,
        [Description("Deduções")]
        Deducoes,
        [Description("Custos")]
        Custos,
        [Description("Despesas operacionais")]
        DespesasOperacionais,
        [Description("Atividades de financiamento")]
        AtividadesFinanciamento,
        [Description("Atividades de investimento")]
        AtividadesInvestimento
    }
}