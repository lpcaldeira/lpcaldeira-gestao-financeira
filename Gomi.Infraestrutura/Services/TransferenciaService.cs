using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Infraestrutura.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ITransferenciaRepository _transferenciaRepository;
        private readonly ICaixaService _caixaService;
        private readonly IEmpresaService _empresaService;

        public TransferenciaService(ITransferenciaRepository transferenciaRepository,
            ICaixaService caixaService,
            IEmpresaService empresaService)
        {
            _transferenciaRepository = transferenciaRepository;
            _caixaService = caixaService;
            _empresaService = empresaService;
        }

        public IEnumerable<TransferenciaLista> ObterTransferencias()
        {
            return _transferenciaRepository.ObterTransferencias();
        }

        public async Task<Transferencia> ObterPorId(int id)
        {
            return await _transferenciaRepository.GetById(id);
        }

        public async void Salvar(Transferencia transferencia)
        {
            var empresa = _empresaService.ObterEmpresas().Result.FirstOrDefault();

            var lancamentoCaixaOrigem = await _caixaService.ObterPorId(transferencia.TransferenciaOrigem.IdCaixa) ?? new Caixa();
            lancamentoCaixaOrigem.DataCompetencia = transferencia.DataTransferencia;
            lancamentoCaixaOrigem.IdConta = transferencia.TransferenciaOrigem.IdConta;
            lancamentoCaixaOrigem.NomeConta = transferencia.TransferenciaOrigem.NomeConta;
            lancamentoCaixaOrigem.IdEmpresa = empresa.Id;
            lancamentoCaixaOrigem.Descricao = "Transferência entre contas";
            lancamentoCaixaOrigem.Debito = transferencia.Valor;

            _caixaService.Salvar(lancamentoCaixaOrigem);

            transferencia.TransferenciaOrigem.IdCaixa = lancamentoCaixaOrigem.Id;

            var lancamentoCaixaDestino = await _caixaService.ObterPorId(transferencia.TransferenciaDestino.IdCaixa) ?? new Caixa();
            lancamentoCaixaDestino.DataCompetencia = transferencia.DataTransferencia;
            lancamentoCaixaDestino.IdConta = transferencia.TransferenciaDestino.IdConta;
            lancamentoCaixaDestino.NomeConta = transferencia.TransferenciaDestino.NomeConta;
            lancamentoCaixaDestino.IdEmpresa = empresa.Id;
            lancamentoCaixaDestino.Descricao = "Transferência entre contas";
            lancamentoCaixaDestino.Credito = transferencia.Valor;

            _caixaService.Salvar(lancamentoCaixaDestino);

            transferencia.TransferenciaDestino.IdCaixa = lancamentoCaixaDestino.Id;

            SalvarTransferencia(transferencia);
        }

        private void SalvarTransferencia(Transferencia transferencia)
        {
            if (transferencia.Id == 0)
                _transferenciaRepository.Add(transferencia);
            else
                _transferenciaRepository.Update(transferencia);
        }

        public void Excluir(Transferencia transferencia)
        {
            _transferenciaRepository.Delete(transferencia);
        }
    }
}