using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Core.Contexts.Interfaces;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Dados.Repositories
{
    public class TransferenciaRepository : Repository<Transferencia>, ITransferenciaRepository
    {
        public TransferenciaRepository(IDapperContext dbContext) 
            : base(dbContext)
        { }

        public IEnumerable<TransferenciaLista> ObterTransferencias()
        {
            const string sql = "select t.id, " +
                               "       t.datatransferencia," +
                               "       t.valor, " +
                               "       cto.nome as nomecontaorigem, " +
                               "       ctd.nome as nomecontadestino " +
                               "from transferencia t " +
                               "left join transferenciaorigem trfo on trfo.id = t.idtransferenciaorigem " +
                               "left join conta cto on cto.id = trfo.idconta " +
                               "left join transferenciadestino trfd on trfd.id = t.idtransferenciadestino " +
                               "left join conta ctd on ctd.id = trfd.idconta";
            var resultado = DbContext.Query(sql);
            return (from registro in resultado
                        select new TransferenciaLista(registro.id, registro.datatransferencia, registro.valor, registro.nomecontaorigem, registro.nomecontadestino)).ToList();
        }
    }
}