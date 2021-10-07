using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;
        }

        public IActionResult ListarUsuarios() {
            return View();
        }

        public IActionResult ExcluirUsuario(int id){
            Usuario userFound = new UsuarioService().Listar(id);
            return View(userFound);
        }
        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao, int id){
            Usuario userFound = new UsuarioService().Listar(id);
            return View(userFound);
        }

        public IActionResult NeedAdmin() {
            Autenticacao.CheckLogin(this);
            return View();
        }

    }
}
