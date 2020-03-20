using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IDashboardService
    {
        ResultadoOperacional ObterResultadoOperacional();
        ResultadoLiquido ObterResultadoLiquido();
        ResultadoLiquidoAno ObterResultadoLiquidoAno();
        ResultadoFinanceiro ObterResultadoFinanceiroLiquidoMesAtual();
        ResultadoFinanceiro ObterResultadoFinanceiroBrutoMesAtual();
        ResultadoFinanceiro ObterResultadoFinanceiroLiquidoAcumuladoAno();
        IEnumerable<RecebimentosCliente> ObterTotalEmAbertoDeContasReceberNoPeriodoCliente();
        IEnumerable<RecebimentosDetalhadoCliente> ObterTotalEmAbertoDeContasReceberNoPeriodoDetalhadoCliente();
        IEnumerable<RecebimentosAtrasadoCliente> ObterTotalAtrasadoDeContasReceberNoPeriodoCliente();
        IEnumerable<PagamentosPessoa> ObterTotalEmAbertoDeContasPagarNoPeriodoPessoa();
        IEnumerable<PagamentosAtrasadoPessoa> ObterTotalAtrasadoDeContasPagarNoPeriodoPessoa();
        IEnumerable<PagamentosDetalhadoPessoa> ObterTotalEmAbertoDeContasPagarNoPeriodoDetalhadoPessoa();
        PrevisaoFinanceiraHoje ObterPrevisaoFinanceiraDeHoje();
        PrevisaoFinanceiraSemana ObterPrevisaoFinanceiraDaSemana();
        PrevisaoFinanceiraProximaSemana ObterPrevisaoFinanceiraProximaSemana();
        IEnumerable<FluxoResultado> ObterFluxoDeResultados();
        IEnumerable<DistribuicaoResultado> ObterDistribuicaoResultados();
        IEnumerable<ComparacaoMesAnterior> ObterComparacaoMesAnterior();
        IEnumerable<SaldoDisponibilidade> ObterSaldosDisponibilidade();
        SaldoDisponibilidadeSoma ObterSaldoDisponibilidadeSoma();
        IEnumerable<ContasReceberTransacionadores> ObterContasReceberTransacionadores();
        IEnumerable<ContasPagarTransacionadores> ObterContasPagarTransacionadores();
        IEnumerable<ProjecaoMensal> ObterProjecaoMensalDeContasPagarReceberNoPeriodo();
        IEnumerable<ProjecaoMensalGrid> ObterProjecaoMensalDeContasPagarReceberGrid();
        IEnumerable<ReceitaOperacional> ObterReceitaOperacional();
        IEnumerable<ReceitaOperacionalGrid> ObterReceitaOperacionalGrid();
        ReceitaOperacionalOutras ObterReceitaOperacionalOutras();
        IEnumerable<GastosOperacionais> ObterGastosOperacionais();
        IEnumerable<GastosOperacionaisGrid> ObterGastosOperacionaisGrid();
        GastosOperacionaisOutras ObterGastosOperacionaisOutras();
        IEnumerable<AnaliseFinanceira> ObterAnaliseFinanceira();
        AnaliseFinanceiraOutras ObterAnaliseFinanceiraOutras();
        IEnumerable<PontoEquilibrioFinanceiro> ObterPontoEquilibrioFinanceiro();
        IEnumerable<PrazoMedioRecebimento> ObterPrazoMedioRecebimento();
        PrazoMedioRecebimentoMesAtual ObterPrazoMedioRecebimentoMesAtual();
        IEnumerable<PrazoMedioPagamento> ObterPrazoMedioPagamento();
        PrazoMedioPagamentoMesAtual ObterPrazoMedioPagamentoMesAtual();
        IEnumerable<PrazoMedioEstocagem> ObterPrazoMedioEstocagem();
        PrazoMedioEstocagemMesAtual ObterPrazoMedioEstocagemMesAtual();
        IEnumerable<CicloFinanceiro> ObterCicloFinanceiro();
        CicloFinanceiroMesAtual ObterCicloFinanceiroMesAtual();
        IEnumerable<CapitalGiroLiquido> ObterCapitalGiroLiquido();
        IEnumerable<ProjecaoFluxoCaixa> ObterProjecaoFluxoCaixa();
        ResultadoPrevistoParaMes ObterResultadoPrevistoParaMes();
        IEnumerable<ResultadoParaTrimestreRP> ObterResultadoParaTrimestreRP();
        IEnumerable<ListaReceber> ObterListaRecebimentos();
        IEnumerable<ListaPagar> ObterListaPagamentos();
        IEnumerable<ListaCategoria> ObterListaCategoria();
        IEnumerable<RelRecebimentosPorDia> ObterRelRecebimentosPorDia();
        IEnumerable<RelPagamentosPorDia> ObterRelPagamentosPorDia();
        IEnumerable<RelRecebimentosPorTipoPessoa> ObterRelRecebimentosPorTipoPessoa();
        IEnumerable<RelPagamentosPorTipoPessoa> ObterRelPagamentosPorTipoPessoa();
        IEnumerable<RelRecebimentosPorCategoria> ObterRelRecebimentosPorCategoria();
        IEnumerable<RelPagamentosPorCategoria> ObterRelPagamentosPorCategoria();
        IEnumerable<RelRelacaoRecebimentos> ObterRelRelacaoRecebimentos();
        IEnumerable<RelRelacaoPagamentos> ObterRelRelacaoPagamentos();
        IEnumerable<RelFluxoDeCaixa> ObterRelFluxoDeCaixa();
        IEnumerable<RelDRE> ObterRelDRE();
        IEnumerable<RelDREDetalhado> ObterRelDREDetalhado();
        IEnumerable<RelFluxoDeCaixaGeralRealizado> ObterRelFluxoDeCaixaGeralRealizado();
        IEnumerable<RelFluxoDeCaixaGeralPrevisto> ObterRelFluxoDeCaixaGeralPrevisto();
        IEnumerable<RelResumoGeral> ObterRelResumoGeral();

    }
}
