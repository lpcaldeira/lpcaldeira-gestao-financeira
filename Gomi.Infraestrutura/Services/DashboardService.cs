using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using Gomi.Infraestrutura.Models.Financeiro;
using System;
using System.Collections.Generic;

namespace Gomi.Infraestrutura.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ICaixaRepository _caixaRepository;
        private readonly IReceberRepository _receberRepository;
        private readonly IPagarRepository _pagarRepository;
        private readonly IPlanoContaRepository _planocontaRepository;


        public DashboardService(ICaixaRepository caixaRepository,
            IReceberRepository receberRepository,
            IPagarRepository pagarRepository,
            IPlanoContaRepository planocontaRepository
            )
        {
            _caixaRepository = caixaRepository;
            _receberRepository = receberRepository;
            _pagarRepository = pagarRepository;
            _planocontaRepository = planocontaRepository;
        }

        public ResultadoOperacional ObterResultadoOperacional()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            var saldo = _caixaRepository.ObterResultadoOperacional(dataInicial, dataFinal);
            var percentual = _caixaRepository.ObterResultadoOperacionalPercentual(dataInicial, dataFinal);
            return new ResultadoOperacional(saldo, percentual);
        }

        public ResultadoLiquido ObterResultadoLiquido()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            var saldo = _caixaRepository.ObterResultadoLiquido(dataInicial, dataFinal);
            var percentual = _caixaRepository.ObterResultadoLiquidoPercentual(dataInicial, dataFinal);
            return new ResultadoLiquido(saldo, percentual);
        }

        public ResultadoLiquidoAno ObterResultadoLiquidoAno()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, 1, 1);


            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            var saldo = _caixaRepository.ObterResultadoLiquidoAno(dataInicial, dataFinal);
            var percentual = _caixaRepository.ObterResultadoLiquidoPercentualAno(dataInicial, dataFinal);
            return new ResultadoLiquidoAno(saldo, percentual);
        }

        public ResultadoFinanceiro ObterResultadoFinanceiroLiquidoMesAtual()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            var saldo = _caixaRepository.ObterSaldoLiquidoNoPeriodo(dataInicial, dataFinal);
            var percentualLucro = _caixaRepository.ObterPercentualLucroLiquidoNoPeriodo(dataInicial, dataFinal);
            return new ResultadoFinanceiro(saldo, percentualLucro);
        }

        public ResultadoFinanceiro ObterResultadoFinanceiroBrutoMesAtual()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            var saldo = _caixaRepository.ObterSaldoBrutoNoPeriodo(dataInicial, dataFinal);
            var percentualLucro = _caixaRepository.ObterPercentualLucroBrutoNoPeriodo(dataInicial, dataFinal);
            return new ResultadoFinanceiro(saldo, percentualLucro);
        }

        public ResultadoFinanceiro ObterResultadoFinanceiroLiquidoAcumuladoAno()
        {

            var UltimoDiaMes = new DateTime(DateTime.Now.Year, 1, 1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;

            var saldo = _caixaRepository.ObterSaldoLiquidoNoPeriodo(dataInicial, dataFinal);
            var percentualLucro = _caixaRepository.ObterPercentualLucroLiquidoNoPeriodo(dataInicial, dataFinal);
            return new ResultadoFinanceiro(saldo, percentualLucro);
        }

        public PrevisaoFinanceiraHoje ObterPrevisaoFinanceiraDeHoje()
        {
            var hoje = System.DateTime.Now;
            var totalContasReceber = _receberRepository.ObterTotalEmAbertoDeContasReceberHoje();
            var totalContasPagar = _pagarRepository.ObterTotalEmAbertoDeContasPagarHoje();
            var diferenca = totalContasReceber - totalContasPagar;

            return new PrevisaoFinanceiraHoje(totalContasReceber, totalContasPagar, diferenca, hoje);
        }

        public PrevisaoFinanceiraSemana ObterPrevisaoFinanceiraDaSemana()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(+7);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var dataInicialSemana = PrimeiroDiaMes;
            var dataFinalSemana = UltimoDiaMes;


            var totalContasReceber = _receberRepository.ObterTotalEmAbertoDeContasReceberNoPeriodo(dataInicialSemana, dataFinalSemana);

            var totalContasPagar = _pagarRepository.ObterTotalEmAbertoDeContasPagarNoPeriodo(dataInicialSemana, dataFinalSemana);
            var diferenca = totalContasReceber - totalContasPagar;
            return new PrevisaoFinanceiraSemana(totalContasReceber, totalContasPagar, diferenca, dataInicialSemana, dataFinalSemana);
        }

        public PrevisaoFinanceiraProximaSemana ObterPrevisaoFinanceiraProximaSemana()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(+14);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(+7);

            var dataInicialSemana = PrimeiroDiaMes;
            var dataFinalSemana = UltimoDiaMes;

            var totalContasReceber = _receberRepository.ObterTotalEmAbertoDeContasReceberNoPeriodo(dataInicialSemana, dataFinalSemana);
            var totalContasPagar = _pagarRepository.ObterTotalEmAbertoDeContasPagarNoPeriodo(dataInicialSemana, dataFinalSemana);
            var diferenca = totalContasReceber - totalContasPagar;

            return new PrevisaoFinanceiraProximaSemana(totalContasReceber, totalContasPagar, diferenca, dataInicialSemana, dataFinalSemana);
        }

        public IEnumerable<FluxoResultado> ObterFluxoDeResultados()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-5);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterFluxoDeResultados(dataInicial, dataFinal);

        }

        public IEnumerable<DistribuicaoResultado> ObterDistribuicaoResultados()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var UltimoDiaMes_ant = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            var PrimeiroDiaMes_ant = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;
            var dataInicial_ant = PrimeiroDiaMes_ant;
            var dataFinal_ant = UltimoDiaMes_ant;

            return _caixaRepository.ObterDistribuicaoResultados(dataInicial, dataFinal, dataInicial_ant, dataFinal_ant);
        }

        public IEnumerable<ComparacaoMesAnterior> ObterComparacaoMesAnterior()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-2);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterComparacaoMesAnterior(dataInicial, dataFinal);
        }

        public IEnumerable<SaldoDisponibilidade> ObterSaldosDisponibilidade()
        {

            return _caixaRepository.ObterSaldosDisponibilidade();
        }

        public SaldoDisponibilidadeSoma ObterSaldoDisponibilidadeSoma()
        {

            var saldo = _caixaRepository.SaldoDisponibilidadeSoma();

            return new SaldoDisponibilidadeSoma(saldo);
        }

        public IEnumerable<RecebimentosCliente> ObterTotalEmAbertoDeContasReceberNoPeriodoCliente()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-12);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _receberRepository.ObterTotalEmAbertoDeContasReceberNoPeriodoCliente();
        }

        public IEnumerable<RecebimentosAtrasadoCliente> ObterTotalAtrasadoDeContasReceberNoPeriodoCliente()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-12);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _receberRepository.ObterTotalAtrasadoDeContasReceberNoPeriodoCliente();
        }
        public IEnumerable<PagamentosPessoa> ObterTotalEmAbertoDeContasPagarNoPeriodoPessoa()
        {

            return _pagarRepository.ObterTotalEmAbertoDeContasPagarNoPeriodoPessoa();
        }

        public IEnumerable<PagamentosAtrasadoPessoa> ObterTotalAtrasadoDeContasPagarNoPeriodoPessoa()
        {

            return _pagarRepository.ObterTotalAtrasadoDeContasPagarNoPeriodoPessoa();
        }
        public IEnumerable<RecebimentosDetalhadoCliente> ObterTotalEmAbertoDeContasReceberNoPeriodoDetalhadoCliente()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(12).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-12);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;
            return _receberRepository.ObterTotalEmAbertoDeContasReceberNoPeriodoDetalhadoCliente();
        }
        public IEnumerable<PagamentosDetalhadoPessoa> ObterTotalEmAbertoDeContasPagarNoPeriodoDetalhadoPessoa()
        {

            return _pagarRepository.ObterTotalEmAbertoDeContasPagarNoPeriodoDetalhadoPessoa();
        }
        public IEnumerable<ContasReceberTransacionadores> ObterContasReceberTransacionadores()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;
            return _receberRepository.ObterContasReceberTransacionadores();
        }
        public IEnumerable<ContasPagarTransacionadores> ObterContasPagarTransacionadores()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;
            return _pagarRepository.ObterContasPagarTransacionadores();

        }

        public IEnumerable<ProjecaoMensal> ObterProjecaoMensalDeContasPagarReceberNoPeriodo()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(13).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;
            return _caixaRepository.ObterProjecaoMensalDeContasPagarReceberNoPeriodo(dataInicial, dataFinal);
        }

        public IEnumerable<ProjecaoMensalGrid> ObterProjecaoMensalDeContasPagarReceberGrid()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(12).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;
            return _caixaRepository.ObterProjecaoMensalDeContasPagarReceberGrid(dataInicial, dataFinal);
        }

        public IEnumerable<ReceitaOperacional> ObterReceitaOperacional()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterReceitaOperacional(dataInicial, dataFinal);
        }
        public IEnumerable<ReceitaOperacionalGrid> ObterReceitaOperacionalGrid()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterReceitaOperacionalGrid(dataInicial, dataFinal);
        }

        public ReceitaOperacionalOutras ObterReceitaOperacionalOutras()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;

            var variacao = _caixaRepository.ObterReceitaOperacionalVariacao(dataInicial, dataFinal);
            var mediaano = _caixaRepository.ObterReceitaOperacionalMediaAno(dataInicial, dataFinal);
            var acumulado = _caixaRepository.ObterReceitaOperacionalAcumulado(dataInicial, dataFinal);

            return new ReceitaOperacionalOutras(mediaano, acumulado, variacao);
        }


        public IEnumerable<GastosOperacionais> ObterGastosOperacionais()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);


            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterGastosOperacionais(dataInicial, dataFinal);
        }
        public IEnumerable<GastosOperacionaisGrid> ObterGastosOperacionaisGrid()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterGastosOperacionaisGrid(dataInicial, dataFinal);
        }

        public GastosOperacionaisOutras ObterGastosOperacionaisOutras()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;

            var des_variacao = _caixaRepository.ObterDespesasVariacao(dataInicial, dataFinal);
            var des_mediaano = _caixaRepository.ObterDespesasMediaAno(dataInicial, dataFinal);
            var des_acumulado = _caixaRepository.ObterDespesasAcumulado(dataInicial, dataFinal);

            var c_variacao = _caixaRepository.ObterCustosVariacao(dataInicial, dataFinal);
            var c_mediaano = _caixaRepository.ObterCustosMediaAno(dataInicial, dataFinal);
            var c_acumulado = _caixaRepository.ObterCustosAcumulado(dataInicial, dataFinal);

            var ded_variacao = _caixaRepository.ObterDeducoesVariacao(dataInicial, dataFinal);
            var ded_mediaano = _caixaRepository.ObterDeducoesMediaAno(dataInicial, dataFinal);
            var ded_acumulado = _caixaRepository.ObterDeducoesAcumulado(dataInicial, dataFinal);

            return new GastosOperacionaisOutras(ded_mediaano, ded_acumulado, ded_variacao, c_mediaano, c_acumulado, c_variacao, des_mediaano, des_acumulado, des_variacao);
        }

        public IEnumerable<AnaliseFinanceira> ObterAnaliseFinanceira()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterAnaliseFinanceira(dataInicial, dataFinal);
        }

        public AnaliseFinanceiraOutras ObterAnaliseFinanceiraOutras()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;

            var mc_variacao = _caixaRepository.ObterMCVariacao(dataInicial, dataFinal);
            var mc_mediaano = _caixaRepository.ObterMCMediaAno(dataInicial, dataFinal);
            var mc_acumulado = _caixaRepository.ObterMCAcumulado(dataInicial, dataFinal);

            var ro_variacao = _caixaRepository.ObterROVariacao(dataInicial, dataFinal);
            var ro_mediaano = _caixaRepository.ObterROMediaAno(dataInicial, dataFinal);
            var ro_acumulado = _caixaRepository.ObterROAcumulado(dataInicial, dataFinal);

            var rl_variacao = _caixaRepository.ObterRLVariacao(dataInicial, dataFinal);
            var rl_mediaano = _caixaRepository.ObterRLMediaAno(dataInicial, dataFinal);
            var rl_acumulado = _caixaRepository.ObterRLAcumulado(dataInicial, dataFinal);

            return new AnaliseFinanceiraOutras(mc_mediaano, mc_acumulado, mc_variacao, ro_mediaano, ro_acumulado, ro_variacao, rl_mediaano, rl_acumulado, rl_variacao);
        }


        public IEnumerable<PontoEquilibrioFinanceiro> ObterPontoEquilibrioFinanceiro()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-5);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterPontoEquilibrioFinanceiro(dataInicial, dataFinal);
        }
        public IEnumerable<PrazoMedioRecebimento> ObterPrazoMedioRecebimento()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterPrazoMedioRecebimento(dataInicial, dataFinal);
        }

        public PrazoMedioRecebimentoMesAtual ObterPrazoMedioRecebimentoMesAtual()
        {

            var dias = _caixaRepository.ObterPrazoMedioRecebimentosMesAtual();

            return new PrazoMedioRecebimentoMesAtual(dias);
        }

        public IEnumerable<PrazoMedioPagamento> ObterPrazoMedioPagamento()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterPrazoMedioPagamento(dataInicial, dataFinal);
        }

        public PrazoMedioPagamentoMesAtual ObterPrazoMedioPagamentoMesAtual()
        {

            var dias = _caixaRepository.ObterPrazoMedioPagamentosMesAtual();

            return new PrazoMedioPagamentoMesAtual(dias);
        }

        public IEnumerable<CapitalGiroLiquido> ObterCapitalGiroLiquido()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-6);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+6).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterCapitalGiroLiquido(dataInicial, dataFinal);
        }
        public IEnumerable<ProjecaoFluxoCaixa> ObterProjecaoFluxoCaixa()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-1);
            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;
            return _caixaRepository.ObterProjecaoFluxoCaixa(dataInicial, dataFinal);
        }

        public IEnumerable<PrazoMedioEstocagem> ObterPrazoMedioEstocagem()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterPrazoMedioEstocagem(dataInicial, dataFinal);
        }

        public PrazoMedioEstocagemMesAtual ObterPrazoMedioEstocagemMesAtual()
        {

            var dias = _caixaRepository.ObterPrazoMedioEstocagemMesAtual();

            return new PrazoMedioEstocagemMesAtual(dias);
        }

        public IEnumerable<CicloFinanceiro> ObterCicloFinanceiro()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _caixaRepository.ObterCicloFinanceiro(dataInicial, dataFinal);
        }

        public CicloFinanceiroMesAtual ObterCicloFinanceiroMesAtual()
        {

            var dias = _caixaRepository.ObterPrazoMedioEstocagemMesAtual() + _caixaRepository.ObterPrazoMedioRecebimentosMesAtual() - _caixaRepository.ObterPrazoMedioPagamentosMesAtual();

            return new CicloFinanceiroMesAtual(dias);
        }

        public ResultadoPrevistoParaMes ObterResultadoPrevistoParaMes()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            var recebido = _receberRepository.ResultadoPrevistoRecebido(dataInicial, dataFinal);
            var avencer = _receberRepository.ResultadoPrevistoAVencer(dataInicial, dataFinal);
            var vencidos = _receberRepository.ResultadoPrevistoVencido(dataInicial, dataFinal);
            var previsto = recebido + avencer + vencidos;
            var pago_pag = _pagarRepository.ResultadoPrevistoPagoPagar(dataInicial, dataFinal);
            var avencer_pag = _pagarRepository.ResultadoPrevistoAVencerPagar(dataInicial, dataFinal);
            var vencidos_pag = _pagarRepository.ResultadoPrevistoVencidoPagar(dataInicial, dataFinal);
            var previsto_pag = pago_pag + avencer_pag + vencidos_pag;
            return new ResultadoPrevistoParaMes(recebido, avencer, vencidos, previsto, pago_pag, avencer_pag, vencidos_pag, previsto_pag);
        }

        public IEnumerable<ResultadoParaTrimestreRP> ObterResultadoParaTrimestreRP()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;
            return _caixaRepository.ObterResultadoParaTrimestreRP(dataInicial, dataFinal);

        }

        public IEnumerable<ListaReceber> ObterListaRecebimentos()
        {
            return _receberRepository.ObterListaRecebimentos();
        }

        public IEnumerable<ListaPagar> ObterListaPagamentos()
        {
            return _pagarRepository.ObterListaPagamentos();
        }

        public IEnumerable<ListaCategoria> ObterListaCategoria()
        {
            return _planocontaRepository.ObterListaCategoria();
        }

        public IEnumerable<RelRecebimentosPorDia> ObterRelRecebimentosPorDia()

        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _receberRepository.ObterRelRecebimentosPorDia(dataInicial, dataFinal);
        }

        public IEnumerable<RelPagamentosPorDia> ObterRelPagamentosPorDia()

        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _pagarRepository.ObterRelPagamentosPorDia(dataInicial, dataFinal);
        }

        public IEnumerable<RelRecebimentosPorTipoPessoa> ObterRelRecebimentosPorTipoPessoa()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _receberRepository.ObterRelRecebimentosPorTipoPessoa(dataInicial, dataFinal);
        }

        public IEnumerable<RelPagamentosPorTipoPessoa> ObterRelPagamentosPorTipoPessoa()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _pagarRepository.ObterRelPagamentosPorTipoPessoa(dataInicial, dataFinal);
        }

        public IEnumerable<RelRecebimentosPorCategoria> ObterRelRecebimentosPorCategoria()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _receberRepository.ObterRelRecebimentosPorCategoria(dataInicial, dataFinal);
        }

        public IEnumerable<RelPagamentosPorCategoria> ObterRelPagamentosPorCategoria()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _pagarRepository.ObterRelPagamentosPorCategoria(dataInicial, dataFinal);
        }

        public IEnumerable<RelRelacaoRecebimentos> ObterRelRelacaoRecebimentos()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _receberRepository.ObterRelRelacaoRecebimentos(dataInicial, dataFinal);
        }

        public IEnumerable<RelRelacaoPagamentos> ObterRelRelacaoPagamentos()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _pagarRepository.ObterRelRelacaoPagamentos(dataInicial, dataFinal);
        }
        public IEnumerable<RelFluxoDeCaixa> ObterRelFluxoDeCaixa()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _caixaRepository.ObterRelFluxoDeCaixa(dataInicial, dataFinal);
        }

        public IEnumerable<RelDRE> ObterRelDRE()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _caixaRepository.ObterRelDRE(dataInicial, dataFinal);
        }
        
        public IEnumerable<RelDREDetalhado> ObterRelDREDetalhado()
        {
            var ano = 2018;
            return _caixaRepository.ObterRelDREDetalhado(ano);
        }

        public IEnumerable<RelFluxoDeCaixaGeralRealizado> ObterRelFluxoDeCaixaGeralRealizado()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _caixaRepository.ObterRelFluxoDeCaixaGeralRealizado(dataInicial, dataFinal);
        }

        public IEnumerable<RelFluxoDeCaixaGeralPrevisto> ObterRelFluxoDeCaixaGeralPrevisto()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(+2).AddDays(-1);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var dataInicial = PrimeiroDiaMes;
            var dataFinal = UltimoDiaMes;

            return _caixaRepository.ObterRelFluxoDeCaixaGeralPrevisto(dataInicial, dataFinal);
        }
        
        public IEnumerable<RelResumoGeral> ObterRelResumoGeral()
        {
            var ano = 2018;
            return _caixaRepository.ObterRelResumoGeral(ano);
        }

    }

}



