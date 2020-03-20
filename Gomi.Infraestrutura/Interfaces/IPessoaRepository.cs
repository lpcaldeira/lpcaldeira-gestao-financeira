using Gomi.Infraestrutura.Models.Pessoas;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Enums;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<IEnumerable<Pessoa>> ObterPessoasParaPagamentoPorNome(string nome);
        Task<IEnumerable<Pessoa>> ObterPessoasParaRecebimentoPorNome(string nome);
        Task<IEnumerable<Pessoa>> ObterPessoaPorNomeETipoTransacionador(string nome, Transacionador transacionador);
        IEnumerable<ListaPessoas> ObterListaPessoas();
        IEnumerable<ListaPessoasDetalhado> ObterListaPessoasDetalhado();
        IEnumerable<ListaMovimentacaoHistorico> ObterListaMovimentacaoHistorico(DateTime dataInicial, DateTime dataFinal);
    }
}
