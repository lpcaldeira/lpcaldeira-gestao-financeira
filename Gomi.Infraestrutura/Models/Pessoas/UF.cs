namespace Gomi.Infraestrutura.Models.Pessoas
{
    public class Uf
    {
        public Uf(int codigoIbge, string nome, string sigla)
        {
            CodigoIbge = codigoIbge;
            Nome = nome;
            Sigla = sigla;
        }

        public int CodigoIbge { get; protected set; }
        public string Nome { get; protected set; }
        public string Sigla { get; protected set; }
    }
}
