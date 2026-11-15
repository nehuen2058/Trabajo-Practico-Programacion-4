using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Compras.data.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public String Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        [Column(TypeName = "bit")]
        public bool Borrado { get; set; }

    }
}
