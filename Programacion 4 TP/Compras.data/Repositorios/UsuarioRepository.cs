using Compras.data.Db;
using Compras.data.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Compras.data.Repositorios.UsuarioRepository;


namespace Compras.data.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public interface IUsuarioRepository
        {
            Usuario ObtenerOCrearUsuario(string email, string nombre);
            List<String> ObtenerAcciones(string email);

            public Usuario ObtenerUsuarioPorId(int idUsuario);
            public Usuario ObtenerUsuarioPorEmail(string email);
        }

        private ComprasContext _db;

        public UsuarioRepository(ComprasContext context)
        {
            _db = context;
        }


        public List<String> ObtenerAcciones(string Email)
        {
            try
            {
                var usuario = _db.Usuarios.Include(u => u.UsuarioRoles)
                                           .ThenInclude(ur => ur.Rol)
                                           .ThenInclude(r => r.RolesAcciones)
                                           .ThenInclude(ra => ra.Accion)
                                           .FirstOrDefault(u => u.Email == Email);
                if (usuario == null)
                {
                    return new List<string>();
                }
                var acciones = usuario.UsuarioRoles
                                      .SelectMany(ur => ur.Rol.RolesAcciones)
                                      .Select(ra => ra.Accion.Descripcion)
                                      .Distinct()
                                      .ToList();
                return acciones;
            }
            catch
            {

                return new List<string>();
            }
        }
        public Usuario ObtenerOCrearUsuario(string email, string nombre)
        {
            try
            {
                var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == email);
                if (usuario != null)
                {
                    return usuario;
                }

                usuario = new Usuario { Email = email, Nombre = nombre, FechaAlta = DateTime.Now };
                _db.Usuarios.Add(usuario);
                _db.SaveChanges();

                return usuario;
            }
            catch
            {

                return null;
            }

        }
        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {

            Usuario Usuario = _db.Usuarios
                                .Where(x => x.IdUsuario == idUsuario)
                                .FirstOrDefault();

            return Usuario;
        }

        public Usuario ObtenerUsuarioPorEmail(string email)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == email);

            return usuario;

        }

    }
}
