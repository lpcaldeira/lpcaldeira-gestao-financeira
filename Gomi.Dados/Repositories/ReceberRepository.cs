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
    public class ReceberRepository : Repository<Receber>, IReceberRepository
    {
        public ReceberRepository(IDapperContext dapperContext)
            : base(dapperContext)
        { }

        public decimal ObterTotalEmAbertoDeContasReceberHoje()
        {
            return DbContext.Query<decimal>("select coalesce(sum(r.valor),0) from receber r             " +
                                             "where r.datarecebimento is null  and r.vencimento = current_date").Single();
        }

        public decimal ObterTotalEmAbertoDeContasReceberNoPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(sum(r.valor),0) from receber r             " +
                                             "where r.datarecebimento is null and r.vencimento between @dataInicial and @dataFinal", parametros).Single();
        }

        public IEnumerable<RecebimentosCliente> ObterTotalEmAbertoDeContasReceberNoPeriodoCliente()
        {
            var resultado = DbContext.Query("select p.nome as pessoa, coalesce(sum(r.valor),0) as valor from receber r   inner join pessoa p on (r.idpessoa = p.id)           " +
                                             "where r.datarecebimento is null  group by 1 ").ToList();
            return (from registro in resultado
                    select new RecebimentosCliente(registro.pessoa, registro.valor)).ToList();
        }
        public IEnumerable<RecebimentosAtrasadoCliente> ObterTotalAtrasadoDeContasReceberNoPeriodoCliente()
        {
            var resultado = DbContext.Query("select p.nome as pessoa, coalesce(sum(r.valor),0) as valor from receber r  inner join pessoa p on (r.idpessoa = p.id)            " +
                                             "where r.datarecebimento is null and r.vencimento < current_date group by 1 ").ToList();
            return (from registro in resultado
                    select new RecebimentosAtrasadoCliente(registro.pessoa, registro.valor)).ToList();
        }
        public IEnumerable<RecebimentosDetalhadoCliente> ObterTotalEmAbertoDeContasReceberNoPeriodoDetalhadoCliente()
        {
            var resultado = DbContext.Query(" select p.nome as  pessoa, coalesce(r.valor,0) as valor, r.vencimento, r.descricao, " +
                                            " 	case when r.valorrecebido > 0 then 'Quitado' " +
                                            " 	     when r.vencimento < current_date and r.datarecebimento is null then 'Atrasado' " +
                                            " 	     when r.vencimento >= current_date and r.datarecebimento  is null  then 'Em Aberto' end as situacao " +
                                            " from receber r  inner join pessoa p on (r.idpessoa = p.id)" +
                                            " where r.datarecebimento is null  " +
                                            " order by 3  ").ToList();
            return (from registro in resultado
                    select new RecebimentosDetalhadoCliente(registro.pessoa, registro.valor, registro.vencimento, registro.descricao, registro.situacao)).ToList();
        }

        public IEnumerable<ContasReceberTransacionadores> ObterContasReceberTransacionadores()
        {
            var resultado = DbContext.Query("select r.transacionador, coalesce(sum(r.valor), 0) as valor from receber r  " +
                                            "where r.datarecebimento is null group by 1").ToList();
            return (from registro in resultado
                    select new ContasReceberTransacionadores(registro.transacionador, registro.valor)).ToList();
        }

        public decimal ResultadoPrevistoRecebido(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(sum(r.valorrecebido),0) from receber r where r.vencimento between  @dataInicial and @dataFinal ", parametros).Single();
        }

        public decimal ResultadoPrevistoAVencer(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(sum(r.valor),0) from receber r             " +
                                             "where r.datarecebimento is null and r.vencimento between  @dataInicial and @dataFinal ", parametros).Single();
        }

        public decimal ResultadoPrevistoVencido(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            return DbContext.Query<decimal>("select coalesce(sum(r.valor),0) from receber r             " +
                                             "where r.datarecebimento is null and r.vencimento < current_date and  r.vencimento between  @dataInicial and @dataFinal", parametros).Single();
        }

        public IEnumerable<Receber> ObterRecebimentos()
        {
            return DbContext.Query<Receber>("select * from receber");
        }

        public IEnumerable<ListaReceber> ObterListaRecebimentos()
        {
            var resultado = DbContext.Query(" select r.id, CAST(r.vencimento AS DATE) as vencimento, cast(coalesce(r.datarecebimento,'01/01/2001') as date) as datarecebimento, p.id as idpessoa,  p.nome as pessoa, " +
                                                 " pc.categoria as categoria, r.descricao, r.valor, r.valorrecebido " +
                                                 " from receber r inner join pessoa p on (r.idpessoa = p.id) " +
                                                 " left join planoconta pc on (pc.id = r.idplanoconta) order by 2,3").ToList(); ;
            return (from registro in resultado
                    select new ListaReceber(registro.id, registro.vencimento, registro.datarecebimento, registro.idpessoa, registro.pessoa, registro.categoria, registro.descricao, registro.valor, registro.valorrecebido)).ToList();
        }


        public IEnumerable<RelRecebimentosPorDia> ObterRelRecebimentosPorDia(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                    " with receberdiario as " +
                                    " (select r.datarecebimento  as competencia, " +
                                    "                           CASE EXTRACT(DOW FROM datarecebimento)  " +
                                    "                                 WHEN 0 THEN 'Domingo'  " +
                                    "                                 WHEN 1 THEN 'Segunda'  " +
                                    "                                 WHEN 2 THEN 'Terça'  " +
                                    "                                 WHEN 3 THEN 'Quarta'  " +
                                    "                                 WHEN 4 THEN 'Quinta'  " +
                                    "                                 WHEN 5 THEN 'Sexta'  " +
                                    "                                 WHEN 6 THEN 'Sábado'  " +
                                    "                           END AS diasemana,  " +
                                    "                           sum(r.valorrecebido) as valor,  " +
                                    "                           cast((sum(valorrecebido) / (select sum(valorrecebido) from receber re where re.valorrecebido > 0 and " +
                                    " re.datarecebimento between @dataInicial and @dataFinal)) * 100 as numeric(15, 2)) as percentual  " +
                                    " from receber r  " +
                                    " where r.valorrecebido > 0 and r.datarecebimento between @dataInicial and @dataFinal  " +
                                    " group by r.datarecebimento ),  " +
                                    "     receberdiario2 as  " +
                                    " (select row_number() over(order by competencia) as seq,  " +
                                    " 	competencia,  " +
                                    " 	diasemana,  " +
                                    " 	valor, " +
                                    " 	percentual  " +
                                    "  from receberdiario ) " +
                                    " select cast(seq as int) as seq, competencia , diasemana, valor, percentual,  " +
                                    " ( select  coalesce(a.valor, 0) + coalesce(sum(b.valor), 0)  from receberdiario2 b where b.SEQ <= a.SEQ - 1  ) as valoracumulado,  " +
                                    " ( select  coalesce(a.percentual, 0) + coalesce(sum(b.percentual), 0)  from receberdiario2 b where b.SEQ <= a.SEQ - 1 ) as percentualacumulado  " +
                                    " from receberdiario2 a   " +
                                    " order by 1 ", parametros ).ToList();
            return (from registro in resultado
                    select new RelRecebimentosPorDia(registro.seq  , registro.competencia, registro.diasemana, registro.valor, registro.percentual, registro.valoracumulado, 
                    registro.percentualacumulado)).ToList();
        }

        public IEnumerable<RelRecebimentosPorTipoPessoa> ObterRelRecebimentosPorTipoPessoa(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                  "  select r.id, cast(p.transacionadorvalor as int) as transacionadorvalor , p.transacionador, r.datarecebimento, p.nome, pc.categoria, r.descricao, r.valorrecebido as valor " +
                                  "  from receber r inner " +
                                  "  join pessoa p on (r.idpessoa = p.id) " +
                                  "  inner join planoconta pc on (pc.id = r.idplanoconta) " +
                                  "  where r.datarecebimento between @dataInicial and @dataFinal and r.valorrecebido > 0", parametros).ToList();
            return (from registro in resultado
                    select new RelRecebimentosPorTipoPessoa(registro.id, registro.transacionadorvalor, registro.transacionador, registro.datarecebimento, registro.nome, registro.categoria, registro.descricao, registro.valor)).ToList();
        }

        public IEnumerable<RelRecebimentosPorCategoria> ObterRelRecebimentosPorCategoria(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                                "select pc.categoria, sum(r.valorrecebido) as valor, " +
                                "cast((sum(valorrecebido) / (select sum(valorrecebido) from receber re inner join planoconta pc on (pc.id = re.idplanoconta) where re.valorrecebido > 0 and re.datarecebimento between @dataInicial and @dataFinal)) * 100 as numeric(15, 2)) as percentual " +
                                "from receber r inner " +
                                "join planoconta pc on (pc.id = r.idplanoconta) " +
                                "where r.datarecebimento between  @dataInicial and @dataFinal and r.valorrecebido > 0 " +
                                "group by 1", parametros).ToList();
            return (from registro in resultado
                    select new RelRecebimentosPorCategoria(registro.categoria, registro.valor, registro.percentual)).ToList();
        }

        public IEnumerable<RelRelacaoRecebimentos> ObterRelRelacaoRecebimentos(DateTime dataInicial, DateTime dataFinal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(
                             
                                " select r.datarecebimento as data, " +
                                " 	p.nome, " +
                                " 	pc.categoria, " +
                                " 	r.descricao, " +
                                " 	r.valorrecebido as valor " +
                                " from receber r inner join planoconta pc on (pc.id = r.idplanoconta)	 " +
                                " 	       inner join pessoa p on (p.id = r.idpessoa)	 " +
                                " where r.datarecebimento between @dataInicial and @dataFinal and r.valorrecebido > 0 " +
                                " order by 1 "
                                , parametros).ToList();
            return (from registro in resultado
                    select new RelRelacaoRecebimentos(registro.data, registro.nome, registro.categoria, registro.descricao, registro.valor)).ToList();
        }

    }
}


