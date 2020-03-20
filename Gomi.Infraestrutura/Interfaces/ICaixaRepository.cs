using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using Gomi.Infraestrutura.Models.Financeiro;
using System;
using System.Collections.Generic;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface ICaixaRepository : IRepository<Caixa>
    {
        decimal ObterResultadoOperacional(DateTime dataInicial, DateTime dataFinal);
        decimal ObterResultadoOperacionalPercentual(DateTime dataInicial, DateTime dataFinal);
        decimal ObterResultadoLiquido(DateTime dataInicial, DateTime dataFinal);
        decimal ObterResultadoLiquidoPercentual(DateTime dataInicial, DateTime dataFinal);
        decimal ObterResultadoLiquidoAno(DateTime dataInicial, DateTime dataFinal);
        decimal ObterResultadoLiquidoPercentualAno(DateTime dataInicial, DateTime dataFinal);
        decimal ObterSaldoLiquidoNoPeriodo(DateTime dataInicial, DateTime dataFinal);
        decimal ObterSaldoBrutoNoPeriodo(DateTime dataInicial, DateTime dataFinal);
        decimal ObterPercentualLucroLiquidoNoPeriodo(DateTime dataInicial, DateTime dataFinal);
        decimal ObterPercentualLucroBrutoNoPeriodo(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<FluxoResultado> ObterFluxoDeResultados(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<DistribuicaoResultado> ObterDistribuicaoResultados(DateTime dataInicial, DateTime dataFinal, DateTime dataInicial_ant, DateTime dataFinal_ant);
        IEnumerable<ComparacaoMesAnterior> ObterComparacaoMesAnterior(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<SaldoDisponibilidade> ObterSaldosDisponibilidade();
        decimal SaldoDisponibilidadeSoma();
        IEnumerable<ReceitaOperacional> ObterReceitaOperacional(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<ReceitaOperacionalGrid> ObterReceitaOperacionalGrid(DateTime dataInicial, DateTime dataFinal);
        decimal ObterReceitaOperacionalVariacao(DateTime dataInicial, DateTime dataFinal);
        decimal ObterReceitaOperacionalAcumulado(DateTime dataInicial, DateTime dataFinal);
        decimal ObterReceitaOperacionalMediaAno(DateTime dataInicial, DateTime dataFinal);
        decimal ObterDeducoesVariacao(DateTime dataInicial, DateTime dataFinal);
        decimal ObterDeducoesAcumulado(DateTime dataInicial, DateTime dataFinal);
        decimal ObterDeducoesMediaAno(DateTime dataInicial, DateTime dataFinal);
        decimal ObterCustosVariacao(DateTime dataInicial, DateTime dataFinal);
        decimal ObterCustosAcumulado(DateTime dataInicial, DateTime dataFinal);
        decimal ObterCustosMediaAno(DateTime dataInicial, DateTime dataFinal);
        decimal ObterDespesasVariacao(DateTime dataInicial, DateTime dataFinal);
        decimal ObterDespesasAcumulado(DateTime dataInicial, DateTime dataFinal);
        decimal ObterDespesasMediaAno(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<GastosOperacionais> ObterGastosOperacionais(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<GastosOperacionaisGrid> ObterGastosOperacionaisGrid(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<AnaliseFinanceira> ObterAnaliseFinanceira(DateTime dataInicial, DateTime dataFinal);
        decimal ObterMCVariacao(DateTime dataInicial, DateTime dataFinal);
        decimal ObterMCAcumulado(DateTime dataInicial, DateTime dataFinal);
        decimal ObterMCMediaAno(DateTime dataInicial, DateTime dataFinal);
        decimal ObterROVariacao(DateTime dataInicial, DateTime dataFinal);
        decimal ObterROAcumulado(DateTime dataInicial, DateTime dataFinal);
        decimal ObterROMediaAno(DateTime dataInicial, DateTime dataFinal);
        decimal ObterRLVariacao(DateTime dataInicial, DateTime dataFinal);
        decimal ObterRLAcumulado(DateTime dataInicial, DateTime dataFinal);
        decimal ObterRLMediaAno(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<PontoEquilibrioFinanceiro> ObterPontoEquilibrioFinanceiro(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<PrazoMedioRecebimento> ObterPrazoMedioRecebimento(DateTime dataInicial, DateTime dataFinal);
        int ObterPrazoMedioRecebimentosMesAtual();
        IEnumerable<PrazoMedioPagamento> ObterPrazoMedioPagamento(DateTime dataInicial, DateTime dataFinal);
        int ObterPrazoMedioPagamentosMesAtual();
        IEnumerable<PrazoMedioEstocagem> ObterPrazoMedioEstocagem(DateTime dataInicial, DateTime dataFinal);
        int ObterPrazoMedioEstocagemMesAtual();
        IEnumerable<CapitalGiroLiquido> ObterCapitalGiroLiquido(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<ProjecaoFluxoCaixa> ObterProjecaoFluxoCaixa(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<CicloFinanceiro> ObterCicloFinanceiro(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<ResultadoParaTrimestreRP> ObterResultadoParaTrimestreRP(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<ProjecaoMensal> ObterProjecaoMensalDeContasPagarReceberNoPeriodo(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<ProjecaoMensalGrid> ObterProjecaoMensalDeContasPagarReceberGrid(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelFluxoDeCaixa> ObterRelFluxoDeCaixa(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelDRE> ObterRelDRE(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelDREDetalhado> ObterRelDREDetalhado(int ano);
        IEnumerable<RelFluxoDeCaixaGeralRealizado> ObterRelFluxoDeCaixaGeralRealizado(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelFluxoDeCaixaGeralPrevisto> ObterRelFluxoDeCaixaGeralPrevisto(DateTime dataInicial, DateTime dataFinal);
        IEnumerable<RelResumoGeral> ObterRelResumoGeral(int ano);
    }
}
