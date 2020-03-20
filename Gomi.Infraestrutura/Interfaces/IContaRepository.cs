using Gomi.Infraestrutura.Models.Financeiro;

using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using System;
using System.Collections.Generic;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IContaRepository : IRepository<Conta>
    {
        IEnumerable<ContaExtrato> ObterContaExtrato();
        IEnumerable<ListaConta> ObterListaConta();
    }
}
