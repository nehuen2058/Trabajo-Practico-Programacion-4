using Compras.data.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Compras.data.Modelos.Identidad;


namespace Compras.data.Modelos
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(200)]
        public string Nombre { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();


    }
}
