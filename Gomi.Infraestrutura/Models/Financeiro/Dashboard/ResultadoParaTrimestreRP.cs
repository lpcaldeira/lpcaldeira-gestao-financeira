namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ResultadoParaTrimestreRP
    {
        public ResultadoParaTrimestreRP( System.DateTime mes, decimal recebido, decimal avencer, decimal vencidos, decimal pago_pag, decimal avencer_pag, decimal vencidos_pag)

        {
            Mes = mes;    
            Recebido = recebido;
            AVencer = avencer;
            Vencidos = vencidos;

            Pago_pag = 0 -  pago_pag;
            AVencer_pag = 0 - avencer_pag;
            Vencidos_pag = 0 - vencidos_pag;
            Resultado = recebido + avencer - pago_pag - avencer_pag;


        }
        public System.DateTime Mes { get; set; }
        public decimal Recebido { get; set; }
        public decimal AVencer { get; set; }
        public decimal Vencidos { get; set; }
       
        public decimal Pago_pag { get; set; }
        public decimal AVencer_pag { get; set; }
        public decimal Vencidos_pag { get; set; }
        public decimal Resultado { get; set; }


    }
}
