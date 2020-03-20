namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class GastosOperacionais
    {
        public GastosOperacionais( System.DateTime mes, decimal custos, decimal deducoes, decimal despesas)
        {
           
            Mes = mes;
          
            Custos = custos;
            Deducoes = deducoes;
            Despesas = despesas;
        }

        public System.DateTime Mes { get; set; }
        public decimal Custos { get; set; }
        public decimal Deducoes { get; set; }
        public decimal Despesas { get; set; }
    }
}
