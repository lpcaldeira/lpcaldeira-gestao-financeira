using System;
using System.ComponentModel;

namespace Gomi.Infraestrutura.Models.Pessoas
{
    public class ListaPessoas
    {
        public ListaPessoas(int id, string transacionador, string razaosocial, string nome, string cnpj, string cpf, string endereco, string numero,
            string bairro, string cidade, string uf, string telefone, string pessoacontato, string email, string tipo,
             decimal emaberto, decimal vencido)
        {
            Id = id;
            Transacionador = transacionador;
            Razaosocial = razaosocial;
            Nome = nome;
            Cnpj = cnpj;
            Cpf = cpf;
            Endereco = endereco; 
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Uf = uf;
            Tel =  telefone;
            Contato = pessoacontato;
            Email_ = email;
            Tipo = tipo;
            Emaberto = emaberto;
            Vencido = vencido;
            
        }

        public int Id { get; protected set; }
        public string Transacionador { get; protected set; }
        public string Razaosocial { get; protected set; }
        public string Nome { get; protected set; }
        public string Cnpj  { get; protected set; }
        public string Cpf { get; protected set; }
        public string Endereco { get; protected set; }
        public string Numero { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string Uf { get; protected set; }
        public string Tel { get; protected set; }
        public string Telefone => !string.IsNullOrEmpty(Tel) ? $"Telefone: {Tel}" : string.Empty;
        public string Contato { get; protected set; }
        public string Pessoacontato => !string.IsNullOrEmpty(Tel) ? $"Contato: {Contato}" : string.Empty;
        public string Email_ { get; protected set; }
        public string Email => !string.IsNullOrEmpty(Email_) ? $"Email: {Email_}" : string.Empty;
        public string Tipo { get; protected set; }
        public decimal Emaberto { get; protected set; }
        public decimal Vencido { get; protected set; }

        public string Documento => !string.IsNullOrEmpty(Cnpj) ? $"CNPJ: {Cnpj}" : !string.IsNullOrEmpty(Cpf) ? $"CPF: {Cpf}" : string.Empty;

    }
}
