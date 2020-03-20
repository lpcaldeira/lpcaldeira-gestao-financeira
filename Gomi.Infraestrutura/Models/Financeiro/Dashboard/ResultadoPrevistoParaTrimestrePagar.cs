namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ResultadoPrevistoParaTrimestrePagar
    {
        public ResultadoPrevistoParaTrimestrePagar( string mes, decimal pago_pag, decimal avencer_pag, decimal vencidos_pag)

        {
            Mes = mes;
            Pago_pag = pago_pag;
            AVencer_pag = avencer_pag;
            Vencidos_pag = vencidos_pag;
            
           

        }
        public string Mes { get; set; }
      
        public decimal Pago_pag { get; set; }
        public decimal AVencer_pag { get; set; }
        public decimal Vencidos_pag { get; set; }
       
    }


}

