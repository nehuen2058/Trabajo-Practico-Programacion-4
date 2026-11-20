namespace Programacion_4_TP.Models
{
    public class CarritoVM
    {
        public int IdCarrito { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public int Cantidad { get; set; } = 1;
        public decimal Subtotal => Precio * Cantidad;
    }
}
