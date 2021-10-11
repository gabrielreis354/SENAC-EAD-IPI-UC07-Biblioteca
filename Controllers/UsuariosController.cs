using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;


namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;
        }

        public IActionResult ListaDeUsuarios() 
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaseUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }

        public IActionResult editarUsuario(int id) 
        {
            Usuario u = new UsuarioService().Listar(id);
            return View(id);
        }
        [HttpPost]
        public IActionResult editarUsuario(Usuario userModified) 
        {
            UsuarioService us = new UsuarioService();
            us.editarUsuario(userModified);
            return RedirectToAction("ListaDeUsuarios");
        }

        public IActionResult registrarUsuario() 
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaseUsuarioEAdmin(this);
            return View();
        }
        [HttpPost]
        public IActionResult registrarUsuario(Usuario newUser) 
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaseUsuarioEAdmin(this);

            newUser.Senha = Criptografo.TextoCriptografado(newUser.Senha);

            UsuarioService us = new UsuarioService();
            us.incluirUsuario(newUser);

            return RedirectToAction("cadastroRealizado");
        }

        public IActionResult ExcluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }
        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            if (decisao=="Excluir")
            {   
                ViewData["Mensagem"] = "Exclusão de Usuario" + new UsuarioService().Listar(id).Nome + "realizada com sucesso";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"] = "Exclusão Cancelada";
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
        }

        public IActionResult cadastroRealizado() 
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaseUsuarioEAdmin(this);
            return View();
        }

        public IActionResult NeedAdmin() 
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Sair() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
