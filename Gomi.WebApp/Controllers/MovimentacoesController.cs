using System.Collections.Generic;
using System.Linq;
using DevExtreme.AspNet.Mvc;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gomi.WebApp.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly IPagarService _pagarService;
        private readonly IReceberService _receberService;
        private readonly IEmpresaService _empresaService;
        private readonly IContaService _contaService;
        private readonly IPessoaService _pessoaService;
        private readonly IPlanoContaService _planoContaService;
        private readonly ICentroCustoService _centroCustoService;
        private readonly ITransferenciaService _transferenciaService;

        public MovimentacoesController(IPagarService pagarService,
            IReceberService receberService,
            IEmpresaService empresaService,
            IContaService contaService,
            IPessoaService pessoaService,
            IPlanoContaService planoContaService,
            ICentroCustoService centroCustoService,
            ITransferenciaService transferenciaService)
        {
            _pagarService = pagarService;
            _receberService = receberService;
            _empresaService = empresaService;
            _contaService = contaService;
            _pessoaService = pessoaService;
            _planoContaService = planoContaService;
            _centroCustoService = centroCustoService;
            _transferenciaService = transferenciaService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarPagamento(Pagar pagar)
        {
            if (pagar == null) return BadRequest();
            _pagarService.Salvar(pagar);
            return ViewComponent("MovimentacaoPagamentoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirPagamento()
        {
            var empresa = _empresaService.ObterEmpresas().Result.FirstOrDefault();
            var planosContas = _planoContaService.ObterPlanosContasSaida().Result;
            var centrosCustos = _centroCustoService.ObterCentrosCustos().Result;
            var ultimoRegistro = _pagarService.ObterPagamentos().Result.LastOrDefault();

            ViewData["Pagamento"] = new Pagar()
                .SetarIdEmpresa(empresa?.Id ?? 0)
                .SetarPlanosContas(planosContas)
                .SetarCentrosCustos(centrosCustos)
                .SetarIdPlanoConta(ultimoRegistro?.IdPlanoConta)
                .SetarDescricaoPlanoConta(ultimoRegistro?.DescricaoPlanoConta)
                .SetarDescricao(ultimoRegistro?.Descricao);

            return ViewComponent("MovimentacaoPagamentoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarPagamento(Pagar pagar)
        {
            var registroPagar = _pagarService.ObterPorId(pagar.Id);
            var planosContas = _planoContaService.ObterPlanosContasSaida().Result;
            var centrosCustos = _centroCustoService.ObterCentrosCustos().Result;

            ViewData["Pagamento"] = registroPagar
                .SetarPlanosContas(planosContas)
                .SetarCentrosCustos(centrosCustos);

            return ViewComponent("MovimentacaoPagamentoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirPagamento(int id)
        {
            var registroPagar = _pagarService.ObterPorId(id);
            _pagarService.Excluir(registroPagar);
            return ViewComponent("MovimentacaoPagamentoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpGet]
        public ActionResult GetPessoasPagamento(DataSourceLoadOptions loadOptions)
        {
            var filtro = loadOptions.Filter;
            var informacaoPesquisa = string.Empty;
            if (filtro != null)
            {
                informacaoPesquisa = filtro.Count == 1
                    ? ((JArray)filtro[0])?.Last?.Value<string>() ?? string.Empty
                    : ((List<object>)filtro).LastOrDefault() as string ?? string.Empty;
            }
            var pessoas = _pessoaService.ObterPessoasParaPagamentoPorNome(informacaoPesquisa.ToLowerInvariant()).Result;

            return Content(JsonConvert.SerializeObject(pessoas.OrderBy(x => x.Nome).ToList()), "application/json");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarRecebimento(Receber receber)
        {
            if (receber == null) return BadRequest();
            _receberService.Salvar(receber);
            return ViewComponent("MovimentacaoRecebimentoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirRecebimento()
        {
            var empresa = _empresaService.ObterEmpresas().Result.FirstOrDefault();
            var planosContas = _planoContaService.ObterPlanosContasEntrada().Result;
            var centrosCustos = _centroCustoService.ObterCentrosCustos().Result;
            var ultimoRegistro = _receberService.ObterRecebimentos().Result?.LastOrDefault();

            ViewData["Recebimento"] = new Receber()
                .SetarIdEmpresa(empresa?.Id ?? 0)
                .SetarPlanosContas(planosContas)
                .SetarCentrosCustos(centrosCustos)
                .SetarIdPlanoConta(ultimoRegistro?.IdPlanoConta)
                .SetarDescricaoPlanoConta(ultimoRegistro?.DescricaoPlanoConta)
                .SetarDescricao(ultimoRegistro?.Descricao);

            return ViewComponent("MovimentacaoRecebimentoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarRecebimento(Receber receber)
        {
            var registroReceber = _receberService.ObterPorId(receber.Id);
            var planosContas = _planoContaService.ObterPlanosContasEntrada().Result;
            var centrosCustos = _centroCustoService.ObterCentrosCustos().Result;

            ViewData["Recebimento"] = registroReceber
                .SetarPlanosContas(planosContas)
                .SetarCentrosCustos(centrosCustos);

            return ViewComponent("MovimentacaoRecebimentoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirRecebimento(int id)
        {
            var registroReceber = _receberService.ObterPorId(id);
            _receberService.Excluir(registroReceber);
            return Ok();
        }

        [HttpGet]
        public ActionResult GetPessoasRecebimento(DataSourceLoadOptions loadOptions)
        {
            var filtro = loadOptions.Filter;
            var informacaoPesquisa = string.Empty;
            if (filtro != null)
            {
                informacaoPesquisa = filtro.Count == 1
                    ? ((JArray)filtro[0])?.Last?.Value<string>() ?? string.Empty
                    : ((List<object>)filtro).LastOrDefault() as string ?? string.Empty;
            }
            var pessoas = _pessoaService.ObterPessoasParaRecebimentoPorNome(informacaoPesquisa.ToLowerInvariant()).Result;

            return Content(JsonConvert.SerializeObject(pessoas.OrderBy(x => x.Nome).ToList()), "application/json");
        }

        [HttpGet]
        public ActionResult GetContas()
        {
            var contas = _contaService.ObterContas().Result;
            return Content(JsonConvert.SerializeObject(contas.OrderBy(x => x.Nome).ToList()), "application/json");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarTransferencia(Transferencia transferencia)
        {
            if (transferencia == null) return BadRequest();
            _transferenciaService.Salvar(transferencia);
            return Ok();
        }

        [HttpPost]
        public ActionResult InserirTransferencia()
        {
            var contas = _contaService.ObterContas().Result;
            ViewData["Transferencia"] = new Transferencia().SetarContas(contas);
            return ViewComponent("MovimentacaoTransferenciaFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarTransferencia(TransferenciaLista transferenciaLista)
        {
            var transferencia = _transferenciaService.ObterPorId(transferenciaLista.Id).Result;
            var contas = _contaService.ObterContas().Result;
            ViewData["Transferencia"] = transferencia.SetarContas(contas);
            return ViewComponent("MovimentacaoTransferenciaFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirTransferencia(Transferencia transferencia)
        {
            _transferenciaService.Excluir(transferencia);
            return Ok();
        }
    }
}