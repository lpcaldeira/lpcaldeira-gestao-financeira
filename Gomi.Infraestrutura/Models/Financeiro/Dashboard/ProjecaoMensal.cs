namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ProjecaoMensal
    {
        public ProjecaoMensal(decimal pagar, decimal receber, decimal diferenca, System.DateTime mes, decimal diferencaacumulada)
        {
            Pagar = pagar;
            Receber = receber;
            Diferenca = diferenca;
            Mes = mes;
            Diferencaacumulada = diferencaacumulada;
        }

        public decimal Pagar { get; set; }
        public decimal Receber { get; set; }
        public decimal Diferenca { get; set; }
        public System.DateTime Mes { get; set; }
        public decimal Diferencaacumulada { get; set; }
    }
}
