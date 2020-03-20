using System.ComponentModel;

namespace Gomi.Infraestrutura.Enums
{
    public enum TipoConta
    {
        [Description("Conta corrente")]
        ContaCorrente,
        [Description("Conta poupança")]
        ContaPoupança,
        [Description("Aplicação financeira")]
        AplicacaoFinanceira,
        [Description("Em espécie")]
        EmEspecie,
        [Description("Outras")]
        Outras
    }
}