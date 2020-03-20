using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Core.Contexts.Interfaces;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Pessoas;
using System.Linq;
using Dapper;
using System;
using Gomi.Infraestrutura.Enums;

namespace Gomi.Dados.Repositories
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(IDapperContext dapperContext)
            : base(dapperContext)
        { }

        public override async Task<IEnumerable<Pessoa>> GetAll()
        {
            const string sql = "select * from pessoa";
            return await DbContext.QueryManyAsync<Pessoa>(sql);
        }

        public async Task<IEnumerable<Pessoa>> ObterPessoasParaPagamentoPorNome(string nome)
        {
            const string sql = "select * from pessoa p                       " +
                               "where lower(p.nome) like @nome and           "+
                               "     (p.transacionadorvalor = @fornecedor or " +
                               "      p.transacionadorvalor = @socio)        ";

            var parametros = new DynamicParameters();
            parametros.Add("@nome", $"%{nome}%", System.Data.DbType.String);
            parametros.Add("@fornecedor", (int)Transacionador.Fornecedor, System.Data.DbType.String);
            parametros.Add("@socio", (int)Transacionador.Socio, System.Data.DbType.String);

            return await DbContext.QueryManyAsync<Pessoa>(sql, parametros);
        }

        public async Task<IEnumerable<Pessoa>> ObterPessoasParaRecebimentoPorNome(string nome)
        {
            const string sql = "select * from pessoa p                     " +
                               "where lower(p.nome) like @nome and         "+
                               "      (p.transacionadorvalor = @cliente or " +
                               "       p.transacionadorvalor = @socio)      ";

            var parametros = new DynamicParameters();
            parametros.Add("@nome", $"%{nome}%", System.Data.DbType.String);
            parametros.Add("@cliente", (int)Transacionador.Cliente, System.Data.DbType.String);
            parametros.Add("@socio", (int)Transacionador.Socio, System.Data.DbType.String);

            return await DbContext.QueryManyAsync<Pessoa>(sql, parametros);
        }

        public async Task<IEnumerable<Pessoa>> ObterPessoaPorNomeETipoTransacionador(string nome, Transacionador transacionador)
        {
            const string sql = "select * from pessoa p                        " +
                               "where lower(p.nome) like @nome and            " +
                               "      p.transacionadorvalor = @transacionador "; 

            var parametros = new DynamicParameters();
            parametros.Add("@nome", $"%{nome?.ToLowerInvariant()}%", System.Data.DbType.String);
            parametros.Add("@transacionador", (int)Transacionador.Cliente, System.Data.DbType.String);

            return await DbContext.QueryManyAsync<Pessoa>(sql, parametros);
        }

        public IEnumerable<ListaPessoas> ObterListaPessoas()
        {
                       var resultado = DbContext.Query(" select id, transacionador, razaosocial, nome, cnpj, cpf, endereco, numero, bairro, cidade, uf, telefone, pessoacontato, email, tipo, " +
"                                                          sum(coalesce(emaberto,0)) as emaberto, sum(coalesce(vencido,0)) as vencido " +
"                                                         from(  " +
"                                                         select p.id,   " +
"                                                               p.transacionador,  " +
"                                                               p.razaosocial, " +
"                                                         		p.nome,   " +
"                                                         		p.cnpj,  " +
"                                                                p.cpf,  " +
"                                                                p.endereco,  " +
"                                                                p.numero,  " +
"                                                                p.bairro,  " +
"                                                                p.cidade,  " +
"                                                                p.uf,  " +
"                                                                p.telefone,  " +
"                                                                p.pessoacontato,  " +
"                                                                p.email,  " +
"                                                                'Receber' as tipo,   "+  
"                                                           	  	case when re.datarecebimento is null  then sum(coalesce(re.valor,0)) end as emaberto,  " +
"                                                              	case when re.datarecebimento is null and re.vencimento < current_date then sum(coalesce(re.valor,0)) end as vencido " +
"                                                         from pessoa p left join receber re on (p.id = re.idpessoa)   " +
"                                                         group by 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15, re.vencimento, re.datarecebimento  " +
"                                                         union all " +
"                                                         select p.id,   " +
"                                                               p.transacionador,  " +
"                                                               p.razaosocial, " +
"                                                         		p.nome,   " +
"                                                         		p.cnpj,  " +
"                                                                p.cpf,  " +
"                                                                p.endereco,  " +
"                                                                p.numero,  " +
"                                                                p.bairro,  " +
"                                                                p.cidade,  " +
"                                                                p.uf,  " +
"                                                                p.telefone,  " +
"                                                                p.pessoacontato,  " +
"                                                                p.email,  " +
"                                                                'Pagar' as tipo,"+
"                                                           	  	case when re.datapagamento is null  then sum(coalesce(re.valor,0)) end as emaberto,  " +
"                                                              	case when re.datapagamento is null and re.vencimento < current_date then sum(coalesce(re.valor,0)) end as vencido " +
"                                                         from pessoa p left join pagar re on (p.id = re.idpessoa)   " +
"                                                         group by 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15, re.vencimento, re.datapagamento ) as receberpagar " +
"                                                         group by 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15 order by 4"

                                                         ).ToList();
            return (from registro in resultado
                    select new ListaPessoas(registro.id, registro.transacionador, registro.razaosocial, registro.nome, registro.cnpj, registro.cpf, registro.endereco,registro.numero, registro.bairro, registro.cidade, registro.uf, registro.telefone, registro.pessoacontato, registro.email, registro.tipo,  registro.emaberto, registro.vencido)).ToList();
        }

        public IEnumerable<ListaPessoasDetalhado> ObterListaPessoasDetalhado()
        {
            var resultado = DbContext.Query(" select id, nome, categoria, descricao, vencimento, emissao, situacao, valor, emaberto, vencido, recebido, tipo " +
"                                           from ( " +
"                                           select p.id,  " +
"                                              		p.nome, " +
"                                                     pc.categoria as categoria,  " +
"                                                     re.descricao,  " +
"                                                     re.vencimento,  " +
"                                                     re.emissao,  " +
"                                                     case when re.datarecebimento is null and re.vencimento >= current_date then 'Em Aberto'  " +
"                                                     	 when re.datarecebimento is null and re.vencimento < current_date then 'Vencido'  " +
"                                                          else 'Recebido' end as situacao,  " +
"                                                     coalesce(re.valor,0) as valor,  " +
"                                                     case when re.datarecebimento is null and re.vencimento >= current_date then coalesce(re.valor,0) else 0 end as emaberto,  " +
"                                                  	case when re.datarecebimento is null and re.vencimento < current_date then coalesce(re.valor,0) else 0 end as vencido,  " +
"                                                     case when re.valorrecebido > 0 then coalesce(re.valorrecebido,0) else 0 end as recebido, 'Receber' as tipo  " +
"                                              from pessoa p inner join receber re on (p.id = re.idpessoa)  " +
"                                              			   left join planoconta pc on (pc.id = re.idplanoconta) where re.datarecebimento is null " +
"                                                           union all" +
"                                                    select p.id,  " +
"                                              		p.nome, " +
"                                                     pc.categoria as categoria,  " +
"                                                     re.descricao,  " +
"                                                     re.vencimento,  " +
"                                                     re.emissao,  " +
"                                                     case when re.datapagamento is null and re.vencimento >= current_date then 'Em Aberto'  " +
"                                                     	 when re.datapagamento is null and re.vencimento < current_date then 'Vencido'  " +
"                                                          else 'Pago' end as situacao,  " +
"                                                     coalesce(re.valor,0) as valor,  " +
"                                                     case when re.datapagamento is null and re.vencimento >= current_date then coalesce(re.valor,0) else 0 end as emaberto,  " +
"                                                  	case when re.datapagamento is null and re.vencimento < current_date then coalesce(re.valor,0) else 0 end as vencido,  " +
"                                                     case when re.valorpago > 0 then coalesce(re.valorpago,0) else 0  end as recebido, 'Pagar' as tipo  " +
"                                              from pessoa p inner join pagar re on (p.id = re.idpessoa)  " +
"                                              			   left join planoconta pc on (pc.id = re.idplanoconta) where re.datapagamento is null " +
"       ) as receberpagar    order by 5                                  ").ToList();
            return (from registro in resultado
                    select new ListaPessoasDetalhado(registro.id, registro.nome, registro.categoria, registro.descricao, registro.vencimento, registro.emissao, registro.situacao, registro.valor, registro.emaberto, registro.vencido, registro.recebido, registro.tipo)).ToList();
        }

        public IEnumerable<ListaMovimentacaoHistorico> ObterListaMovimentacaoHistorico(DateTime dataInicial, DateTime dataFinal)

        {
            var parametros = new DynamicParameters();
            parametros.Add("@dataInicial", dataInicial, System.Data.DbType.DateTime);
            parametros.Add("@dataFinal", dataFinal, System.Data.DbType.DateTime);

            var resultado = DbContext.Query(" select id, nome, vencimento, emissao, coalesce(sum(valor),0)as valor , coalesce(sum(emaberto),0) as emaberto, " +
                                            " coalesce(sum(vencido),0)as vencido, coalesce(sum(recebido),0) as recebido, tipo " +
                                            " from (  " +
                                            " select p.id,   " +
                                            "                                              		p.nome,  " +
                                            "               										cast( '01'  || '/' ||  EXTRACT(month from re.vencimento) ||'/'|| EXTRACT(year from re.vencimento) as date) as vencimento, " +
                                            "                                                    cast( '01'  || '/' ||  EXTRACT(month from re.emissao) ||'/'|| EXTRACT(year from re.emissao) as date) as emissao, " +
                                            "                                                    coalesce(sum(re.valor),0) as valor,   " +
                                            "                                                    case when re.datarecebimento is null and re.vencimento >= current_date then coalesce(sum(re.valor),0) else 0 end as emaberto,   " +
                                            "                                                  	case when re.datarecebimento is null and re.vencimento < current_date then coalesce(sum(re.valor),0) else 0 end as vencido,   " +
                                            "                                                    case when re.valorrecebido > 0 then coalesce(sum(re.valorrecebido),0) else 0 end as recebido, 'Receber' as tipo     " +
                                            "                                              from pessoa p inner join receber re on (p.id = re.idpessoa)   " +
                                            "                                              			   left join planoconta pc on (pc.id = re.idplanoconta) " +
                                            "                                              group by 1,2,3,4, re.datarecebimento, re.vencimento, RE.valorrecebido,9 " +
                                            " union all " +
                                            " select p.id, " +
                                            "                                              		p.nome,  " +
                                            "                                                    cast( '01'  || '/' ||  EXTRACT(month from re.vencimento) ||'/'|| EXTRACT(year from re.vencimento) as date) as vencimento, " +
                                            "                                                    cast( '01'  || '/' ||  EXTRACT(month from re.emissao) ||'/'|| EXTRACT(year from re.emissao) as date) as emissao,   " +
                                            "                                                     coalesce(sum(re.valor),0) as valor,   " +
                                            "                                                     case when re.datapagamento is null and re.vencimento >= current_date then coalesce(sum(re.valor),0) else 0 end as emaberto,   " +
                                            "                                                  	case when re.datapagamento is null and re.vencimento < current_date then coalesce(sum(re.valor),0) else 0 end as vencido,   " +
                                            "                                                     case when re.valorpago > 0 then coalesce(sum(re.valorpago),0) else 0 end as recebido, 'Pagar' as tipo    " +
                                            "                                              from pessoa p inner join pagar re on (p.id = re.idpessoa)   " +
                                            "                                              			   left join planoconta pc on (pc.id = re.idplanoconta) 											 " +
                                            "                                                            group by 1,2,3,4, re.datapagamento, re.vencimento, re.valorpago,9 " +
                                            "       ) as receberpagar " +
                                            "        group by 1,2,3,4,9 order by 3 ", parametros).ToList();
            return (from registro in resultado
                    select new ListaMovimentacaoHistorico(registro.id, registro.nome,  registro.vencimento, registro.emissao,  registro.valor, registro.emaberto, registro.vencido, registro.recebido, registro.tipo)).ToList();
        }
        
    }
}
