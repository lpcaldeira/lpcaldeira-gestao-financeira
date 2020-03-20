namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ReceitaOperacional
    {
        public ReceitaOperacional(decimal valor, System.DateTime mes)
        {
            Valor = valor;
            Mes = mes;
        }

        public decimal Valor { get; set; }
        public System.DateTime Mes { get; set; }
    }
}
