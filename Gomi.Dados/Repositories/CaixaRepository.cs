using System;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Core.Contexts.Interfaces;

namespace Gomi.Dados.Repositories
{
    public class CaixaRepository : Repository<Caixa>, ICaixaRepository
    {
        public CaixaRepository(IDapperContext dapperContext)
            : base(dapperContext)
        {
        }

        public decimal ObterResultadoOperacional(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);
            return DbContext.Query<decimal>("select coalesce(sum(coalesce(c.credito,0)) - sum(coalesce(c.debito, 0)),0) as saldo      " +
                                             "from caixa c   inner join planoconta pc on (c.idplanoconta = pc.id)                                             " +
                                             "where c.datacompetencia between @dataInicial and @dataFinal and pc.descricao not in ('Atividades de investimento','Atividades de financiamento') ", parametros).SingleOrDefault();
        }

        public decimal ObterResultadoOperacionalPercentual(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);
            return DbContext.Query<decimal>("select case when sum(coalesce(receita,0)) = 0 then 0 else  coalesce(cast(((sum(coalesce(saldo,0)) * 100)/ sum(coalesce(receita,0))) as numeric (15,2)),0.00) end as percentual " +
                                            " from( " +
                                            " select coalesce(sum(coalesce(c.credito,0) - coalesce(c.debito,0)), 0) as saldo, case when pc.descricao = 'Receitas operacionais' then sum(coalesce(c.credito,0)) else 0 end as receita " +
                                            " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)" +
                                            " where c.datacompetencia between @dataInicial and @dataFinal and  pc.descricao not in ('Atividades de investimento', 'Atividades de financiamento') " +
                                            " group by pc.descricao) as resultado", parametros).SingleOrDefault();
        }


        public decimal ObterResultadoLiquido(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);
            return DbContext.Query<decimal>("select  coalesce(sum(coalesce(c.credito,0)) - sum(coalesce(c.debito,0)),0) as saldo      " +
                                             "from caixa c                                                " +
                                             "where c.datacompetencia between @dataInicial and @dataFinal  ", parametros).SingleOrDefault();
        }

        public decimal ObterResultadoLiquidoPercentual(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);
            return DbContext.Query<decimal>("select case when sum(coalesce(receita,0)) = 0 then 0 else coalesce( cast( ( ( sum(coalesce(saldo,0)) * 100)/ sum(coalesce(receita,0)) ) as numeric (15,2)),0) end as percentual " +
                                            " from( " +
                                            " select sum(coalesce(c.credito,0)) - sum(coalesce(c.debito,0)) as saldo, case when pc.descricao = 'Receitas operacionais' then sum(coalesce(c.credito,0)) else 0 end as receita " +
                                            " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)" +
                                            " where c.datacompetencia between @dataInicial and @dataFinal  " +
                                            " group by pc.descricao) as resultado", parametros).SingleOrDefault();
        }

        public decimal ObterResultadoLiquidoAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);
            return DbContext.Query<decimal>("select coalesce( sum(coalesce(c.credito,0)) - sum(coalesce(c.debito,0)), 0) as saldo      " +
                                             "from caixa c                                                " +
                                             "where c.datacompetencia between @dataInicial and @dataFinal  ", parametros).SingleOrDefault();
        }

        public decimal ObterResultadoLiquidoPercentualAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);
            return DbContext.Query<decimal>("select case when sum(coalesce(receita,0)) = 0 then 0 else coalesce(cast(( (sum(coalesce(saldo,0)) * 100) / sum(coalesce(receita,0)) ) as numeric (15,2)),0) end as percentual " +
                                            " from( " +
                                            " select coalesce( sum(coalesce(c.credito,0)) - sum(coalesce(c.debito,0)), 0) as saldo, case when pc.descricao = 'Receitas operacionais' then sum(coalesce(c.credito,0)) else 0 end as receita " +
                                            " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)" +
                                            " where c.datacompetencia between @dataInicial and @dataFinal  " +
                                            " group by pc.descricao) as resultado", parametros).SingleOrDefault();
        }

        public decimal ObterSaldoLiquidoNoPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce( sum(coalesce(c.credito,0)) - sum(coalesce(c.debito,0)), 0) as saldo      " +
                                             "from caixa c                                                " +
                                             "where c.datacompetencia between @dataInicial and @dataFinal ", parametros).SingleOrDefault();
        }

        public decimal ObterSaldoBrutoNoPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce( sum(coalesce(c.credito,0)) - sum(coalesce(c.debito,0)), 0) as saldo      " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)                                               " +
                                             "where c.datacompetencia between @dataInicial and @dataFinal and pc.descricao not in ('Atividades de investimento','Atividades de financiamento') ", parametros).SingleOrDefault();
        }

        public decimal ObterPercentualLucroLiquidoNoPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(cast(((sum(coalesce(c.credito,0)) - sum(coalesce(c.debito,0)) - sum(coalesce(c.credito,0))) * 100) as numeric(15, 2)), 0) as saldo  " +
                                             "from caixa c                                                                                               " +
                                             "where c.datacompetencia between @dataInicial and @dataFinal", parametros).Single();
        }

        public decimal ObterPercentualLucroBrutoNoPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(cast(((sum(coalesce(c.credito,0)) - sum(coalesce(c.debito,0)) / sum(coalesce(c.credito,0))) * 100) as numeric(15, 2)), 0) as saldo  " +
                                             "from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)                                                                                             " +
                                             "where c.datacompetencia between @dataInicial and @dataFinal and pc.descricao not in ('Atividades de investimento','Atividades de financiamento') ", parametros).Single();
        }

        public IEnumerable<FluxoResultado> ObterFluxoDeResultados(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select  mes, " +
                                            " 	sum(coalesce(gastos,0)) as gastos,  " +
                                            " 	sum(coalesce(receitas,0)) as receitas, " +
                                            " 	sum(coalesce(liquido,0)) as liquido,  " +
                                            " 	sum(coalesce(receitasoper,0) - coalesce(despesas,0)) as operacional  " +
                                            " from(  select sum(coalesce(c.debito, 0)) as gastos, " +
                                            " 	      sum(coalesce(c.credito, 0)) as receitas, " +
                                            " 	      sum(coalesce(c.credito, 0) - coalesce(c.debito, 0)) as liquido,  " +
                                            "               case when pc.descricao in ('Custos', 'Deduções', 'Despesas operacionais')then  sum(coalesce(c.debito, 0)) end as despesas,  " +
                                            "               case when pc.descricao in ('Receitas operacionais') then sum(coalesce(c.credito,0)) end as receitasoper, " +
                                            "               cast( '01'  || '/' ||  EXTRACT(month from c.datacompetencia) ||'/'|| EXTRACT(year from c.datacompetencia) as date) as mes " +
                                            "        from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                            "        where c.datacompetencia between @dataInicial and @dataFinal " +
                                            "        group by  c.datacompetencia, pc.descricao) as fluxo  " +
                                            " group by 1  " +
                                            " order by 1 desc ", parametros).ToList();
            return (from registro in resultado
                    select new FluxoResultado(registro.mes, registro.gastos, registro.receitas, registro.liquido, registro.operacional)).ToList();
        }


        public IEnumerable<ComparacaoMesAnterior> ObterComparacaoMesAnterior(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query("select coalesce(pc.descricao,'Nao imformado') as descricaoplanoconta,                                          " +
                                             "       sum(coalesce(c.credito,0) + coalesce(c.debito,0)) as saldo,                    " +
                                              "       case when EXTRACT(month from c.datacompetencia) = 1 then 'Jan'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 2 then 'Fev'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 3 then 'Mar'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 4 then 'Abr'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 5 then 'Mai'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 6 then 'Jun'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 7 then 'Jul'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 8 then 'Ago'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 9 then 'Set'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 10 then 'Out'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 11 then 'Nov'" +
                                                     "            when EXTRACT(month from c.datacompetencia) = 12 then 'Dez'" +
                                                     "else 'nda' end as mes         " +
                                             "from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)                                                " +
                                             "where c.datacompetencia  between @dataInicial and @dataFinal " +
                                             "group by 1, 3", parametros).ToList();
            return (from registro in resultado
                    select new ComparacaoMesAnterior(registro.descricaoplanoconta, registro.saldo, registro.mes)).ToList();
        }

        public IEnumerable<SaldoDisponibilidade> ObterSaldosDisponibilidade()
        {
            var resultado = DbContext.Query("select  co.id, co.nome as conta,                                    " +
                                             "       coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) as saldo                   " +
                                             "from conta co left join caixa c    on (c.idconta = co.id)                                              " +
                                             "group by 1,2 order by 2").ToList();
            return (from registro in resultado
                    select new SaldoDisponibilidade(registro.id, registro.conta, registro.saldo)).ToList();
        }
        public decimal SaldoDisponibilidadeSoma()
        {
            return DbContext.Query<decimal>("select coalesce(sum(c.credito),0) - coalesce(sum(c.debito), 0) as saldo      " +
                                             "from caixa c                                                ").SingleOrDefault();
        }

        public IEnumerable<ReceitaOperacional> ObterReceitaOperacional(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query("select coalesce(sum(credito),0) as valor, " +
                                            "       cast( '01'  || '/' ||  EXTRACT(month from c.datacompetencia) ||'/'|| EXTRACT(year from c.datacompetencia) as date) as mes " +
                                            "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                            "where pc.descricao = 'Receitas operacionais' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                                            "group by EXTRACT(month from c.datacompetencia),EXTRACT(year from c.datacompetencia) " +
                                            "order by EXTRACT(year from c.datacompetencia) desc,EXTRACT(month from c.datacompetencia)desc ", parametros).ToList();
            return (from registro in resultado
                    select new ReceitaOperacional(registro.valor, registro.mes)).ToList();
        }

        public IEnumerable<ReceitaOperacionalGrid> ObterReceitaOperacionalGrid(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query("select  coalesce(sum(Jan),0) as jan, " +
                        " coalesce(sum(Fev), 0) as fev, " +
                        " coalesce(sum(Mar), 0) as mar, " +
                        " coalesce(sum(Abr), 0) as abr, " +
                        " coalesce(sum(Mai), 0) as mai, " +
                        " coalesce(sum(Jun), 0) as jun, " +
                        " coalesce(sum(Jul), 0) as jul, " +
                        " coalesce(sum(Ago), 0) as ago, " +
                        " coalesce(sum(Set), 0) as set, " +
                        " coalesce(sum(Out), 0) as oct, " +
                        " coalesce(sum(Nov), 0) as nov, " +
                        " coalesce(sum(Dez), 0) as dez " +
                        "  from(  " +
                        " select case when EXTRACT(month from c.datacompetencia) = 1 then sum(c.credito) end as Jan, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 2 then sum(c.credito) end as Fev, " +
                        "   case when EXTRACT(month from c.datacompetencia) = 3 then sum(c.credito) end as Mar, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 4 then sum(c.credito) end as Abr, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 5 then sum(c.credito) end as Mai, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 6 then sum(c.credito) end as Jun, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 7 then sum(c.credito) end as Jul, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 8 then sum(c.credito) end as Ago, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 9 then sum(c.credito) end as Set, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 10 then sum(c.credito) end as Out, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 11 then sum(c.credito) end as Nov, " +
                        "    case when EXTRACT(month from c.datacompetencia) = 12 then sum(c.credito) end as Dez " +
                        "     from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                        " where pc.descricao = 'Receitas operacionais' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                        " group by c.datacompetencia order by c.datacompetencia) as receita", parametros).ToList();
            return (from registro in resultado
                    select new ReceitaOperacionalGrid(registro.jan, registro.fev, registro.mar, registro.abr, registro.mai, registro.jun, registro.jul, registro.ago, registro.set, registro.oct, registro.nov, registro.dez)).ToList();
        }

        public decimal ObterReceitaOperacionalMediaAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>(" select case when sum(c.credito) is null then 0 else cast(sum(c.credito) /12 as numeric(15,2)) end as mediaano " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                             "where pc.descricao = 'Receitas operacionais' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterReceitaOperacionalAcumulado(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select  coalesce(sum(c.credito),0) as acumulado " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)  " +
                                             "where pc.descricao = 'Receitas operacionais' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterReceitaOperacionalVariacao(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(mesatual,0) - coalesce(mesanterior,0) as variacao from(  " +
                        " select  case when EXTRACT(month from current_date) = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) = 11 then coalesce(sum(Nov), 0) " +
                        "          when EXTRACT(month from current_date) = 12 then coalesce(sum(Dez), 0) end as mesatual, " +
                        "     case when EXTRACT(month from current_date) - 1 = 0 then coalesce(sum(Dez), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 11 then coalesce(sum(Nov), 0) end as mesanterior " +
                        "          from(  " +
                        " select case when EXTRACT(month from c.datacompetencia) = 1 then sum(c.credito) end as Jan, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 2 then sum(c.credito) end as Fev, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 3 then sum(c.credito) end as Mar, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 4 then sum(c.credito) end as Abr, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 5 then sum(c.credito) end as Mai, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 6 then sum(c.credito) end as Jun, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 7 then sum(c.credito) end as Jul, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 8 then sum(c.credito) end as Ago, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 9 then sum(c.credito) end as Set, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 10 then sum(c.credito) end as Out, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 11 then sum(c.credito) end as Nov, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 12 then sum(c.credito) end as Dez " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                       "  where pc.descricao = 'Receitas operacionais' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                       " group by c.datacompetencia) as receita) as rec", parametros).SingleOrDefault();
        }

        public IEnumerable<GastosOperacionais> ObterGastosOperacionais(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select  mes, " +
                                            " 	sum(coalesce(custos,0)) as custos, " +
                                            " 	sum(coalesce(Deducoes,0)) as Deducoes, " +
                                            " 	sum(coalesce(despesas,0)) as despesas " +
                                            " from( " +
                                            " select case when pc.descricao = 'Custos' then sum(debito) else 0 end as Custos,  " +
                                            "         case when pc.descricao = 'Deduções' then sum(debito) else 0 end as Deducoes, " +
                                            "         case when pc.descricao = 'Despesas operacionais' then sum(debito) else 0 end as Despesas,  " +
                                            "         cast( '01'  || '/' ||  EXTRACT(month from c.datacompetencia) ||'/'|| EXTRACT(year from c.datacompetencia) as date) as mes  " +
                                            " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                            " where pc.descricao in ('Deduções','Custos','Despesas operacionais') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal  " +
                                            " group by EXTRACT(month from c.datacompetencia),EXTRACT(year from c.datacompetencia), pc.descricao  " +
                                            " order by EXTRACT(year from c.datacompetencia) desc, EXTRACT(month from c.datacompetencia)desc) as gastos " +
                                            " group by 1 " +
                                            " order by 1 desc ", parametros).ToList();
            return (from registro in resultado
                    select new GastosOperacionais(registro.mes, registro.custos, registro.deducoes, registro.despesas)).ToList();
        }
        public IEnumerable<GastosOperacionaisGrid> ObterGastosOperacionaisGrid(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query("select descricaoplanoconta, sum(Jan)as Jan, sum(Fev) as Fev, sum(Mar) as Mar, sum(Abr) as Abr, " +
                                             "sum(Mai) as Mai, sum(Jun) as Jun, sum(Jul) as Jul,                                            " +
                                             "sum(Ago) as Ago, sum(Set) as Set, sum(Oct) as Oct, sum(Nov) as Nov, sum(Dez) as Dez from( " +
                                             "select pc.descricao as descricaoplanoconta,                                                                " +
                                             "case when EXTRACT(month from c.datacompetencia) = 1 then sum(coalesce(c.debito,0)) else 0 end as Jan,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 2 then sum(coalesce(c.debito,0)) else 0 end as Fev,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 3 then sum(coalesce(c.debito,0)) else 0 end as Mar,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 4 then sum(coalesce(c.debito,0)) else 0 end as Abr,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 5 then sum(coalesce(c.debito,0)) else 0 end as Mai,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 6 then sum(coalesce(c.debito,0)) else 0 end as Jun,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 7 then sum(coalesce(c.debito,0)) else 0 end as Jul,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 8 then sum(coalesce(c.debito,0)) else 0 end as Ago,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 9 then sum(coalesce(c.debito,0)) else 0 end as Set,    " +
                                             "case when EXTRACT(month from c.datacompetencia) = 10 then sum(coalesce(c.debito,0)) else 0 end as Oct,   " +
                                             "case when EXTRACT(month from c.datacompetencia) = 11 then sum(coalesce(c.debito,0)) else 0 end as Nov,   " +
                                             "case when EXTRACT(month from c.datacompetencia) = 12 then sum(coalesce(c.debito,0)) else 0 end as Dez    " +
                                             "from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                             "where pc.descricao in ('Deduções', 'Custos', 'Despesas operacionais') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                                             "group by c.datacompetencia, c.descricaoplanoconta order by c.datacompetencia) as  cx " +
                                             "group by descricaoplanoconta", parametros).ToList();
            return (from registro in resultado
                    select new GastosOperacionaisGrid(registro.descricaoplanoconta, registro.jan, registro.fev, registro.mar, registro.abr, registro.mai, registro.jun, registro.jul, registro.ago, registro.set, registro.oct, registro.nov, registro.dez)).ToList();
        }

        public decimal ObterDeducoesMediaAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>(" select case when sum(c.debito) is null then 0 else cast(sum(c.debito) /12 as numeric(15,2)) end as mediaano " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                             "where pc.descricao = 'Deduções' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterDeducoesAcumulado(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select  coalesce(sum(c.debito),0) as acumulado " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                             "where pc.descricao = 'Deduções' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterDeducoesVariacao(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select mesatual - mesanterior as variacao from(  " +
                        " select  case when EXTRACT(month from current_date) = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) = 11 then coalesce(sum(Nov), 0) " +
                        "          when EXTRACT(month from current_date) = 12 then coalesce(sum(Dez), 0) end as mesatual, " +
                        "     case when EXTRACT(month from current_date) - 1 = 0 then coalesce(sum(Dez), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 11 then coalesce(sum(Nov), 0) end as mesanterior " +
                        "          from(  " +
                        " select case when EXTRACT(month from c.datacompetencia) = 1 then sum(c.debito) end as Jan, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 2 then sum(c.debito) end as Fev, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 3 then sum(c.debito) end as Mar, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 4 then sum(c.debito) end as Abr, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 5 then sum(c.debito) end as Mai, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 6 then sum(c.debito) end as Jun, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 7 then sum(c.debito) end as Jul, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 8 then sum(c.debito) end as Ago, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 9 then sum(c.debito) end as Set, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 10 then sum(c.debito) end as Out, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 11 then sum(c.debito) end as Nov, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 12 then sum(c.debito) end as Dez " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                       "  where pc.descricao = 'Deduções' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                       " group by c.datacompetencia) as receita) as rec", parametros).SingleOrDefault();
        }


        public decimal ObterCustosMediaAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>(" select case when sum(c.debito) is null then 0 else cast(sum(c.debito) /12 as numeric(15,2)) end as mediaano " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)  " +
                                             "where pc.descricao = 'Custos' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterCustosAcumulado(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select  coalesce(sum(c.debito),0) as acumulado " +
                                             "from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)" +
                                             "where pc.descricao = 'Custos' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterCustosVariacao(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select mesatual - mesanterior as variacao from(  " +
                        " select  case when EXTRACT(month from current_date) = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) = 11 then coalesce(sum(Nov), 0) " +
                        "          when EXTRACT(month from current_date) = 12 then coalesce(sum(Dez), 0) end as mesatual, " +
                        "     case when EXTRACT(month from current_date) - 1 = 0 then coalesce(sum(Dez), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 11 then coalesce(sum(Nov), 0) end as mesanterior " +
                        "          from(  " +
                        " select case when EXTRACT(month from c.datacompetencia) = 1 then sum(c.debito) end as Jan, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 2 then sum(c.debito) end as Fev, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 3 then sum(c.debito) end as Mar, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 4 then sum(c.debito) end as Abr, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 5 then sum(c.debito) end as Mai, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 6 then sum(c.debito) end as Jun, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 7 then sum(c.debito) end as Jul, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 8 then sum(c.debito) end as Ago, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 9 then sum(c.debito) end as Set, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 10 then sum(c.debito) end as Out, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 11 then sum(c.debito) end as Nov, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 12 then sum(c.debito) end as Dez " +
                        " from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)" +
                       "  where pc.descricao = 'Custos' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                       " group by c.datacompetencia) as receita) as rec", parametros).SingleOrDefault();
        }

        public decimal ObterDespesasMediaAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>(" select case when sum(c.debito) is null then 0 else cast(sum(c.debito) /12 as numeric(15,2)) end as mediaano " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)  " +
                                             "where pc.descricao = 'Despesas operacionais' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterDespesasAcumulado(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select  coalesce(sum(c.debito),0) as acumulado " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)  " +
                                             "where pc.descricao = 'Despesas operacionais' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterDespesasVariacao(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select mesatual - mesanterior as variacao from(  " +
                        " select  case when EXTRACT(month from current_date) = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) = 11 then coalesce(sum(Nov), 0) " +
                        "          when EXTRACT(month from current_date) = 12 then coalesce(sum(Dez), 0) end as mesatual, " +
                        "     case when EXTRACT(month from current_date) - 1 = 0 then coalesce(sum(Dez), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 11 then coalesce(sum(Nov), 0) end as mesanterior " +
                        "          from(  " +
                        " select case when EXTRACT(month from c.datacompetencia) = 1 then sum(c.debito) end as Jan, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 2 then sum(c.debito) end as Fev, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 3 then sum(c.debito) end as Mar, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 4 then sum(c.debito) end as Abr, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 5 then sum(c.debito) end as Mai, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 6 then sum(c.debito) end as Jun, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 7 then sum(c.debito) end as Jul, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 8 then sum(c.debito) end as Ago, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 9 then sum(c.debito) end as Set, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 10 then sum(c.debito) end as Out, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 11 then sum(c.debito) end as Nov, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 12 then sum(c.debito) end as Dez " +
                        " from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)" +
                       "  where pc.descricao = 'Despesas operacionais' and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                       " group by c.datacompetencia) as receita) as rec", parametros).SingleOrDefault();
        }

        public IEnumerable<AnaliseFinanceira> ObterAnaliseFinanceira(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select mes, " +
                                            "         sum(coalesce(receitasoperacionais, 0)) as receitasoperacionais,  " +
                                            "         sum(coalesce(margemcontribuicao, 0)) as margemcontribuicao,  " +
                                            "         sum(coalesce(resultadooperacional, 0)) as resultadooperacional, " +
                                            "         sum(resultadofinanceiro) as resultadofinanceiro  " +
                                            " from( " +
                                            "        select cast( '01'  || '/' ||  EXTRACT(month from c.datacompetencia) ||'/'|| EXTRACT(year from c.datacompetencia) as date) as mes,  " +
                                            "        case when pc.descricao in ('Receitas operacionais') then sum(coalesce(credito, 0)) end as receitasoperacionais, + " +
                                            "        case when pc.descricao in ('Receitas operacionais', 'Deduções', 'Custos') then sum(coalesce(credito, 0)) - sum(coalesce(debito, 0)) else 0 end as margemcontribuicao,  " +
                                            "        case when pc.descricao in ('Receitas operacionais', 'Deduções', 'Custos', 'Despesas operacionais') then sum(coalesce(credito, 0)) - sum(coalesce(debito, 0)) else 0 end as resultadooperacional,  " +
                                            "        case when pc.descricao in ('Receitas operacionais', 'Deduções', 'Custos', 'Despesas operacionais', 'Atividades de financiamento', 'Atividades de investimento') then sum(coalesce(credito, 0)) - sum(coalesce(debito, 0)) else 0 end as resultadofinanceiro  " +
                                            "        from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)" +
                                            "        where cast(c.datacompetencia as date) between @dataInicial and @dataFinal  " +
                                            "        group by EXTRACT(month from c.datacompetencia),EXTRACT(year from c.datacompetencia), pc.descricao) as cx  " +
                                            " group by mes " +
                                            " order by mes desc ", parametros).ToList();
            return (from registro in resultado
                    select new AnaliseFinanceira(registro.mes, registro.receitasoperacionais, registro.margemcontribuicao, registro.resultadooperacional, registro.resultadofinanceiro)).ToList();
        }

        public decimal ObterMCMediaAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>(" select case when sum(c.credito - c.debito) is null then 0 else cast(sum(c.credito - c.debito) /12 as numeric(15,2)) end as mediaano " +
                                             "from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)" +
                                             "where pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterMCAcumulado(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select  coalesce(sum(c.credito - c.debito),0) as acumulado " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                             "where pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterMCVariacao(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select mesatual - mesanterior as variacao from(  " +
                        " select  case when EXTRACT(month from current_date) = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) = 11 then coalesce(sum(Nov), 0) " +
                        "          when EXTRACT(month from current_date) = 12 then coalesce(sum(Dez), 0) end as mesatual, " +
                        "     case when EXTRACT(month from current_date) - 1 = 0 then coalesce(sum(Dez), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 11 then coalesce(sum(Nov), 0) end as mesanterior " +
                        "          from(  " +
                        " select case when EXTRACT(month from c.datacompetencia) = 1 then sum(c.credito - c.debito) end as Jan, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 2 then sum(c.credito - c.debito) end as Fev, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 3 then sum(c.credito - c.debito) end as Mar, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 4 then sum(c.credito - c.debito) end as Abr, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 5 then sum(c.credito - c.debito) end as Mai, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 6 then sum(c.credito - c.debito) end as Jun, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 7 then sum(c.credito - c.debito) end as Jul, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 8 then sum(c.credito - c.debito) end as Ago, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 9 then sum(c.credito - c.debito) end as Set, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 10 then sum(c.credito - c.debito) end as Out, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 11 then sum(c.credito - c.debito) end as Nov, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 12 then sum(c.credito - c.debito) end as Dez " +
                        " from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)" +
                       "  where pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                       " group by c.datacompetencia) as receita) as rec", parametros).SingleOrDefault();
        }


        public decimal ObterROMediaAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>(" select case when sum(c.credito - c.debito) is null then 0 else cast(sum(c.credito - c.debito) /12 as numeric(15,2)) end as mediaano " +
                                             "from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)" +
                                             "where pc.descricao in ('Receitas operacionais','Deduções','Custos','Despesas operacionais') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterROAcumulado(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select  coalesce(sum(c.credito - c.debito),0) as acumulado " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                             "where pc.descricao in ('Receitas operacionais','Deduções','Custos','Despesas operacionais') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterROVariacao(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select mesatual - mesanterior as variacao from(  " +
                        " select  case when EXTRACT(month from current_date) = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) = 11 then coalesce(sum(Nov), 0) " +
                        "          when EXTRACT(month from current_date) = 12 then coalesce(sum(Dez), 0) end as mesatual, " +
                        "     case when EXTRACT(month from current_date) - 1 = 0 then coalesce(sum(Dez), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 11 then coalesce(sum(Nov), 0) end as mesanterior " +
                        "          from(  " +
                        " select case when EXTRACT(month from c.datacompetencia) = 1 then sum(c.credito - c.debito) end as Jan, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 2 then sum(c.credito - c.debito) end as Fev, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 3 then sum(c.credito - c.debito) end as Mar, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 4 then sum(c.credito - c.debito) end as Abr, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 5 then sum(c.credito - c.debito) end as Mai, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 6 then sum(c.credito - c.debito) end as Jun, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 7 then sum(c.credito - c.debito) end as Jul, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 8 then sum(c.credito - c.debito) end as Ago, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 9 then sum(c.credito - c.debito) end as Set, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 10 then sum(c.credito - c.debito) end as Out, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 11 then sum(c.credito - c.debito) end as Nov, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 12 then sum(c.credito - c.debito) end as Dez " +
                        " from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id)" +
                       "  where pc.descricao in ('Receitas operacionais','Deduções','Custos','Despesas operacionais') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                       " group by c.datacompetencia) as receita) as rec", parametros).SingleOrDefault();
        }

        public decimal ObterRLMediaAno(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>(" select case when sum(c.credito - c.debito) is null then 0 else cast(sum(c.credito - c.debito) /12 as numeric(15,2)) end as mediaano " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                             "where pc.descricao in ('Receitas operacionais','Deduções','Custos','Despesas operacionais','Atividades de financiamento','Atividades de investimento') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterRLAcumulado(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select  coalesce(sum(c.credito - c.debito),0) as acumulado " +
                                             "from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)  " +
                                             "where pc.descricao in ('Receitas operacionais','Deduções','Custos','Despesas operacionais','Atividades de financiamento','Atividades de investimento') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal", parametros).SingleOrDefault();
        }

        public decimal ObterRLVariacao(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select mesatual - mesanterior as variacao from(  " +
                        " select  case when EXTRACT(month from current_date) = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) = 11 then coalesce(sum(Nov), 0) " +
                        "          when EXTRACT(month from current_date) = 12 then coalesce(sum(Dez), 0) end as mesatual, " +
                        "     case when EXTRACT(month from current_date) - 1 = 0 then coalesce(sum(Dez), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 1 then coalesce(sum(Jan), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 2 then coalesce(sum(Fev), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 3 then coalesce(sum(Mar), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 4 then coalesce(sum(Abr), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 5 then coalesce(sum(Mai), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 6 then coalesce(sum(Jun), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 7 then coalesce(sum(Jul), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 8 then coalesce(sum(Ago), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 9 then coalesce(sum(Set), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 10 then coalesce(sum(Out), 0) " +
                        "          when EXTRACT(month from current_date) - 1 = 11 then coalesce(sum(Nov), 0) end as mesanterior " +
                        "          from(  " +
                        " select case when EXTRACT(month from c.datacompetencia) = 1 then sum(c.credito - c.debito) end as Jan, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 2 then sum(c.credito - c.debito) end as Fev, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 3 then sum(c.credito - c.debito) end as Mar, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 4 then sum(c.credito - c.debito) end as Abr, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 5 then sum(c.credito - c.debito) end as Mai, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 6 then sum(c.credito - c.debito) end as Jun, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 7 then sum(c.credito - c.debito) end as Jul, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 8 then sum(c.credito - c.debito) end as Ago, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 9 then sum(c.credito - c.debito) end as Set, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 10 then sum(c.credito - c.debito) end as Out, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 11 then sum(c.credito - c.debito) end as Nov, " +
                        "        case when EXTRACT(month from c.datacompetencia) = 12 then sum(c.credito - c.debito) end as Dez " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                       "  where pc.descricao in ('Receitas operacionais','Deduções','Custos','Despesas operacionais','Atividades de financiamento','Atividades de investimento') and cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                       " group by c.datacompetencia) as receita) as rec", parametros).SingleOrDefault();
        }


        public IEnumerable<PontoEquilibrioFinanceiro> ObterPontoEquilibrioFinanceiro(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select  mes, " +
                                            " 	sum(receitaoperacional) as receitaoperacional,  " +
                                            " 	sum(despesasoperacionais) as despesasoperacionais, " +
                                            " 	sum(deducoes) as Deducoes,  " +
                                            " 	sum(custos) as custos,  " +
                                            " 	sum(AtivFinancimentoDebito) as AtivFinancimentoDebito,  " +
                                            " 	sum(AtivFinancimentoCredito) as AtivFinancimentoCredito,  " +
                                            " 	sum(AtivInvestimentoDebito) as AtivInvestimentoDebito,  " +
                                            " 	sum(AtivInvestimentoCredito) as AtivInvestimentoCredito  " +
                                            " from( " +
                                            " 	select  cast( '01'  || '/' ||  EXTRACT(month from c.datacompetencia) ||'/'|| EXTRACT(year from c.datacompetencia) as date) as mes,  " +
                                            " 	case when pc.descricao in ('Receitas operacionais') then sum(coalesce(credito, 0)) else 0 end as receitaoperacional,  " +
                                            "         case when pc.descricao in ('Despesas operacionais') then sum(coalesce(debito, 0)) else 0 end as despesasoperacionais, " +
                                            "         case when pc.descricao in ('Deduções') then sum(coalesce(debito, 0)) else 0 end as Deducoes, " +
                                            "         case when pc.descricao in ('Custos') then sum(coalesce(debito, 0)) else 0 end as custos, " +
                                            "         case when pc.descricao in ('Atividades de financiamento') then sum(coalesce(debito, 0)) else 0 end as AtivFinancimentoDebito, " +
                                            "         case when pc.descricao in ('Atividades de financiamento') then sum(coalesce(credito, 0)) else 0 end as AtivFinancimentoCredito, " +
                                            "         case when pc.descricao in ('Atividades de investimento') then sum(coalesce(debito, 0)) else 0 end as AtivInvestimentoDebito, " +
                                            "         case when pc.descricao in ('Atividades de investimento') then sum(coalesce(credito, 0)) else 0 end as AtivInvestimentoCredito " +
                                            "         from caixa c  inner join planoconta pc on (c.idplanoconta = pc.id) " +
                                            "         where cast(c.datacompetencia as date) between @dataInicial and @dataFinal " +
                                            "         group by c.datacompetencia, pc.descricao) as cx  " +
                                            " group by 1 " +
                                            " Order by 1 desc ", parametros).ToList();
            return (from registro in resultado
                    select new PontoEquilibrioFinanceiro(registro.mes, registro.receitaoperacional, registro.despesasoperacionais, registro.deducoes, registro.custos, registro.ativfinancimentodebito, registro.ativfinancimentocredito, registro.ativinvestimentodebito, registro.ativinvestimentocredito)).ToList();
        }
        public IEnumerable<PrazoMedioRecebimento> ObterPrazoMedioRecebimento(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select mes, sum(valorponderado/valorreceber) as pmr " +
                                            " from( select r.vencimento - r.emissao as prazodias,  " +
                                            "              r.valor as valorreceber,  " +
                                            "              r.valor,  " +
                                            "              r.valor * (cast(r.vencimento as date) - cast(r.emissao as date)) as valorponderado,  " +
                                            "              cast( '01' || '/' || EXTRACT(month from r.emissao) ||'/'|| EXTRACT(year from r.emissao) as date) as mes " +
                                            " from receber r  " +
                                            " where cast(r.emissao as date) between @dataInicial and @dataFinal  " +
                                            " ) as pmr  " +
                                            " group by 1 " +
                                            " order by 1 desc ", parametros).ToList();
            return (from registro in resultado
                    select new PrazoMedioRecebimento(registro.mes, registro.pmr)).ToList();
        }

        public int ObterPrazoMedioRecebimentosMesAtual()
        {
            return DbContext.Query<int>("select coalesce(sum(valorponderado/valorreceber),0) as pmr from(  " +
                                          " select  " +
                                          "     coalesce(r.valor,0) as valorreceber, " +
                                          "     coalesce(r.valor,0) * (cast(r.vencimento as date) - cast(r.emissao as date)) as valorponderado " +
                                         " from receber r where EXTRACT(month from r.emissao) = EXTRACT(month from current_date) and EXTRACT(year from r.emissao)  = EXTRACT(year from current_date)) as pmr ").SingleOrDefault();
        }

        public IEnumerable<PrazoMedioPagamento> ObterPrazoMedioPagamento(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select mes, sum(valorponderado/ valorreceber) as pmp  " +
                                            " from( select r.vencimento - r.emissao as prazodias,  " +
                                            "              coalesce(r.valor,0) as valorreceber,  " +
                                            "              coalesce(r.valor,0) as valor,  " +
                                            "              r.valor * (cast(r.vencimento as date) - cast(r.emissao as date)) as valorponderado,  " +
                                            "              cast( '01' || '/' || EXTRACT(month from r.emissao) ||'/'|| EXTRACT(year from r.emissao) as date) as mes " +
                                            " from pagar r  " +
                                            " where cast(r.emissao as date) between @dataInicial and @dataFinal  " +
                                            " ) as pmr  " +
                                            " group by 1 " +
                                            " order by 1 desc ", parametros).ToList();
            return (from registro in resultado
                    select new PrazoMedioPagamento(registro.mes, registro.pmp)).ToList();
        }

        public int ObterPrazoMedioPagamentosMesAtual()
        {
            return DbContext.Query<int>("select  coalesce(sum(valorponderado/coalesce(valorreceber,0)),0) as pmp from(  " +
                                          " select r.vencimento - r.emissao as prazodias, " +
                                          "     coalesce(r.valor,0) as valorreceber, " +
                                          "     coalesce(r.valor,0) as valor, " +
                                          "     r.valor * (cast(r.vencimento as date) - cast(r.emissao as date)) as valorponderado " +
                                         " from pagar r where EXTRACT(month from r.emissao) = EXTRACT(month from current_date) and EXTRACT(year from r.emissao)  = EXTRACT(year from current_date)) as pmr ").SingleOrDefault();
        }

        public IEnumerable<CicloFinanceiro> ObterCicloFinanceiro(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var PrazoMedioPagamento = ObterPrazoMedioPagamento(dataInicial, dataFinal);
            var PrazoMedioRecebimento = ObterPrazoMedioRecebimento(dataInicial, dataFinal);
            var PrazoMedioEstocagem = ObterPrazoMedioEstocagem(dataInicial, dataFinal);

            return (from registro in PrazoMedioEstocagem
                    let Prazomediorecebimento = PrazoMedioRecebimento.FirstOrDefault(x => x.Mes == registro.Mes)
                    let Prazomediopagamento = PrazoMedioPagamento.FirstOrDefault(x => x.Mes == registro.Mes)

                    select new CicloFinanceiro(registro.Mes, Prazomediopagamento?.Pmp ?? decimal.Zero, Prazomediorecebimento?.Pmr ?? decimal.Zero, registro.Dias)).ToList();
        }


        public IEnumerable<CapitalGiroLiquido> ObterCapitalGiroLiquido(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" SELECT  mes, " +
                                            " 	coalesce(SUM(RECEBER),0) - coalesce(SUM(PAGAR),0) AS previsao, " +
                                            " 	coalesce(sum(realizado),0) as realizado  " +
                                            " FROM ( select  cast( '01' || '/' || EXTRACT(month from r.vencimento) ||'/'|| EXTRACT(year from r.vencimento) as date) as mes, " +
                                            "                SUM(R.VALOR) AS RECEBER,  " +
                                            "                SUM(0) AS PAGAR,  " +
                                            "                sum(0) as realizado  " +
                                            "        FROM RECEBER R  " +
                                            "        WHERE R.datarecebimento is null and r.vencimento between CURRENT_DATE and @dataFinal " +
                                            "        GROUP BY EXTRACT(month from r.vencimento),EXTRACT(year from r.vencimento) " +
                                            " UNION ALL " +
                                            "        select  cast( '01' || '/' || EXTRACT(month from r.vencimento) ||'/'|| EXTRACT(year from r.vencimento) as date) as mes, " +
                                            "                SUM(0) AS RECEBER,  " +
                                            "                SUM(R.VALOR) AS PAGAR,  " +
                                            "                sum(0) as realizado  " +
                                            "        FROM PAGAR R  " +
                                            "        WHERE R.DATAPAGAMENTO IS NULL and r.vencimento between CURRENT_DATE and @dataFinal " +
                                            "        GROUP BY EXTRACT(month from r.vencimento),EXTRACT(year from r.vencimento) " +
                                            " union ALL  " +
                                            "        select  cast( '01' || '/' || EXTRACT(month from r.datacompetencia) ||'/'|| EXTRACT(year from r.datacompetencia) as date) as mes, " +
                                            "                SUM(0) AS RECEBER, " +
                                            "                SUM(0) AS PAGAR, " +
                                            "                coalesce(sum(r.debito),0) - coalesce(sum(r.credito),0) as realizado  " +
                                            "        FROM caixa R  " +
                                            "        where r.datacompetencia between @dataInicial and @dataFinal " +
                                            "        GROUP BY EXTRACT(month from r.datacompetencia),EXTRACT(year from r.datacompetencia)) AS PAG  " +
                                            " GROUP BY 1 " +
                                            " ORDER BY 1 DESC ", parametros).ToList();
            return (from registro in resultado
                    select new CapitalGiroLiquido(registro.mes, registro.previsao, registro.realizado)).ToList();
        }

        public IEnumerable<ProjecaoFluxoCaixa> ObterProjecaoFluxoCaixa(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();

            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" with ctedados as (   " +
                              "                     select dia,    " +
                             " 	diaprev,  " +
                                  "                 	sum(coalesce(pagar,0)) as pagar,    " +
                                    "               	sum(coalesce(receber,0)) as receber,  " +
                                    "               	sum(coalesce(receber,0)) - sum(coalesce(pagar,0)) as diferenca,  " +
                                    "               	sum(coalesce(saldodia,0)) as saldodia  " +
                                    "               from(    " +
                    " 		SELECT  cast(generate_series as date) as dia, " +
                "  			cast(generate_series as date) as diaprev, " +
                " 			 sum(0) as pagar,   " +
                                "                           sum(0) as receber,  " +
                                "                           sum(0) as saldodia  " +
                " 	        FROM generate_series(DATE (@dataInicial) ,DATE (@dataFinal),INTERVAL'1 day')	 " +
                " 	        group by dia, diaprev  " +
                " 	        union all  " +
                                "                    select p.vencimento as dia,   " +
                             " 	 p.vencimento  as diaprev,  " +
                                "                           sum(p.valor) as pagar,   " +
                                "                           sum(0) as receber,  " +
                                "                            sum(0) as saldodia  " +
                                "                    from pagar p    " +
                                "                    where p.datapagamento is null and p.vencimento between current_date and @dataFinal  " +
                                "                    group by  dia, diaprev  " +
                                "                   union all    " +
                                "                    select p.vencimento as dia,   " +
                             " 	 p.vencimento as diaprev,   " +
                                "                           sum(0) as pagar,     " +
                               "                            sum(p.valor) as receber,  " +
                                "                           sum(0) as saldodia    " +
                                "                    from Receber p     " +
                                "                    where p.datarecebimento is null and p.vencimento between current_date and @dataFinal " +
                                "                    group by   dia, diaprev  " +
                                "                    union all  " +
                                "                    select c.datacompetencia  as dia,   " +
                              " 	 c.datacompetencia  as diaprev,   " +
                                "                           sum(0) as pagar,   " +
                                "                           sum(0) as receber,  " +
                                "                           coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) as saldodia    " +
                                "                    from caixa c    " +
                                "                    where c.datacompetencia <= current_date   " +
                                "                    group by  dia, diaprev  " +
                                "                   ) as pagarreceber    " +
                                "                   group by dia, diaprev " +
                                "                   order by dia, diaprev  " +
                                "                 ), ctedados2 as (select row_number() over(order by dia, diaprev) as seq, dia, diaprev, pagar, receber, diferenca, saldodia from ctedados)  " +
                                "                 select seq, case when dia > current_date then current_date else dia end as dia, case when diaprev <= current_date then dia else diaprev end as diaprev  , pagar, receber, diferenca, saldodia, " +
                                "                 (select  coalesce(a.saldodia, 0) + coalesce(sum(b.saldodia), 0)  from ctedados2 b where SEQ <= a.SEQ - 1 ) as diferencaacumuladadia," +
                                "                 (select  coalesce(a.saldodia,0) + coalesce(sum(b.saldodia),0) + coalesce(a.diferenca,0) + coalesce(sum(b.diferenca),0) from ctedados2 b where SEQ <= a.SEQ-1 ) as diferencaacumuladaprev  " +
                                "                from ctedados2 a where dia between @dataInicial  and @dataFinal order by 1   ", parametros).ToList();
            return (from registro in resultado
                    select new ProjecaoFluxoCaixa(registro.seq, registro.dia, registro.diaprev, registro.pagar, registro.receber, registro.diferenca, registro.saldodia, registro.diferencaacumuladadia, registro.diferencaacumuladaprev)).ToList();
        }

        public IEnumerable<PrazoMedioEstocagem> ObterPrazoMedioEstocagem(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select cast( '01' || '/' || EXTRACT(month from P.data) ||'/'|| EXTRACT(year from p.data) as date) as mes, " +
                                            "        max(p.dias) as dias " +
                                            " from pme p  " +
                                            " where cast(p.data as date) between @dataInicial AND @dataFinal  " +
                                            " group by  EXTRACT(month from P.data), EXTRACT(year from P.data) " +
                                            " order by EXTRACT(YEAR from P.data) DESC, EXTRACT(month from P.data) desc ", parametros).ToList();
            return (from registro in resultado
                    select new PrazoMedioEstocagem(registro.mes, registro.dias)).ToList();
        }


        public int ObterPrazoMedioEstocagemMesAtual()
        {

            return DbContext.Query<int>("select coalesce(max(p.dias),0) as dias from pme p  " +
                "where EXTRACT(month from p.data) = EXTRACT(month from current_date) and EXTRACT(year from p.data) = EXTRACT(year from current_date)").SingleOrDefault();
        }

        public IEnumerable<DistribuicaoResultado> ObterDistribuicaoResultados(DateTime dataInicial, DateTime dataFinal, DateTime dataInicial_ant, DateTime dataFinal_ant)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);
            parametros.Add("@dataInicial_ant", dataInicial_ant, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal_ant", dataFinal_ant, System.Data.DbType.DateTime);

            var resultado = DbContext.Query("select categoria,  " +
                     "   sum(coalesce(Mesatual, 0)) as mesatual, " +
                     "    sum(coalesce(Mesanterior, 0)) as mesanterior, " +
                     "    sum(coalesce(Mesatual, 0)) - sum(coalesce(Mesanterior, 0)) as diferenca " +
                     "    from( " +
                     "  " +
                     "    SELECT '1-Receitas operacionais' as categoria, " +
                     "    case when datacompetencia between @dataInicial and @dataFinal   then sum(coalesce(credito, 0)) else 0 end as Mesatual, " +
                     "    case when datacompetencia between @dataInicial_ant and @dataFinal_ant   then sum(coalesce(credito, 0)) else 0 end as Mesanterior " +
                     "    from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)" +
                     "    where pc.descricao = 'Receitas operacionais' " +
                     "    group by 1, datacompetencia " +
                     "  " +
                     "    union all " +
                     "  " +
                     "    SELECT '2-Deduções' as categoria, " +
                     "    case when datacompetencia between @dataInicial and @dataFinal   then sum(coalesce(debito, 0)) else 0 end as Mesatual, " +
                     "    case when datacompetencia between @dataInicial_ant and @dataFinal_ant   then sum(coalesce(debito, 0)) else 0 end as Mesanterior " +
                     "    from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)" +
                     "    where pc.descricao = 'Deduções' " +
                     "    group by 1, datacompetencia " +
                     "  " +
                     "    union all " +
                     "  " +
                     "    SELECT '3-Custos' as categoria, " +
                     "    case when datacompetencia between @dataInicial and @dataFinal   then sum(coalesce(debito, 0)) else 0 end as Mesatual, " +
                     "    case when datacompetencia between @dataInicial_ant and @dataFinal_ant   then sum(coalesce(debito, 0)) else 0 end as Mesanterior " +
                     "    from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)" +
                     "    where pc.descricao = 'Custos' " +
                     "    group by 1, datacompetencia " +
                     "  " +
                     "    union all" +
                     "  " +
                     "    SELECT '4-Despesas operacionais' as categoria, " +
                     "    case when datacompetencia between @dataInicial and @dataFinal   then sum(coalesce(debito, 0)) else 0 end as Mesatual, " +
                     "    case when datacompetencia between @dataInicial_ant and @dataFinal_ant   then sum(coalesce(debito, 0)) else 0 end as Mesanterior " +
                     "    from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                     "    where pc.descricao = 'Despesas operacionais' " +
                     "    group by 1, datacompetencia " +
                     "  " +
                     "    union all " +
                     "  " +
                     "    SELECT '5-(=)Resultado Operacional' as categoria, " +
                     "    case when datacompetencia between @dataInicial and @dataFinal   then sum(coalesce(credito, 0) - coalesce(debito, 0)) else 0 end as Mesatual, " +
                     "    case when datacompetencia between @dataInicial_ant and @dataFinal_ant   then sum(coalesce(credito, 0) - coalesce(debito, 0)) else 0 end as Mesanterior " +
                     "    from caixa c inner join planoconta pc on (c.idplanoconta = pc.id)" +
                     "    where pc.descricao not in ('Atividades de financiamento', 'Atividades de investimento') " +
                     "    group by 1, datacompetencia " +
                     "  " +
                     "    union all" +
                     "  " +
                     "    SELECT '6-Atividades de financiamento' as categoria, " +
                     "    case when datacompetencia between @dataInicial and @dataFinal   then sum(coalesce(credito, 0) - coalesce(debito, 0)) else 0 end as Mesatual, " +
                     "    case when datacompetencia between @dataInicial_ant and @dataFinal_ant   then sum(coalesce(credito, 0) - coalesce(debito, 0)) else 0 end as Mesanterior " +
                     "    from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                     "    where pc.descricao = 'Atividades de financiamento' " +
                     "    group by 1, datacompetencia " +
                     "  " +
                     "    union all" +
                     "  " +
                     "    SELECT '7-Atividades de investimento' as categoria, " +
                     "    case when datacompetencia between @dataInicial and @dataFinal   then sum(coalesce(credito, 0) - coalesce(debito, 0)) else 0 end as Mesatual, " +
                     "    case when datacompetencia between @dataInicial_ant and @dataFinal_ant   then sum(coalesce(credito, 0) - coalesce(debito, 0)) else 0 end as Mesanterior " +
                     "    from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                     "    where pc.descricao = 'Atividades de investimento' " +
                     "    group by 1, datacompetencia " +
                     "  " +
                     "    union all" +
                     "  " +
                     "    SELECT '8-(=)Resultado Líquido' as categoria, " +
                     "    case when datacompetencia between @dataInicial and @dataFinal   then sum(coalesce(credito, 0) - coalesce(debito, 0)) else 0 end as Mesatual, " +
                     "    case when datacompetencia between @dataInicial_ant and @dataFinal_ant   then sum(coalesce(credito, 0) - coalesce(debito, 0)) else 0 end as Mesanterior " +
                     "    from caixa  c" +
                     "    group by 1, datacompetencia " +
                     "     ) as resultado " +
                     "    group by categoria " +
                     "    order by categoria", parametros).ToList();
            return (from registro in resultado
                    select new DistribuicaoResultado(registro.categoria, registro.mesatual, registro.mesanterior, registro.diferenca)).ToList();
        }

        public IEnumerable<ResultadoParaTrimestreRP> ObterResultadoParaTrimestreRP(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select  mes, " +
                                            " 	coalesce(sum(recebido),0) as recebido, " +
                                            " 	coalesce(sum(Avencer),0) as avencer,  " +
                                            " 	coalesce(sum(vencido),0) as vencidos, " +
                                            " 	coalesce(sum(pagopag), 0) as pagopag, " +
                                            "         coalesce(sum(Avencerpag), 0) as avencerpag,  " +
                                            "         coalesce(sum(vencidopag), 0) as vencidospag  " +
                                            " from(   select  sum(p.valorpago) as pagopag,  " +
                                            "                 case when p.datapagamento is null and vencimento >= current_date then sum(p.valor) end as Avencerpag,  " +
                                            "                 case when p.datapagamento is null and vencimento < current_date then sum(p.valor) end as Vencidopag,  " +
                                            "                 sum(0) as recebido,  " +
                                            "                 sum(0) as Avencer,  " +
                                            "                 sum(0) as vencido,  " +
                                            " 		cast( '01' || '/' || EXTRACT(month from p.vencimento) ||'/'|| EXTRACT(year from p.vencimento) as date) as mes  " +
                                            "         from pagar p  " +
                                            "         where cast(p.vencimento as date) between @dataInicial and @dataFinal " +
                                            "         group by p.datapagamento, p.vencimento  " +
                                            " union all  " +
                                            " 	select  sum(0) as pago, " +
                                            " 		sum(0) as Avencer,  " +
                                            " 		sum(0) as vencido,  " +
                                            " 		sum(r.valorrecebido) as recebido,  " +
                                            " 		case when r.datarecebimento is null and vencimento >= current_date then sum(r.valor) end as Avencer,  " +
                                            "                 case when r.datarecebimento is null and vencimento < current_date then sum(r.valor) end as Vencido,  " +
                                            "                 cast( '01'  || '/' || EXTRACT(month from r.vencimento) ||'/'|| EXTRACT(year from r.vencimento) as date) as mes  " +
                                            "         from receber r  " +
                                            "         where cast(r.vencimento as date) between @dataInicial and @dataFinal " +
                                            "         group by r.datarecebimento, r.vencimento) as caixa  " +
                                            " group by 1 " +
                                            " order by 1 desc ", parametros).ToList();
            return (from registro in resultado

                    select new ResultadoParaTrimestreRP(registro.mes, registro.recebido, registro.avencer, registro.vencidos, registro.pagopag, registro.avencerpag, registro.vencidospag)).ToList();
        }

        public IEnumerable<ProjecaoMensal> ObterProjecaoMensalDeContasPagarReceberNoPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" with ctedados as ( " +
                                            "  select mes,  " +
                                            " 	sum(coalesce(pagar,0)) as pagar,  " +
                                            " 	sum(coalesce(receber,0)) as receber,  " +
                                            " 	sum(coalesce(receber,0)) - sum(coalesce(pagar,0)) as diferenca " +
                                            " from(  " +
                                            "  select cast( '01' || '/' || EXTRACT(month from p.vencimento) ||'/'|| EXTRACT(year from p.vencimento) as date) as mes, " +
                                            "         sum(p.valor) as pagar, " +
                                            "         sum(0) as receber  " +
                                            "  from pagar p  " +
                                            "  where p.datapagamento is null and p.vencimento between @dataInicial and @dataFinal " +
                                            "  group by EXTRACT(month from p.vencimento),EXTRACT(year from p.vencimento) " +
                                            " union all  " +
                                            "  select cast( '01' || '/' || EXTRACT(month from p.vencimento) ||'/'|| EXTRACT(year from p.vencimento) as date) as mes,  " +
                                            "         sum(0) as pagar,   " +
                                            "         sum(p.valor) as receber  " +
                                            "  from Receber p   " +
                                            "  where p.datarecebimento is null and p.vencimento between @dataInicial and @dataFinal  " +
                                            "  group by EXTRACT(month from p.vencimento),EXTRACT(year from p.vencimento)  " +
                                            " ) as pagarreceber  " +
                                            " group by mes " +
                                            " order by mes " +
                                            " ), ctedados2 as (select row_number() over(order by mes) as SEQ, MES, pagar, receber, diferenca from ctedados) " +
                                            " select *, " +
                                            "     (select coalesce(a.diferenca,0) + coalesce(sum(b.diferenca),0) from ctedados2 b where SEQ <= a.SEQ-1) as diferencaacumulada " +
                                            " from ctedados2 a ", parametros).ToList();
            return (from registro in resultado
                    select new ProjecaoMensal(registro.pagar, registro.receber, registro.diferenca, registro.mes, registro.diferencaacumulada)).ToList();
        }

        public IEnumerable<ProjecaoMensalGrid> ObterProjecaoMensalDeContasPagarReceberGrid(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query("select tipo,  " +
                            " sum(coalesce(Jan, 0)) as jan, " +
                            " sum(coalesce(Fev, 0)) as fev, " +
                            " sum(coalesce(Mar, 0)) as mar, " +
                            " sum(coalesce(Abr, 0)) as abr, " +
                            " sum(coalesce(Mai, 0)) as mai, " +
                            " sum(coalesce(Jun, 0)) as jun, " +
                            " sum(coalesce(Jul, 0)) as jul, " +
                            " sum(coalesce(Ago, 0)) as ago, " +
                            " sum(coalesce(Set, 0)) as set, " +
                            " sum(coalesce(Oct, 0)) as oct, " +
                            " sum(coalesce(Nov, 0)) as nov, " +
                            " sum(coalesce(Dez, 0)) as dez " +
                        " from( " +
                        " select " +
                        " 'Pagar' as tipo, " +
                        "  case when EXTRACT(month from p.vencimento) = 1 then sum(p.valor) end as Jan, " +
                        "  case when EXTRACT(month from p.vencimento) = 2 then sum(p.valor)  end as Fev, " +
                        "  case when EXTRACT(month from p.vencimento) = 3 then sum(p.valor)  end as Mar, " +
                        "  case when EXTRACT(month from p.vencimento) = 4 then sum(p.valor)  end as Abr, " +
                        "  case when EXTRACT(month from p.vencimento) = 5 then sum(p.valor)  end as Mai, " +
                        "  case when EXTRACT(month from p.vencimento) = 6 then sum(p.valor)  end as Jun, " +
                        "  case when EXTRACT(month from p.vencimento) = 7 then sum(p.valor)  end as Jul, " +
                        "  case when EXTRACT(month from p.vencimento) = 8 then sum(p.valor)  end as Ago, " +
                        "  case when EXTRACT(month from p.vencimento) = 9 then sum(p.valor)  end as Set, " +
                        "  case when EXTRACT(month from p.vencimento) = 10 then sum(p.valor)  end as Oct, " +
                        "  case when EXTRACT(month from p.vencimento) = 11 then sum(p.valor)  end as Nov, " +
                        "  case when EXTRACT(month from p.vencimento) = 12 then sum(p.valor) end as Dez " +
                       "  from pagar p " +
                       "  where p.situacao = 'EmAberto' and p.vencimento between @dataInicial and @dataFinal " +
                      "   group by EXTRACT(month from p.vencimento) " +
                       "  union all " +
                       "  select " +
                       "  'Receber' as tipo, " +
                       "   case when EXTRACT(month from p.vencimento) = 1 then sum(p.valor) end as Jan, " +
                       "   case when EXTRACT(month from p.vencimento) = 2 then sum(p.valor)  end as Fev, " +
                       "   case when EXTRACT(month from p.vencimento) = 3 then sum(p.valor)  end as Mar, " +
                       "   case when EXTRACT(month from p.vencimento) = 4 then sum(p.valor)  end as Abr, " +
                       "   case when EXTRACT(month from p.vencimento) = 5 then sum(p.valor)  end as Mai, " +
                       "   case when EXTRACT(month from p.vencimento) = 6 then sum(p.valor)  end as Jun, " +
                       "   case when EXTRACT(month from p.vencimento) = 7 then sum(p.valor)  end as Jul, " +
                       "   case when EXTRACT(month from p.vencimento) = 8 then sum(p.valor)  end as Ago, " +
                       "   case when EXTRACT(month from p.vencimento) = 9 then sum(p.valor)  end as Set, " +
                       "   case when EXTRACT(month from p.vencimento) = 10 then sum(p.valor)  end as Oct, " +
                       "   case when EXTRACT(month from p.vencimento) = 11 then sum(p.valor)  end as Nov, " +
                       "   case when EXTRACT(month from p.vencimento) = 12 then sum(p.valor) end as Dez " +
                       "  from Receber p " +
                       "  where p.situacao = 'EmAberto' and p.vencimento between @dataInicial and @dataFinal " +
                       "  group by EXTRACT(month from p.vencimento) " +
                       "  ) as gridpr " +
                       "  group by tipo", parametros).ToList();
            return (from registro in resultado
                    select new ProjecaoMensalGrid(registro.tipo, registro.jan, registro.fev, registro.mar, registro.abr, registro.mai, registro.jun, registro.jul, registro.ago, registro.set, registro.oct, registro.nov, registro.dez)).ToList();
        }

        public IEnumerable<RelFluxoDeCaixa> ObterRelFluxoDeCaixa(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                                " with ctedados as (   select co.nome as conta, " +
                                " 		      c.datacompetencia as competencia, " +
                                " 		      'nome da pessoa' as nomepessoa, " +
                                "                       pc.categoria, " +
                                "                       c.descricao, " +
                                "                       c.credito as recebimentos, " +
                                "                       c.debito as pagamentos, " +
                                "                       coalesce(c.credito,0) - coalesce(c.debito,0) as saldodia " +
                                " from caixa c inner join conta co on (c.idconta = co.id)  left join planoconta pc on (pc.id = c.idplanoconta)),  " +
                                " ctedados2 as (select row_number() over(order by competencia) as seq,  " +
                                " 		   conta,			 " +
                                " 		   competencia, " +
                                " 		   nomepessoa, " +
                                " 		   categoria, " +
                                " 		   descricao, " +
                                " 		   recebimentos, " +
                                " 		   pagamentos,  " +
                                " 		   saldodia " +
                                " from ctedados)   " +
                                " select 	          cast( seq as int) as seq, " +
                                " 		   conta,	 " +
                                " 		   competencia, " +
                                " 		   nomepessoa, " +
                                " 		   categoria, " +
                                " 		   descricao, " +
                                " 		   recebimentos, " +
                                " 		   pagamentos,  " +
                                " 		   saldodia, " +
                                " (select  coalesce(a.saldodia, 0) + coalesce(sum(b.saldodia), 0)  from ctedados2 b where SEQ <= a.SEQ - 1 and conta = a.conta ) as diferencaacumuladadia " +
                                " from ctedados2 a  " +
                                " where competencia between @dataInicial and @dataFinal   " +
                                " order by 1    "
                , parametros).ToList();
            return (from registro in resultado
                    select new RelFluxoDeCaixa(registro.seq, registro.conta, registro.competencia, registro.nomepessoa, registro.categoria, registro.descricao, registro.recebimentos, registro.pagamentos, registro.saldodia, registro.diferencaacumuladadia)).ToList();
        }

        public IEnumerable<RelDRE> ObterRelDRE(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(

                    "                 select 1 as seq,  '1-Receitas Operacionais' as descricao, " +
                    " pc.categoria, " +
                    " sum(coalesce(c.credito, 0)) as valor, " +
                    " cast(sum(coalesce(c.credito, 0)) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                        join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal " +
                    " group by 1,2 ,3" +
                    "  " +
                    " union all " +
                    "  " +
                    " select 2 as seq," +
                    " '2-Deduções' as descricao, " +
                    " pc.categoria, " +
                    " sum(coalesce(c.debito, 0)) as valor, " +
                    " cast(sum(coalesce(c.debito, 0)) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Deduções' and c.datacompetencia between @dataInicial and @dataFinal " +
                    " group by 1,2,3 " +
                    "  " +
                    " union all " +
                    "  " +
                    " select 3 as seq," +
                    " '3-Receitas Líquida' as descricao, " +
                    " '' as categoria, " +
                    " sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0)) as valor, " +
                    " cast((sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0))) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao in ('Receitas operacionais', 'Deduções') and c.datacompetencia between @dataInicial and @dataFinal " +
                    "  group by 1,2,3 " +
                    "  " +
                    " union all " +
                    "  " +
                    " select 4 as seq, " +
                    " '4-Custos' as descricao, " +
                    " pc.categoria, " +
                    " sum(coalesce(c.debito, 0)) as valor, " +
                    " cast(sum(coalesce(c.debito, 0)) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Custos' and c.datacompetencia between @dataInicial and @dataFinal " +
                    " group by 1,2 ,3" +
                    "  " +
                    " union all " +
                    "  " +
                    " select 5 as seq, " +
                    " '5-Margem de Contribuição Financeira' as descricao, " +
                    " '' as categoria, " +
                    " sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0)) as valor, " +
                    " cast((sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0))) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao in ('Receitas operacionais', 'Deduções', 'Custos') and c.datacompetencia between @dataInicial and @dataFinal " +
                    "  group by 1,2 ,3" +
                    "  " +
                    " union all " +
                    "                  " +
                    " select 6 as seq, " +
                    " '6-Despesas operacionais' as descricao, " +
                    " pc.categoria, " +
                    " sum(coalesce(c.debito, 0)) as valor, " +
                    " cast(sum(coalesce(c.debito, 0)) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Despesas operacionais' and c.datacompetencia between @dataInicial and @dataFinal " +
                    " group by 1,2 ,3" +
                    "  " +
                    " union all " +
                    "   " +
                    " select 7 as seq," +
                    " '7-Resultado Financeiro Operacional' as descricao, " +
                    " '' as categoria, " +
                    " sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0)) as valor, " +
                    " cast((sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0))) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao in ('Receitas operacionais', 'Deduções', 'Custos', 'Despesas operacionais') and c.datacompetencia between @dataInicial and @dataFinal " +
                    "  group by 1,2,3 " +
                    "  " +
                    " union all " +
                    "  " +
                    " select 8 as seq, " +
                    " '8-Atividades de Financiamento' as descricao, " +
                    " pc.categoria, " +
                    " sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0)) as valor, " +
                    " cast((sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0))) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Atividades de financiamento' and c.datacompetencia between @dataInicial and @dataFinal " +
                    " group by 1,2 ,3" +
                    "  " +
                    " union all " +
                    "  " +
                    " select 9 as seq, " +
                    " '9-Atividades de Investimento' as descricao, " +
                    " pc.categoria, " +
                    " sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0)) as valor, " +
                    " cast((sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0))) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Atividades de investimento' and c.datacompetencia between @dataInicial and @dataFinal " +
                    " group by 1,2,3 " +
                    "  " +
                    " union all " +
                    "  " +
                    " select 10 as seq," +
                    " '9.1-Resultado Líquido Financeiro' as descricao, " +
                    " '' as categoria, " +
                    " sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0)) as valor, " +
                    " cast((sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0))) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais'  and c.datacompetencia between @dataInicial and @dataFinal) * 100 as numeric(15, 2)) as percentual " +
                    "  " +
                    "                                                                                                       from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao in ('Receitas operacionais', 'Deduções', 'Custos', 'Despesas operacionais', 'Atividades de financiamento', 'Atividades de investimento') " +
                    " and c.datacompetencia between @dataInicial and @dataFinal " +
                    " group by 1,2,3 " +
                    "  " +
                    " union all " +
                    "  " +
                    " select 11 as seq," +
                    " '9.2-Ponto de Equilíbrio Financeiro' as descricao, " +
                    " '' as categoria, " +
                    " cast( abs(sum(coalesce(c.Credito, 0)) - sum(coalesce(c.debito, 0))) /  " +
                    " (select " +
                    " (sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0))) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                                                      join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal)  as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao in ('Receitas operacionais', 'Deduções', 'Custos') and c.datacompetencia between @dataInicial and @dataFinal " +
                    " )as numeric(15, 2)) as valor, " +
                    "   " +
                    " cast( " +
                    " cast( abs(sum(coalesce(c.Credito, 0)) - sum(coalesce(c.debito, 0))) /  " +
                    " (select " +
                    " (sum(coalesce(c.credito, 0)) - sum(coalesce(c.debito, 0))) / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                                                                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal)  as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao in ('Receitas operacionais', 'Deduções', 'Custos') and c.datacompetencia between @dataInicial and @dataFinal " +
                    " ) as numeric(15, 2)) " +
                    " / (select sum(coalesce(c.credito, 0)) from caixa c inner " +
                    "                                       join planoconta pc on (c.idplanoconta = pc.id) " +
                    "  where pc.descricao = 'Receitas operacionais' and c.datacompetencia between @dataInicial and @dataFinal )*100 as numeric(15, 2)) as percentual " +
                    "  " +
                    " from caixa c inner " +
                    " join planoconta pc on (c.idplanoconta = pc.id) " +
                    " where pc.descricao in('Atividades de financiamento', 'Despesas operacionais') and c.datacompetencia between @dataInicial and @dataFinal " +
                    " group by 1,2,3 ", parametros).ToList();
            return (from registro in resultado
                    select new RelDRE(registro.seq, registro.descricao, registro.categoria, registro.valor, registro.percentual)).ToList();
        }

        public IEnumerable<RelDREDetalhado> ObterRelDREDetalhado(int ano)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ano", ano);


            var resultado = DbContext.Query(
                " select  ordem, " +
" 	grupo, " +
" 	categoria, " +
" 	sum(Jan) as jan, " +
" 	sum(Fev) as fev, " +
" 	sum(Mar) as mar, " +
" 	sum(Abr) as abr, " +
" 	sum(Mai) as mai, " +
" 	sum(Jun) as jun, " +
" 	sum(Jul) as jul, " +
" 	sum(Ago) as ago, " +
" 	sum(Set) as set, " +
" 	sum(Out) as oct, " +
" 	sum(Nov) as nov, " +
" 	sum(Dez) as dez, " +
" 	sum(Media) as Media, " +
" 	sum(Total) as Total " +
" from ( " +
" select '1-Receitas Operacionais' as grupo, " +
" 	1 as ordem, " +
" 	pc.categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then  coalesce(sum(c.credito),0)  else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then  coalesce(sum(c.credito),0)  else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then  coalesce(sum(c.credito),0)  else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then  coalesce(sum(c.credito),0)  else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then  coalesce(sum(c.credito),0)  else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then  coalesce(sum(c.credito),0)  else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then  coalesce(sum(c.credito),0)  else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then  coalesce(sum(c.credito),0)  else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then  coalesce(sum(c.credito),0)  else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) else 0 end as Dez, " +
" 	cast(((coalesce(sum(c.credito),0)) / 12) as numeric (15,2)) as Media, " +
" 	coalesce(sum(c.credito),0)  as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao = 'Receitas operacionais' and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '2-Deduções' as grupo, " +
" 	2 as ordem, " +
" 	pc.categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast((( coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
" 	 coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao = 'Deduções' and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '3-Receita Líquida' as grupo, " +
" 	3 as ordem, " +
" 	'' as categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast(((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
" 	coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao in ('Deduções','Receitas operacionais') and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '4-%-Receita Líquida' as grupo, /*Receita liquida-Percentual*/ " +
" 	4 as ordem, " +
" 	'' as categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 1 )*100 as numeric(15,2)) else 0 end  as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 2 )*100 as numeric(15,2)) else 0 end  as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 3 )*100 as numeric(15,2)) else 0 end  as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 4 )*100 as numeric(15,2)) else 0 end  as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 5 )*100 as numeric(15,2)) else 0 end  as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 6 )*100 as numeric(15,2)) else 0 end   as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 7 )*100 as numeric(15,2)) else 0 end   as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1   else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 8 )*100 as numeric(15,2)) else 0 end   as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 9 )*100 as numeric(15,2)) else 0 end   as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 10 )*100 as numeric(15,2)) else 0 end   as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 11 )*100 as numeric(15,2)) else 0 end   as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 12 )*100 as numeric(15,2)) else 0 end   as Dez, " +
" 	cast( (coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)/12) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end/12 from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais'  )*100 as numeric(15,2))  as Media, " +
" 	cast( coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais'  )*100 as numeric(15,2))  as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao in ('Deduções','Receitas operacionais') and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '5-Custos' as grupo, " +
" 	5 as ordem, " +
" 	pc.categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast((( coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
"  coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao = 'Custos' and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '6-Margem de Contribuição' as grupo, " +
" 	6 as ordem, " +
" 	'' as categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast(((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
" 	coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao in ('Deduções','Receitas operacionais','Custos') and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '7-%-Margem de Contribuição' as grupo, /*Margem de Contribuição*/ " +
" 	7 as ordem, " +
" 	'%' as categoria, " +
" case when EXTRACT(month from c.datacompetencia) = 1 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 1 )*100 as numeric(15,2)) else 0 end  as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 2 )*100 as numeric(15,2)) else 0 end  as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 3 )*100 as numeric(15,2)) else 0 end  as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 4 )*100 as numeric(15,2)) else 0 end  as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 5 )*100 as numeric(15,2)) else 0 end  as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 6 )*100 as numeric(15,2)) else 0 end   as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 7 )*100 as numeric(15,2)) else 0 end   as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 8 )*100 as numeric(15,2)) else 0 end   as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 9 )*100 as numeric(15,2)) else 0 end   as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 10 )*100 as numeric(15,2)) else 0 end   as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 11 )*100 as numeric(15,2)) else 0 end   as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 12 )*100 as numeric(15,2)) else 0 end   as Dez, " +
"	cast( (coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)/12) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end/12 from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais'  )*100 as numeric(15,2))  as Media, " +
" 	cast( coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais'  )*100 as numeric(15,2))  as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao in ('Deduções','Receitas operacionais','Custos') and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '8-Despesas operacionais' as grupo, " +
" 	8 as ordem, " +
" 	pc.categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast((( coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
" 	 coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao = 'Despesas operacionais' and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '9-Resultado Operacional' as grupo, " +
" 	9 as ordem, " +
" 	'' as categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast(((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
" 	coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais') and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '91-%-Resultado Operacional' as grupo, /*Resultado Operacional*/ " +
" 	10 as ordem, " +
" 	'%' as categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 1 )*100 as numeric(15,2)) else 0 end  as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 2 )*100 as numeric(15,2)) else 0 end  as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 3 )*100 as numeric(15,2)) else 0 end  as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 4 )*100 as numeric(15,2)) else 0 end  as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 5 )*100 as numeric(15,2)) else 0 end  as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 6 )*100 as numeric(15,2)) else 0 end   as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 7 )*100 as numeric(15,2)) else 0 end   as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 8 )*100 as numeric(15,2)) else 0 end   as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 9 )*100 as numeric(15,2)) else 0 end   as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 10 )*100 as numeric(15,2)) else 0 end   as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 11 )*100 as numeric(15,2)) else 0 end   as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 12 )*100 as numeric(15,2)) else 0 end   as Dez, " +
" 	cast( (coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)/12) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end/12 from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais'  )*100 as numeric(15,2))  as Media, " +
" 	cast( coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais'  )*100 as numeric(15,2))  as Total " +
" 	from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais') and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '91-Atividades de financiamento' as grupo, " +
" 	11 as ordem, " +
" 	pc.categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast(((coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
" 	coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao = 'Atividades de financiamento' and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '93-Atividades de investimento' as grupo, " +
" 	12 as ordem, " +
" 	pc.categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast(((coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
" 	coalesce(sum(c.credito),0) + coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao = 'Atividades de investimento' and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, c.datacompetencia  " +
" union all " +
" select '94-Resultado Líquido Financeiro' as grupo, " +
" 	13 as ordem, " +
" 	'' as categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Dez, " +
" 	cast(((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / 12) as numeric (15,2)) as Media, " +
" 	coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais','Atividades de financiamento','Atividades de investimento') and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by grupo, ordem, pc.categoria, EXTRACT(month from c.datacompetencia)  " +
" union all " +
" select '95-%-Resultado Líquido Financeiro' as grupo, /*Resultado Líquido Financeiro*/ " +
" 	14 as ordem, " +
" 	'%' as categoria, " +
" 	case when EXTRACT(month from c.datacompetencia) = 1 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 1 )*100 as numeric(15,2)) else 0 end  as Jan, " +
" 	case when EXTRACT(month from c.datacompetencia) = 2 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 2 )*100 as numeric(15,2)) else 0 end  as Fev, " +
" 	case when EXTRACT(month from c.datacompetencia) = 3 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 3 )*100 as numeric(15,2)) else 0 end  as Mar, " +
" 	case when EXTRACT(month from c.datacompetencia) = 4 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 4 )*100 as numeric(15,2)) else 0 end  as Abr, " +
" 	case when EXTRACT(month from c.datacompetencia) = 5 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 5 )*100 as numeric(15,2)) else 0 end  as Mai, " +
" 	case when EXTRACT(month from c.datacompetencia) = 6 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 6 )*100 as numeric(15,2)) else 0 end   as Jun, " +
" 	case when EXTRACT(month from c.datacompetencia) = 7 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 7 )*100 as numeric(15,2)) else 0 end   as Jul, " +
" 	case when EXTRACT(month from c.datacompetencia) = 8 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 8 )*100 as numeric(15,2)) else 0 end   as Ago, " +
" 	case when EXTRACT(month from c.datacompetencia) = 9 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 9 )*100 as numeric(15,2)) else 0 end   as Set, " +
" 	case when EXTRACT(month from c.datacompetencia) = 10 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 10 )*100 as numeric(15,2)) else 0 end   as Out, " +
" 	case when EXTRACT(month from c.datacompetencia) = 11 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 11 )*100 as numeric(15,2)) else 0 end   as Nov, " +
" 	case when EXTRACT(month from c.datacompetencia) = 12 then cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais' and EXTRACT(month from c.datacompetencia) = 12 )*100 as numeric(15,2)) else 0 end   as Dez,	cast( (coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)/12) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end/12 from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais'  )*100 as numeric(15,2))  as Media, " +
" 	cast( coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) / (select case when coalesce(sum(c.credito),0) = 0 then 1 else coalesce(sum(c.credito),0) end from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) where pc.descricao = 'Receitas operacionais'  )*100 as numeric(15,2))  as Total " +
" from planoconta pc inner join caixa c on (pc.id = c.idplanoconta) " +
" where pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais','Atividades de financiamento','Atividades de investimento') and  EXTRACT(year from c.datacompetencia) = @ano " +
" group by 1,2,3, EXTRACT(month from c.datacompetencia) " +
" ) as demonstrativo  " +
" group by grupo, categoria, ordem  " +
" Order by ordem	 ", parametros).ToList();
            return (from registro in resultado
                    select new RelDREDetalhado(registro.ordem, registro.grupo, registro.categoria, registro.jan, registro.fev, registro.mar, registro.abr, registro.mai, registro.jun, registro.jul, registro.ago, registro.set, registro.oct, registro.nov, registro.dez, registro.media, registro.total)).ToList();
        }

        public IEnumerable<RelFluxoDeCaixaGeralRealizado> ObterRelFluxoDeCaixaGeralRealizado(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                                " with ctedados as (   select  " +
                                " 		      c.datacompetencia as competencia, " +
                                " 		      'nome da pessoa' as nomepessoa, " +
                                "                       pc.categoria, " +
                                "                       c.descricao, " +
                                "                       c.credito as recebimentos, " +
                                "                       c.debito as pagamentos, " +
                                "                       coalesce(c.credito,0) - coalesce(c.debito,0) as saldodia " +
                                " from caixa c inner join conta co on (c.idconta = co.id)  left join planoconta pc on (pc.id = c.idplanoconta)" +
                                "  where c.id not in " +
                                " (select id from " +
                                " (select idcaixa as id from transferenciadestino " +
                                " union all " +
                                " select idcaixa as id from transferenciaorigem) as transferencia) " +
                                "),  " +
                                " ctedados2 as (select row_number() over(order by competencia) as seq,  " +
                                " 		   competencia, " +
                                " 		   nomepessoa, " +
                                " 		   categoria, " +
                                " 		   descricao, " +
                                " 		   recebimentos, " +
                                " 		   pagamentos,  " +
                                " 		   saldodia " +
                                " from ctedados)   " +
                                " select 	          cast( seq as int) as seq, " +
                                " 		   competencia, " +
                                " 		   nomepessoa, " +
                                " 		   categoria, " +
                                " 		   descricao, " +
                                " 		   recebimentos, " +
                                " 		   pagamentos,  " +
                                " 		   saldodia, " +
                                " (select  coalesce(a.saldodia, 0) + coalesce(sum(b.saldodia), 0)  from ctedados2 b where SEQ <= a.SEQ - 1 ) as diferencaacumuladadia " +
                                " from ctedados2 a  " +
                                " where competencia between @dataInicial and @dataFinal   " +
                                " order by 1    "
                , parametros).ToList();
            return (from registro in resultado
                    select new RelFluxoDeCaixaGeralRealizado(registro.seq, registro.competencia, registro.nomepessoa, registro.categoria, registro.descricao, registro.recebimentos, registro.pagamentos, registro.saldodia, registro.diferencaacumuladadia)).ToList();
        }

        public IEnumerable<RelFluxoDeCaixaGeralPrevisto> ObterRelFluxoDeCaixaGeralPrevisto(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                           "    with ctedados as (select  " +
                                           "     c.datacompetencia as competencia, " +
                                           "     'nome da pessoa' as nomepessoa, " +
                                           "             pc.categoria, " +
                                           "            c.descricao, " +
                                           "             c.credito as recebimentos, " +
                                           "            c.debito as pagamentos, " +
                                           "             coalesce(c.credito, 0) - coalesce(c.debito, 0) as saldodia " +
                                "  from caixa c inner " +
                                "  join conta co on (c.idconta = co.id)  left " +
                                "  join planoconta pc on (pc.id = c.idplanoconta) " +
                                "    where c.id not in  " +
                                "  (select id from " +
                                "  (select idcaixa as id from transferenciadestino " +
                                "  union all " +
                                "                        select idcaixa as id from transferenciaorigem) as transferencia) " +
                                "  union all " +
                                "  select " +
                                "                p.vencimento,  " +
                                "                pe.nome as nomepessoa,  " +
                                "                        pc.categoria,  " +
                                "                        p.descricao,  " +
                                "                        0 as recebimentos,  " +
                                "                        coalesce(p.valor, 0) as pagamentos,  " +
                                "                        coalesce(p.valor, 0) as saldodia " +
                                "  from pagar p left " +
                                "  join planoconta pc on (pc.id = p.idplanoconta) " +
                                "         left join pessoa pe on (pe.id = p.idpessoa) " +
                                "  where p.vencimento between current_date +1 and @dataFinal and p.valorpago = 0 " +
                                "     union all " +
                                "  select " +
                                "                p.vencimento,  " +
                                "                pe.nome as nomepessoa,  " +
                                "                        pc.categoria,  " +
                                "                        p.descricao,  " +
                                "                        coalesce(p.valor, 0) as recebimentos,  " +
                                "                        0 as pagamentos ,  " +
                                "                        coalesce(p.valor, 0) as saldodia " +
                                "  from receber p left " +
                                "   join planoconta pc on (pc.id = p.idplanoconta) " +
                                "  left join pessoa pe on (pe.id = p.idpessoa) " +
                                "   where p.vencimento between current_date +1 and @dataFinal and p.valorrecebido = 0 ),    " +
                                "  ctedados2 as (select row_number() over(order by competencia) as seq,   " +
                                "             competencia,  " +
                                "             nomepessoa,  " +
                                "             categoria,  " +
                                "             descricao,  " +
                                "             recebimentos,  " +
                                "             pagamentos,    " +
                                "             saldodia " +
                                "  from ctedados)    " +
                                "  select cast(seq as int) as seq,  " +
                                "          competencia,  " +
                                "          nomepessoa,  " +
                                " 		   categoria,  " +
                                " 		   descricao,  " +
                                " 		   recebimentos,  " +
                                " 		   pagamentos,   " +
                                " 		   saldodia,  " +
                                "  (select  coalesce(a.saldodia, 0) + coalesce(sum(b.saldodia), 0)  from ctedados2 b where SEQ <= a.SEQ - 1 ) as diferencaacumuladadia " +
                                "  from ctedados2 a " +
                                "  where competencia between @dataInicial and @dataFinal " +
                               "   order by 1", parametros).ToList();
            return (from registro in resultado
                    select new RelFluxoDeCaixaGeralPrevisto(registro.seq, registro.competencia, registro.nomepessoa, registro.categoria, registro.descricao, registro.recebimentos, registro.pagamentos, registro.saldodia, registro.diferencaacumuladadia)).ToList();
        }

        public IEnumerable<RelResumoGeral> ObterRelResumoGeral(int ano)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ano", ano);


            var resultado = DbContext.Query(
                                        " select  seq, " +
                        "         descricao, " +
                        "  	sum(Jan) as jan, " +
                        "  	sum(Fev) as fev, " +
                        "  	sum(Mar) as mar, " +
                        "  	sum(Abr) as abr, " +
                        "  	sum(Mai) as mai, " +
                        "  	sum(Jun) as jun, " +
                        "  	sum(Jul) as jul, " +
                        "  	sum(Ago) as ago, " +
                        "  	sum(Set) as set, " +
                        "  	sum(Out) as oct, " +
                        "  	sum(Nov) as nov, " +
                        "  	sum(Dez) as dez, " +
                        "  	sum(Media) as media, " +
                        "  	sum(Total) as total " +
                        " from( " +
                        " select  1 as seq, " +
                        " 	'Recebimentos' ::text as descricao, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.credito),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.credito),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.credito),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.credito),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.credito),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.credito),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.credito),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.credito),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.credito),0) else 0 end as Set, " +
                        "   	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) else 0 end as Nov, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) else 0 end as Dez, " +
                        "         cast(coalesce(sum(c.credito),0)/12 as numeric(15,2))  as Media, " +
                        "         coalesce(sum(c.credito),0) as Total " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                        " where EXTRACT(year from c.datacompetencia) = @ano  and pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais') " +
                        " group by EXTRACT(month from c.datacompetencia) " +
                        " union all " +
                        " select  2 as seq, " +
                        " 	'Pagamentos' ::text as descricao, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.debito),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.debito),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.debito),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.debito),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.debito),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.debito),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.debito),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.debito),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.debito),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.debito),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.debito),0) else 0 end as Nov, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.debito),0) else 0 end as Dez, " +
                        "  	cast(coalesce(sum(c.debito),0)/12 as numeric(15,2))  as Media, " +
                        "         coalesce(sum(c.debito),0) as Total " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                        " where EXTRACT(year from c.datacompetencia) = @ano  and pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais') " +
                        " group by EXTRACT(month from c.datacompetencia) " +
                        " union all " +
                        " select  3 as seq, " +
                        " 	'Resultado Operacional' ::text as descricao, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Nov, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Dez, " +
                        "  	cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) /12 as numeric(15,2))  as Media, " +
                        "         coalesce(sum(c.credito),0)- coalesce(sum(c.debito),0) as Total " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                        " where EXTRACT(year from c.datacompetencia) = @ano  and pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais') " +
                        " group by EXTRACT(month from c.datacompetencia) " +
                        " union all " +
                        " select  4 as seq, " +
                        " 	'Resultado de Investimentos' ::text as descricao, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Nov, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Dez, " +
                        "  	cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) /12 as numeric(15,2))  as Media, " +
                        "         coalesce(sum(c.credito),0)- coalesce(sum(c.debito),0) as Total " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                        " where EXTRACT(year from c.datacompetencia) = @ano  and pc.descricao in ('Atividades de investimento') " +
                        " group by EXTRACT(month from c.datacompetencia) " +
                        " union all " +
                        " select  5 as seq, " +
                        " 	'Resultado de Financiamentos' ::text as descricao, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Nov, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Dez, " +
                        "  	cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) /12 as numeric(15,2))  as Media, " +
                        "        coalesce(sum(c.credito),0)- coalesce(sum(c.debito),0) as Total " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                        " where EXTRACT(year from c.datacompetencia) = @ano  and pc.descricao in ('Atividades de financiamento') " +
                        " group by EXTRACT(month from c.datacompetencia) " +
                        " union all " +
                        " select  6 as seq, " +
                        " 	'Variação de Caixa' ::text as descricao, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 1 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 2 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 3 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 4 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 5 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 6 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 7 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 8 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 9 then   coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 10 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 11 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Nov, " +
                        "  	case when EXTRACT(month from c.datacompetencia) = 12 then  coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0) else 0 end as Dez, " +
                        "  	cast((coalesce(sum(c.credito),0) - coalesce(sum(c.debito),0)) /12 as numeric(15,2))  as Media, " +
                        "        coalesce(sum(c.credito),0)- coalesce(sum(c.debito),0) as Total " +
                        " from caixa c inner join planoconta pc on (c.idplanoconta = pc.id) " +
                        " where EXTRACT(year from c.datacompetencia) = @ano  and pc.descricao in ('Deduções','Receitas operacionais','Custos','Despesas operacionais','Atividades de financiamento','Atividades de investimento') " +
                        " group by EXTRACT(month from c.datacompetencia) " +
                        " union all " +
                        " select  7 as seq, " +
                        " 	'Contas a Pagar' ::text as descricao,  " +
                        "  	case when EXTRACT(month from r.vencimento) = 1 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from r.vencimento) = 2 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from r.vencimento) = 3 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from r.vencimento) = 4 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from r.vencimento) = 5 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from r.vencimento) = 6 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from r.vencimento) = 7 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from r.vencimento) = 8 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from r.vencimento) = 9 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from r.vencimento) = 10 and r.vencimento >= current_date then  coalesce(sum(r.valor),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from r.vencimento) = 11 and r.vencimento >= current_date then  coalesce(sum(r.valor),0) else 0 end as Nov, " +
                        "  	case when EXTRACT(month from r.vencimento) = 12 and r.vencimento >= current_date then  coalesce(sum(r.valor),0) else 0 end as Dez, " +
                        "  	0  as Media, " +
                        "        0 as Total " +
                        " from pagar r  " +
                        " where EXTRACT(year from r.vencimento) = @ano and r.valorpago = 0  " +
                        " group by EXTRACT(month from r.vencimento), r.vencimento " +
                        " union all " +
                        " select  8 as seq, " +
                        " 	'Contas a Pagar Vencidas' ::text as descricao, " +
                        "  	case when EXTRACT(month from r.vencimento) = 1 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from r.vencimento) = 2 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from r.vencimento) = 3 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from r.vencimento) = 4 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from r.vencimento) = 5 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from r.vencimento) = 6 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from r.vencimento) = 7 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from r.vencimento) = 8 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from r.vencimento) = 9 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from r.vencimento) = 10 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from r.vencimento) = 11 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Nov, " +
                        "  	case when EXTRACT(month from r.vencimento) = 12 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Dez, " +
                        "  	0  as Media, " +
                        "         0 as Total " +
                        " from pagar r  " +
                        " where EXTRACT(year from r.vencimento) = @ano and r.valorpago = 0  " +
                        " group by EXTRACT(month from r.vencimento), r.vencimento " +
                        " union all " +
                        " select  9 as seq, " +
                        " 	'Contas a Receber' ::text as descricao,  " +
                        "  	case when EXTRACT(month from r.vencimento) = 1 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from r.vencimento) = 2 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from r.vencimento) = 3 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from r.vencimento) = 4 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from r.vencimento) = 5 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from r.vencimento) = 6 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from r.vencimento) = 7 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from r.vencimento) = 8 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from r.vencimento) = 9 and r.vencimento >= current_date then   coalesce(sum(r.valor),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from r.vencimento) = 10 and r.vencimento >= current_date then  coalesce(sum(r.valor),0) else 0 end as Out, " +
                        "  	case when EXTRACT(month from r.vencimento) = 11 and r.vencimento >= current_date then  coalesce(sum(r.valor),0) else 0 end as Nov,  " +
                        "  	case when EXTRACT(month from r.vencimento) = 12 and r.vencimento >= current_date then  coalesce(sum(r.valor),0) else 0 end as Dez, " +
                        "  	0  as Media, " +
                        "         0 as Total " +
                        " from receber r  " +
                        " where EXTRACT(year from r.vencimento) = @ano and r.valorrecebido = 0  " +
                        " group by EXTRACT(month from r.vencimento), r.vencimento " +
                        " union all " +
                        " select  10 as seq, " +
                        " 	'Contas a Receber Atrasadas' ::text as descricao, " +
                        "  	case when EXTRACT(month from r.vencimento) = 1 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Jan, " +
                        "  	case when EXTRACT(month from r.vencimento) = 2 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Fev, " +
                        "  	case when EXTRACT(month from r.vencimento) = 3 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Mar, " +
                        "  	case when EXTRACT(month from r.vencimento) = 4 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Abr, " +
                        "  	case when EXTRACT(month from r.vencimento) = 5 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Mai, " +
                        "  	case when EXTRACT(month from r.vencimento) = 6 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Jun, " +
                        "  	case when EXTRACT(month from r.vencimento) = 7 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Jul, " +
                        "  	case when EXTRACT(month from r.vencimento) = 8 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Ago, " +
                        "  	case when EXTRACT(month from r.vencimento) = 9 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Set, " +
                        "  	case when EXTRACT(month from r.vencimento) = 10 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Out,  " +
                        "  	case when EXTRACT(month from r.vencimento) = 11 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Nov,  " +
                        "  	case when EXTRACT(month from r.vencimento) = 12 and r.vencimento < current_date then coalesce(sum(r.valor),0) else 0 end as Dez, " +
                        "  	0  as Media, " +
                        "         0 as Total " +
                        " from receber r  " +
                        " where EXTRACT(year from r.vencimento) = @ano and r.valorrecebido = 0  " +
                        " group by EXTRACT(month from r.vencimento), r.vencimento " +
                        " ) as resultado " +
                        " group by 1,2 " +
                        " order by 1 "

                , parametros).ToList();
            return (from registro in resultado
                    select new RelResumoGeral(registro.seq, registro.descricao, registro.jan, registro.fev, registro.mar, registro.abr, registro.mai, registro.jun, registro.jul, registro.ago, registro.set, registro.oct, registro.nov, registro.dez, registro.media, registro.total)).ToList();
        }
    }
}

