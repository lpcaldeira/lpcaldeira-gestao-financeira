using System.Collections.Generic;
using System;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class ExtratoConta
    {
        public ExtratoConta(string seq, DateTime dia, string pessoa, string categoria, string descricao, decimal recebimentos, 
                            decimal pagamentos, decimal saldo, decimal diferenca)
        {
            Seq = seq;
            Dia = dia;
            Pessoa = pessoa;
            Categoria = categoria;
            Descricao = descricao;
            Recebimentos = recebimentos;
            Pagamentos = pagamentos;
            Saldo = saldo;
            Diferenca = diferenca;
    
        }

        public string Seq { get;  set; }
        public DateTime Dia { get;  set; }
        public string Pessoa { get;  set; }
        public string Categoria { get;  set; }
        public string Descricao { get;  set; }
        public decimal Recebimentos { get;  set; }
        public decimal Pagamentos { get;  set; }
        public decimal Saldo { get;  set; }
        public decimal Diferenca { get;  set; }


    }
}
