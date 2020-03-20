using System;


namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{
    public class ContaExtrato
    {
        public ContaExtrato( decimal seq, decimal idconta, string conta, DateTime  dia  , string pessoa, string categoria, string descricao, decimal recebimentos, decimal pagamentos, decimal saldo, decimal diferencaacumuladadia)

        {
            Seq = seq;
            Idconta = idconta;
            Conta = conta; 
            Dia = dia;
            Pessoa = pessoa;
            Categoria = categoria;
            Descricao = descricao;
            Recebimentos = recebimentos;
            Pagamentos = pagamentos;
            Saldo = saldo;
            Diferencaacumuladadia = diferencaacumuladadia;
            
        }

        public decimal Seq { get; set; }
        public decimal Idconta { get; set; }
        public string Conta { get; set; }
        public DateTime Dia { get; set; }
        public string Pessoa { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Recebimentos  { get; set; }
        public decimal Pagamentos  { get; set; }
        public decimal Saldo { get; set; }
        public decimal Diferencaacumuladadia { get; set; }
 
    }
}
