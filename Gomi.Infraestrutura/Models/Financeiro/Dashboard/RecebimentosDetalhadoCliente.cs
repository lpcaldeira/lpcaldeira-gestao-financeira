
using System;
namespace Gomi.Infraestrutura.Models.Financeiro.Dashboard
{

    public class RecebimentosDetalhadoCliente
    
    {
        public RecebimentosDetalhadoCliente(string pessoa, decimal valor, DateTime vencimento,   string descricao, string situacao)
        {
            Pessoa = pessoa;
            Valor = valor;
            Vencimento = vencimento;
            Descricao = descricao;
            Situacao = situacao;

        }

        public string Pessoa { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
    }
}
