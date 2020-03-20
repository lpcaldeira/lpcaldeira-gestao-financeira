using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class RelDREDetalhado
    {
    
        public RelDREDetalhado( int ordem, string grupo, string categoria, decimal jan, decimal fev, decimal mar, decimal abr, decimal mai ,
            decimal jun , decimal jul , decimal ago , decimal set , decimal oct , decimal nov , decimal dez, decimal media, decimal total )
        {
            Ordem = ordem;
            Grupo = grupo;
            Categoria = categoria;
            Jan = jan;
            Fev = fev;
            Mar = mar;
            Abr = abr;
            Mai = mai;
            Jun = jun;
            Jul = jul;
            Ago = ago;
            Set = set;
            Oct = oct;
            Nov = nov;
            Dez = dez;
            Media = media;
            Total = total;

        }

        public int Ordem { get; set; }
        public string Grupo { get; set; }
        public string Categoria { get; set; }
        public decimal Jan { get; set; }
        public decimal Fev { get; set; }
        public decimal Mar { get; set; }
        public decimal Abr { get; set; }
        public decimal Mai { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Ago { get; set; }
        public decimal Set { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dez { get; set; }
        public decimal Media { get; set; }
        public decimal Total { get; set; }

    }
}
