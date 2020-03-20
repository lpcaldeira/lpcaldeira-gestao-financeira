namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class FluxoResultado
    {
        public FluxoResultado(System.DateTime mes, decimal gastos, decimal receitas, decimal liquido, decimal operacional )
        {
            Mes = mes;
            Gastos = 0 - gastos;
            Receitas = receitas;
            Liquido = liquido;
            Operacional = operacional;
        }

        public System.DateTime Mes { get; set; }
        public decimal Gastos { get; set; }
        public decimal Receitas { get; set; }
        public decimal Liquido { get; set; }
        public decimal Operacional { get; set; }
        
    }
}
