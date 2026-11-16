using Compras.data.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using static Compras.data.Modelos.Identidad;


namespace Compras.data.Db
{
    public class ComprasContext : DbContext
    {
        public ComprasContext(DbContextOptions<ComprasContext> options) : base(options) { }

        public DbSet<Producto> Productos => Set<Producto>();

        public DbSet<Usuario> Usuarios => Set<Usuario>();

        public DbSet<UsuarioRol> UsuarioRoles => Set<UsuarioRol>();

        public DbSet<Rol> Roles => Set<Rol>();

        public DbSet<RolAccion> RolesAcciones => Set<RolAccion>();

        public DbSet<Accion> Acciones => Set<Accion>();

        public DbSet<Carrito> Carritos => Set<Carrito>();
    }
}
