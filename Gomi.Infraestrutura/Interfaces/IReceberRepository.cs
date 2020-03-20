using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using Gomi.Infraestrutura.Models.Financeiro;
using System;
using System.Collections.Generic;


namespace Gomi.Infraestrutura.Interfaces
{
    public interface IReceberRepository : IRepository<Receber>
    {
        decimal ObterTotalEmAbertoDeContasReceberHoje();
        decimal ObterTotalEmAbertoDeContasReceberNoPeriodo(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RecebimentosCliente> ObterTotalEmAbertoDeContasReceberNoPeriodoCliente();
        IEnumerable<RecebimentosDetalhadoCliente> ObterTotalEmAbertoDeContasReceberNoPeriodoDetalhadoCliente();
        IEnumerable<RecebimentosAtrasadoCliente> ObterTotalAtrasadoDeContasReceberNoPeriodoCliente();
        IEnumerable<ContasReceberTransacionadores> ObterContasReceberTransacionadores();
        decimal ResultadoPrevistoRecebido(DateTime dataInicial, DateTime dataFinal);
        decimal ResultadoPrevistoAVencer(DateTime dataInicial, DateTime dataFinal);
        decimal ResultadoPrevistoVencido(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<Receber> ObterRecebimentos();
        IEnumerable<ListaReceber> ObterListaRecebimentos();
        IEnumerable<RelRecebimentosPorDia> ObterRelRecebimentosPorDia(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelRecebimentosPorTipoPessoa> ObterRelRecebimentosPorTipoPessoa(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelRecebimentosPorCategoria> ObterRelRecebimentosPorCategoria(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelRelacaoRecebimentos> ObterRelRelacaoRecebimentos(DateTime dataInicial, DateTime dataFinal);
    }
}
