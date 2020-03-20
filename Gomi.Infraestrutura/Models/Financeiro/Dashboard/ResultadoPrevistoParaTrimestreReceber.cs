namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ResultadoPrevistoParaTrimestreReceber
    {
        public ResultadoPrevistoParaTrimestreReceber( string mes, decimal recebido, decimal avencer, decimal vencidos)

        {
            Mes = mes;
            Recebido = recebido;
            AVencer = avencer;
            Vencidos = vencidos;
           

        }
        public string Mes { get; set; }
        public decimal Recebido { get; set; }
        public decimal AVencer { get; set; }
        public decimal Vencidos { get; set; }
       }


    }

