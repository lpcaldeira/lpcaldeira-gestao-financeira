using System.ComponentModel;

namespace Gomi.Infraestrutura.Enums
{
    public enum Transacionador
    {
        [Description("Cliente")]
        Cliente,
        [Description("Fornecedor")]
        Fornecedor,
        [Description("Funcionário")]
        Funcionario,
        [Description("Sócio")]
        Socio,
        [Description("Governo")]
        Governo,
        [Description("Financiador")]
        Financiador,
        [Description("Banco")]
        Banco
    }
}