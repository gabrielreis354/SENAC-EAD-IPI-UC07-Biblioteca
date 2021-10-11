using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using System.Linq;
using System.Collections.Generic;
namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static void verificaseUsuarioAdminExiste(BibliotecaContext bc){
            IQueryable<Usuario> userFound = bc.Usuarios.Where(u => u.Login == "admin");

                if (userFound.ToList().Count == 0)
                {
                    Usuario admin = new Usuario();
                    admin.Login = "Admin";
                    admin.Senha = Criptografo.TextoCriptografado("123");
                    admin.Tipo = Usuario.Admin;
                    admin.Nome = "Administrador";

                    bc.Usuarios.Add(admin);
                    bc.SaveChanges();
                }
        }

        public static bool verificaLoginSenha(string Login, string Senha, Controller controller) {
            using (BibliotecaContext bc = new BibliotecaContext()) {
                
                verificaseUsuarioAdminExiste(bc);

                Senha = Criptografo.TextoCriptografado(Senha);

                IQueryable<Usuario> userFound = bc.Usuarios.Where(u => u.Login == Login && u.Senha == Senha);
                List<Usuario>ListaUserFound = userFound.ToList();

                if (ListaUserFound.Count == 0)
                {
                    return false;
                }
                else
                {
                    controller.HttpContext.Session.SetString("Login", ListaUserFound[0].Login);
                    controller.HttpContext.Session.SetString("Nome", ListaUserFound[0].Nome);
                    controller.HttpContext.Session.SetString("Senha", ListaUserFound[0].Senha);
                    controller.HttpContext.Session.SetInt32("Tipo", ListaUserFound[0].Tipo);
                    return true;
                }
            }

        }

        public static void verificaseUsuarioEAdmin(Controller controller) {
            if(!(controller.HttpContext.Session.GetInt32("Tipo") == Usuario.Admin)) {

                controller.Request.HttpContext.Response.Redirect("/Usuarios/NeedAmin");
                
            }
        }
    }
}