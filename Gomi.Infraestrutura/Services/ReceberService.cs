using System;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Services
{
    public class ReceberService : IReceberService
    {
        private readonly IReceberRepository _receberRepository;
        private readonly ICaixaService _caixaService;

        public ReceberService(IReceberRepository receberRepository,
            ICaixaService caixaService)
        {
            _receberRepository = receberRepository;
            _caixaService = caixaService;
        }

        public async Task<IEnumerable<Receber>> ObterRecebimentos()
        {
            var resultado = _receberRepository.ObterRecebimentos();
            return await Task.Factory.StartNew(() => resultado);
        }

        public async void Salvar(Receber recebimento)
        {
            if (recebimento.IdContaRecebimento == 0)
            {
                recebimento.DataRecebimento = null;
                recebimento.IdContaRecebimento = null;
                recebimento.ValorRecebido = 0;
            }

            SalvarReceber(recebimento);

            if (recebimento.ValorRecebido == 0 || recebimento.IdContaRecebimento == null) return;
            if (recebimento.DataRecebimento == null || recebimento.DataRecebimento == DateTime.MinValue)
                recebimento.DataRecebimento = DateTime.Now;

            var lancamentoCaixa = await _caixaService.ObterPorId(recebimento.IdCaixa ?? 0) ?? new Caixa();
            lancamentoCaixa.DataCompetencia = recebimento.DataRecebimento ?? DateTime.Now;
            lancamentoCaixa.IdConta = recebimento.IdContaRecebimento ?? 0;
            lancamentoCaixa.NomeConta = recebimento.NomeContaRecebimento;
            lancamentoCaixa.IdEmpresa = recebimento.IdEmpresa;
            lancamentoCaixa.IdCentroCusto = recebimento.IdCentroCusto;
            lancamentoCaixa.DescricaoCentroCusto = recebimento.DescricaoCentroCusto;
            lancamentoCaixa.IdPlanoConta = recebimento.IdPlanoConta;
            lancamentoCaixa.DescricaoPlanoConta = recebimento.DescricaoPlanoConta;
            lancamentoCaixa.Descricao = "Lançamento de recebimento";
            lancamentoCaixa.Credito = recebimento.ValorRecebido;

            _caixaService.Salvar(lancamentoCaixa);
            recebimento.IdCaixa = lancamentoCaixa.Id;

            SalvarReceber(recebimento);
        }

        public void Excluir(Receber recebimento)
        {
            if (recebimento.Caixa != null)
                _caixaService.Excluir(recebimento.Caixa);
            _receberRepository.Delete(recebimento);
        }

        private void SalvarReceber(Receber recebimento)
        {
            if (recebimento.Id == 0)
                _receberRepository.Add(recebimento);
            else
                _receberRepository.Update(recebimento);
        }

        public Receber ObterPorId(int id)
        {
            return _receberRepository.GetById(id).Result;
        }
    }
}
