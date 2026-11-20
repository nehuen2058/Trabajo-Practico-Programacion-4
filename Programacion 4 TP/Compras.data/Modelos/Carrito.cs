using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Compras.data.Modelos
{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; } 

        public int IdProducto { get; set; }
        [ForeignKey(nameof(IdProducto))]
        public Producto Producto { get; set; }

        public int Cantidad { get; set; } = 1;

    }
}
