using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using Gomi.Infraestrutura.Models.Financeiro;
using System;
using System.Collections.Generic;
namespace Gomi.Infraestrutura.Interfaces
{
    public interface IPagarRepository : IRepository<Pagar>
    {
        decimal ObterTotalEmAbertoDeContasPagarHoje();
        decimal ObterTotalEmAbertoDeContasPagarNoPeriodo(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<PagamentosPessoa> ObterTotalEmAbertoDeContasPagarNoPeriodoPessoa();
        IEnumerable<PagamentosAtrasadoPessoa> ObterTotalAtrasadoDeContasPagarNoPeriodoPessoa();
        IEnumerable<PagamentosDetalhadoPessoa> ObterTotalEmAbertoDeContasPagarNoPeriodoDetalhadoPessoa();
        IEnumerable<ContasPagarTransacionadores> ObterContasPagarTransacionadores();
        decimal ResultadoPrevistoPagoPagar(DateTime dataInicial, DateTime dataFinal);
        decimal ResultadoPrevistoAVencerPagar(DateTime dataInicial, DateTime dataFinal);
        decimal ResultadoPrevistoVencidoPagar(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<Pagar> ObterPagamentos();
        IEnumerable<ListaPagar> ObterListaPagamentos();
        IEnumerable<RelPagamentosPorDia> ObterRelPagamentosPorDia(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelPagamentosPorTipoPessoa> ObterRelPagamentosPorTipoPessoa(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelPagamentosPorCategoria> ObterRelPagamentosPorCategoria(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelRelacaoPagamentos> ObterRelRelacaoPagamentos(DateTime dataInicial, DateTime dataFinal);
    }
}
