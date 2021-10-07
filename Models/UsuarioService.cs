using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar() {
            using(BibliotecaContext bc = new BibliotecaContext()) {
                return bc.Usuarios.ToList();
            } 
        }

        public Usuario Listar(int id) {

            using(BibliotecaContext bc = new BibliotecaContext()) {
                return bc.Usuarios.Find(id);
            } 
        }

        public void incluirUsuario(Usuario userNovo) {
            using(BibliotecaContext bc = new BibliotecaContext()) {
                
                bc.Usuarios.Add(userNovo);
                bc.SaveChanges();
            }
        }

        public void editarUsuario(Usuario userFound){

            using(BibliotecaContext bc = new BibliotecaContext()) {
                
                Usuario u = bc.Usuarios.Find(userFound.Id);
                u.Login = userFound.Login;
                u.Nome = userFound.Nome;
                u.Senha = userFound.Senha;
                u.Tipo = userFound.Tipo;
                bc.SaveChanges();
            }
        }

        public void excluirUsuario(int id) {
            using(BibliotecaContext bc = new BibliotecaContext()) {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }
    }
}

/*

dotnet ef migrations add CriaTabelaUser
dotnet ef database update
*/