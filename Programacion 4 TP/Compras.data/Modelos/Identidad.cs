using Compras.data.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Compras.data.Modelos
{
    public class Identidad
    {
        public class UsuarioRol
        {
            [Key]
            public int IdUsuarioRol { get; set; }
            public int IdUsuario { get; set; }
            [ForeignKey(nameof(IdUsuario))]
            public Usuario Usuario { get; set; }
            public int IdRol { get; set; }
            [ForeignKey(nameof(IdRol))]
            public Rol Rol { get; set; }
        }

        public class Rol
        {
            [Key]
            public int IdRol { get; set; }
            public string Descripcion { get; set; }
            public List<RolAccion> RolesAcciones { get; set; } = new List<RolAccion>();


        }



        public class RolAccion
        {
            [Key]
            public int IdRolAccion { get; set; }
            public int IdRol { get; set; }
            [ForeignKey(nameof(IdRol))]
            public Rol Rol { get; set; }
            public int IdAccion { get; set; }
            [ForeignKey(nameof(IdAccion))]
            public Accion Accion { get; set; }
        }

        public class Accion
        {
            [Key]
            public int IdAccion { get; set; }
            public string Descripcion { get; set; }
        }
    }
}
