namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ProjecaoMensalGrid
    {
        public ProjecaoMensalGrid(string tipo, decimal jan, decimal fev, decimal mar, decimal abr, decimal mai, decimal jun, decimal jul, decimal ago, decimal set, decimal oct, decimal nov, decimal dez)
        {
            Tipo = tipo;
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
        }

        
        public string Tipo { get; set; }
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

    }
}
