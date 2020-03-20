using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.ControleAcesso;
using Gomi.Infraestrutura.Models.Financeiro;
using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using DevExtreme.AspNet.Mvc;
using Gomi.Infraestrutura.Enums;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gomi.WebApp.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly IPessoaService _pessoaService;
        private readonly IEmpresaService _empresaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICentroCustoService _centroCustoService;
        private readonly IPlanoContaService _PlanoContaService;
        private readonly IContaService _contaService;
        private readonly ICaixaRepository _caixaRepository;
        private readonly IReceberRepository _receberRepository;
        private readonly IPagarRepository _pagarRepository;

        public RelatoriosController(IPessoaService pessoaService,
            IEmpresaService empresaService,
            IUsuarioService usuarioService,
            ICentroCustoService centroCustoService,
            IPlanoContaService PlanoContaService,
            IContaService contaService,
            ICaixaRepository caixaRepository,
            IReceberRepository receberRepository,
            IPagarRepository pagarRepository
            )
        {
            _pessoaService = pessoaService;
            _empresaService = empresaService;
            _usuarioService = usuarioService;
            _centroCustoService = centroCustoService;
            _PlanoContaService = PlanoContaService;
            _contaService = contaService;
            _caixaRepository = caixaRepository;
            _receberRepository = receberRepository;
            _pagarRepository = pagarRepository;

        }

        public IActionResult Principal()
        {
            return View();
        }

       

    }
}