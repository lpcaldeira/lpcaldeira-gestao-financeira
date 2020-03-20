using System;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using Gomi.Infraestrutura.Models.Financeiro;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Dapper.Core.Contexts.Interfaces;

namespace Gomi.Dados.Repositories
{
    public class PagarRepository : Repository<Pagar>, IPagarRepository
    {
        public PagarRepository(IDapperContext dapperContext)
            : base(dapperContext)
        { }

        public decimal ObterTotalEmAbertoDeContasPagarHoje()
        {
            return DbContext.Query<decimal>("select coalesce(sum(p.valor),0) from pagar p              " +
                                             "where p.datapagamento is null and p.vencimento = current_date").Single();
        }

        public decimal ObterTotalEmAbertoDeContasPagarNoPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(sum(p.valor),0) as valor from pagar p              " +
                                             "where p.datapagamento is null and p.vencimento between @dataInicial and @dataFinal", parametros).Single();
        }
        public IEnumerable<PagamentosPessoa> ObterTotalEmAbertoDeContasPagarNoPeriodoPessoa()
        {
            var resultado = DbContext.Query("select p.nome as pessoa, coalesce(sum(r.valor),0) as valor from pagar r inner join pessoa p on (r.idpessoa = p.id)            " +
                                             "where r.datapagamento is null  group by 1 ").ToList();
            return (from registro in resultado
                    select new PagamentosPessoa(registro.pessoa, registro.valor)).ToList();
        }

        public IEnumerable<PagamentosAtrasadoPessoa> ObterTotalAtrasadoDeContasPagarNoPeriodoPessoa()
        {
            var resultado = DbContext.Query("select p.nome as pessoa, coalesce(sum(r.valor),0) as valor from pagar r  inner join pessoa p on (r.idpessoa = p.id)            " +
                                             "where r.datapagamento is null and r.vencimento < current_date group by 1 ").ToList();
            return (from registro in resultado
                    select new PagamentosAtrasadoPessoa(registro.pessoa, registro.valor)).ToList();
        }

        public IEnumerable<PagamentosDetalhadoPessoa> ObterTotalEmAbertoDeContasPagarNoPeriodoDetalhadoPessoa()
        {
            var resultado = DbContext.Query(" select p.nome as  pessoa, coalesce(r.valor,0) as valor, r.vencimento, r.descricao, " +
                                            " 	case when r.valorpago > 0 then 'Quitado' " +
                                            " 	     when r.vencimento < current_date and r.datapagamento is null then 'Atrasado' " +
                                            " 	     when r.vencimento >= current_date and r.datapagamento  is null  then 'Em Aberto' end as situacao " +
                                            " from pagar r  inner join pessoa p on (r.idpessoa = p.id) " +
                                            " where r.datapagamento is null  " +
                                            " order by 3  ").ToList();
            return (from registro in resultado
                    select new PagamentosDetalhadoPessoa(registro.pessoa, registro.valor, registro.vencimento, registro.descricao, registro.situacao)).ToList();
        }

        public IEnumerable<ContasPagarTransacionadores> ObterContasPagarTransacionadores()
        {
            var resultado = DbContext.Query("select r.transacionador, coalesce(sum(r.valor), 0) as valor from pagar r " +
                                            "where r.datapagamento is null  group by 1").ToList();
            return (from registro in resultado
                    select new ContasPagarTransacionadores(registro.transacionador, registro.valor)).ToList();
        }

        public decimal ResultadoPrevistoPagoPagar(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(sum(r.valorpago),0) from PAGAR r where r.vencimento between  @dataInicial and @dataFinal", parametros).Single();
        }

        public decimal ResultadoPrevistoAVencerPagar(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(sum(r.valor),0) from PAGAR r             " +
                                             "where r.datapagamento is null and r.vencimento between  @dataInicial and @dataFinal ", parametros).Single();
        }

        public decimal ResultadoPrevistoVencidoPagar(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(sum(r.valor),0) from PAGAR r             " +
                                             "where r.datapagamento is null  and CAST(r.vencimento AS DATE) < current_date and  r.vencimento between  @dataInicial and @dataFinal", parametros).Single();
        }

        public IEnumerable<Pagar> ObterPagamentos()
        {
            return DbContext.Query<Pagar>("select * from pagar");
        }

        public IEnumerable<ListaPagar> ObterListaPagamentos()
        {
            var resultado = DbContext.Query(" select r.id, CAST(r.vencimento AS DATE) as vencimento, cast(coalesce(r.datapagamento,'01/01/2001') as date) as datapagamento, p.id as idpessoa, p.nome as pessoa, " +
                                                 " pc.categoria as categoria, r.descricao, r.valor, r.valorpago " +
                                                 " from pagar r inner join pessoa p on (r.idpessoa = p.id) " +
                                                 " left join planoconta pc on (pc.id = r.idplanoconta) order by 2, 3 ").ToList(); ;
            return (from registro in resultado
                    select new ListaPagar(registro.id, registro.vencimento, registro.datapagamento, registro.idpessoa, registro.pessoa, registro.categoria, registro.descricao, registro.valor, registro.valorpago)).ToList();
        }

        public IEnumerable<RelPagamentosPorDia> ObterRelPagamentosPorDia(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                             "    with pagardiario as "+
                                             "   (select r.datapagamento as competencia, " +
                                             "                  CASE EXTRACT(DOW FROM datapagamento) " +
                                             "                        WHEN 0 THEN 'Domingo' " +
                                             "                        WHEN 1 THEN 'Segunda' " +
                                             "                        WHEN 2 THEN 'Terça' " +
                                             "                        WHEN 3 THEN 'Quarta' " +
                                             "                        WHEN 4 THEN 'Quinta' " +
                                             "                        WHEN 5 THEN 'Sexta' " +
                                             "                        WHEN 6 THEN 'Sábado' " +
                                             "                  END AS diasemana, " +
                                             "                  sum(r.valorpago) as valor, " +
                                             "                  cast((sum(valorpago) / (select sum(valorpago) from pagar re where re.valorpago > 0 and " +
                                             " re.datapagamento between @dataInicial and @dataFinal)) * 100 as numeric(15, 2)) as percentual " +
                                             " from pagar r " +
                                             " where r.valorpago > 0 and r.datapagamento between @dataInicial and @dataFinal " +
                                             " group by r.datapagamento ),   " +
                                             " pagardiario2 as " +
                                             "    (select row_number() over(order by competencia) as seq,   " +
                                             "        competencia,   " +
                                             "        diasemana,   " +
                                             "        valor,  " +
                                             "        percentual " +
                                             "     from pagardiario  )  " +
                                             "    select cast(seq as int) as seq, competencia , diasemana, valor, percentual,   " +
                                             "    (select  coalesce(a.valor, 0) + coalesce(sum(b.valor), 0)  from pagardiario2 b where b.SEQ <= a.SEQ - 1  ) as valoracumulado,  " +
                                             "    (select  coalesce(a.percentual, 0) + coalesce(sum(b.percentual), 0)  from pagardiario2 b where b.SEQ <= a.SEQ - 1 ) as percentualacumulado " +
                                             "    from pagardiario2 a " +
                                             "    order by 1 ", parametros).ToList();
            return (from registro in resultado
                    select new RelPagamentosPorDia(registro.seq, registro.competencia, registro.diasemana, registro.valor, registro.percentual, registro.valoracumulado,
                    registro.percentualacumulado)).ToList();
        }

        public IEnumerable<RelPagamentosPorTipoPessoa> ObterRelPagamentosPorTipoPessoa(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                  "  select r.id, cast(p.transacionadorvalor as int) as transacionadorvalor , p.transacionador, r.datapagamento, p.nome, pc.categoria, r.descricao, r.valorpago as valor " +
                                  "  from pagar r inner " +
                                  "  join pessoa p on (r.idpessoa = p.id) " +
                                  "  inner join planoconta pc on (pc.id = r.idplanoconta) " +
                                  "  where r.datapagamento between @dataInicial and @dataFinal and r.valorpago > 0", parametros).ToList();
            return (from registro in resultado
                    select new RelPagamentosPorTipoPessoa(registro.id, registro.transacionadorvalor, registro.transacionador, registro.datapagamento, registro.nome, registro.categoria, registro.descricao, registro.valor)).ToList();
        }

        public IEnumerable<RelPagamentosPorCategoria> ObterRelPagamentosPorCategoria(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                "select pc.categoria, sum(r.valorpago) as valor, " +
                                "cast((sum(valorpago) / (select sum(valorpago) from pagar re inner join planoconta pc on (pc.id = re.idplanoconta) where re.valorpago > 0 and re.datapagamento between @dataInicial and @dataFinal)) * 100 as numeric(15, 2)) as percentual " +
                                "from pagar r inner " +
                                "join planoconta pc on (pc.id = r.idplanoconta) " +
                                "where r.datapagamento between  @dataInicial and @dataFinal and r.valorpago > 0 " +
                                "group by 1", parametros).ToList();
            return (from registro in resultado
                    select new RelPagamentosPorCategoria(registro.categoria, registro.valor, registro.percentual)).ToList();
        }

        public IEnumerable<RelRelacaoPagamentos> ObterRelRelacaoPagamentos(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(

                                " select r.datapagamento as data, " +
                                " 	p.nome, " +
                                " 	pc.categoria, " +
                                " 	r.descricao, " +
                                " 	r.valorpago as valor " +
                                " from pagar r inner join planoconta pc on (pc.id = r.idplanoconta)	 " +
                                " 	       inner join pessoa p on (p.id = r.idpessoa)	 " +
                                " where r.datapagamento between @dataInicial and @dataFinal and r.valorpago > 0 " +
                                " order by 1 "
                                , parametros).ToList();
            return (from registro in resultado
                    select new RelRelacaoPagamentos(registro.data, registro.nome, registro.categoria, registro.descricao, registro.valor)).ToList();
        }
    }
}
