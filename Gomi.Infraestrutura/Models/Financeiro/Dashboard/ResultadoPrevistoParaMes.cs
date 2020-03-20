namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ResultadoPrevistoParaMes
    {
        public ResultadoPrevistoParaMes( decimal recebido, decimal avencer, decimal vencidos, decimal previsto, decimal pago_pag, decimal avencer_pag, decimal vencidos_pag, decimal previsto_pag)

        {
            Recebido = recebido.ToString("C2");
            AVencer = avencer.ToString("C2");
            Vencidos = vencidos.ToString("C2");
            Previsto = previsto.ToString("C2");
            Pago_pag = pago_pag.ToString("C2");
            AVencer_pag = avencer_pag.ToString("C2");
            Vencidos_pag = vencidos_pag.ToString("C2");
            Previsto_pag = previsto_pag.ToString("C2");
            Resultado_previsto_ = previsto - previsto_pag;
            Resultado_realizado_ = recebido - pago_pag;
            Resultado_previsto = Resultado_previsto_.ToString("C2");
            Resultado_realizado = Resultado_realizado_.ToString("C2");


        }

        public string Recebido { get; set; }
        public string AVencer { get; set; }
        public string Vencidos { get; set; }
        public string Previsto { get; set; }
        public string Pago_pag { get; set; }
        public string AVencer_pag { get; set; }
        public string Vencidos_pag { get; set; }
        public string Previsto_pag { get; set; }
        public decimal Resultado_previsto_ { get; set; }
        public decimal Resultado_realizado_ { get; set; }
        public string Resultado_previsto { get; set; }
        public string Resultado_realizado { get; set; }

    }
}
