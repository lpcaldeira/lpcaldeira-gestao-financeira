using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Principal()
        {
            return View();
        }

        //public ActionResult Lista()
        //{
        //    var repositorio = new UsuarioRepository(SessionUnitOfWork);
        //    IEnumerable<Usuario> usuarios = repositorio.GetAll()?.ToList() ?? new List<Usuario>();
        //    return PartialView("Lista", usuarios);
        //}

        //public ActionResult CriarUsuario()
        //{
        //    return View("Ficha", new Usuario());
        //}

        //[HttpPost]
        //public ActionResult CriarUsuario(Usuario usuario)
        //{
        //    try
        //    {
        //        usuario.Validar();
        //        usuario.DefinirSenha();
        //        var repositorio = new UsuarioRepository(SessionUnitOfWork);
        //        repositorio.Create(usuario);
        //        return Redirect("/Usuario/Principal");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessage = ex.Message;
        //        LogService.RegistrarExcecao(ex);
        //        return View("Ficha", usuario);
        //    }
        //}

        //public ActionResult EditarUsuario(int id)
        //{
        //    var repositorio = new UsuarioRepository(SessionUnitOfWork);
        //    var usuario = repositorio.GetById(id);
        //    return View("Ficha", usuario);
        //}

        //[HttpPost]
        //public ActionResult EditarUsuario(Usuario usuario)
        //{
        //    try
        //    {
        //        usuario.Validar();
        //        var repositorio = new UsuarioRepository(SessionUnitOfWork);
        //        var usuarioBase = repositorio.GetById(usuario.Id);
        //        usuarioBase.Atualizar(usuario.NomeUsuario, usuario.AcessoAlternativo, usuario.NovaSenha, usuario.ConfirmaSenha);
        //        usuarioBase.DefinirSenha();
        //        repositorio.Update(usuarioBase);
        //        return Redirect("/Usuario/Principal");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessage = ex.Message;
        //        LogService.RegistrarExcecao(ex);
        //        return View("Ficha", usuario);
        //    }
        //}

        //public ActionResult ExcluirUsuario(int id)
        //{
        //    var repositorio = new UsuarioRepository(SessionUnitOfWork);
        //    repositorio.Delete(id);
        //    return Redirect("/Usuario/Principal");
        //}
    }
}