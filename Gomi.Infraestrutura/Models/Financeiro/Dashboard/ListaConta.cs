using System;


namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ListaConta
    {
        public ListaConta(int id, string nome, string numero, string tipo, decimal saldo)

        {
            Id = id;
            Nome = nome;
            Numero = numero;
            Tipo = tipo;
            Saldo = saldo;
            
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public decimal Saldo { get; set; }

    }
}
