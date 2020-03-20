using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.ControleAcesso;
using Gomi.Infraestrutura.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gomi.WebApp.Controllers
{
    public class ControleAcessoController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public ControleAcessoController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public ActionResult Login()
        {
            try
            {
                _usuarioService.AdicionarUsuarioSuporte();  
                var usuario = _usuarioService.ObterUsuarioLogado();
                if (string.IsNullOrEmpty(usuario?.NomeAcesso)) return View();
                return RedirectToAction("Principal", "CadastrosConfiguracoes");
            }
            catch (Exception ex)
            {
                LogService.RegistrarExcecao(ex);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Login(UsuarioAutenticacao usuario)
        {
            try
            {
                _usuarioService.Autenticar(usuario.IdUsuario, usuario.Senha);
                return RedirectToAction("Principal", "CadastrosConfiguracoes");
            }
            catch (Exception ex)
            {
                LogService.RegistrarExcecao(ex);
                ViewBag.ErrorMessage = "Usuário ou senha inválido.";
                return View(usuario);
            }
        }

        public ActionResult LogOut()
        {
            try
            {
                _usuarioService.Sair();
                return RedirectToAction("Login", "ControleAcesso");
            }
            catch (Exception e)
            {
                LogService.RegistrarExcecao(e);
                throw;
            }
        }

        public ActionResult UsuarioLogado()
        {
            try
            {
                var usuario = _usuarioService.ObterUsuarioLogado();
                return usuario != null ? View("UsuarioLogado", new UsuarioAutenticacao { IdUsuario = usuario.NomeAcesso }) : null;
            }
            catch (Exception ex)
            {
                LogService.RegistrarExcecao(ex);
                throw;
            }
        }
    }
}