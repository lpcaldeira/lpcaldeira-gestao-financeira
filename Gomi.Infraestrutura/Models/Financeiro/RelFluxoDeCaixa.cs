using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class RelFluxoDeCaixa
    {
    
        public RelFluxoDeCaixa(int seq, string conta, DateTime competencia, string nomepessoa, string categoria, string descricao, decimal recebimentos,
            decimal pagamentos, decimal saldodia, decimal diferencaacumuladadia)
        {
            Seq = seq;
            Conta = conta;
            Competencia = competencia;
            Nomepessoa = nomepessoa;
            Categoria = categoria;
            Descricao = descricao;
            Recebimentos = recebimentos;
            Pagamentos = pagamentos;
            Saldodia = saldodia;
            Diferencaacumuladadia = diferencaacumuladadia;

        }

        public int Seq { get; set; }
        public string Conta { get; set; }
        public DateTime Competencia { get; set; }
        public string Nomepessoa { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Recebimentos { get; set; }
        public decimal Pagamentos { get; set; }
        public decimal Saldodia { get; set; }
        public decimal Diferencaacumuladadia { get; set; }

    }
}
