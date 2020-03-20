namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class Banco
    {
        public Banco(int id, int codigo, int digito, string nome)
        {
            Id = id;
            Codigo = codigo;
            Digito = digito;
            Nome = nome;
        }

        public long Id { get; protected set; }
        public int Codigo { get; protected set; }
        public int Digito { get; protected set; }
        public string Nome { get; protected set; }
    }
}