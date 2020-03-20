using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Core.Contexts.Interfaces;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro;
using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using Dapper;
using System;
using System.Linq;

namespace Gomi.Dados.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(IDapperContext dapperContext)
            : base(dapperContext)
        { }

        public IEnumerable<ContaExtrato> ObterContaExtrato()
        {
            var resultado = DbContext.Query("with ctedados as (select co.id as idconta, co.nome as conta, " +
                                     "                           c.datacompetencia as dia, " +
                                     "                           'Decricao Pessoa' as pessoa, " +
                                     "                           pc.categoria, " +
                                     "                           c.descricao, " +
                                     "                           coalesce(c.credito, 0) as recebimentos, " +
                                     "                           coalesce(c.debito, 0) as pagamentos, " +
                                     "                           coalesce(c.credito, 0) - coalesce(c.debito, 0) as saldo " +
                                     "                     from caixa c left " +
                                     "                     join planoconta pc on (c.idplanoconta = pc.id) " +
                                     "                inner join conta co on (co.id = c.idconta)), " +
                                     "      ctedados2 as (select row_number() over(order by dia) as seq,  " +
                                     "                           idconta, conta, " +
                                     "                            dia, " +
                                     "                           pessoa, " +
                                     "                           categoria, " +
                                     "                           descricao, " +
                                     "                          recebimentos, " +
                                     "                           pagamentos, " +
                                     "                           saldo " +
                                     "                      from ctedados)  " +
                                     " select seq, idconta, conta, dia, pessoa, categoria, descricao, recebimentos, pagamentos, saldo, " +
                                     "       (select  coalesce(a.saldo, 0) + coalesce(sum(b.saldo), 0)  from ctedados2 b where SEQ <= a.SEQ - 1 and a.idconta = b.idconta ) as diferencaacumuladadia " +
                                     " from ctedados2 a " +
                                     " order by 1 ").ToList();
            return (from registro in resultado
                    select new ContaExtrato(registro.seq, registro.idconta, registro.conta, registro.dia, registro.pessoa, registro.categoria, registro.descricao, registro.recebimentos, registro.pagamentos, registro.saldo, registro.diferencaacumuladadia)).ToList();
        }

        public IEnumerable<ListaConta> ObterListaConta()
        {
            var resultado = DbContext.Query(" select c.id, c.nome, c.numeroconta as numero, c.tipoconta as tipo, cast( coalesce(sum(cx.credito), 0) - coalesce(sum(cx.debito), 0) as numeric(15,2)) as saldo " +
                                            " from conta c left join caixa cx on (c.id = cx.idconta) " +
                                            " group by 1,2,3,4 order by 2 ").ToList();
            return (from registro in resultado
                    select new ListaConta(registro.id, registro.nome, registro.numero, registro.tipo, registro.saldo)).ToList();
        }




    }
}
