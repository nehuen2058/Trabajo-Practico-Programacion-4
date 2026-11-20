using Compras.data.Modelos;
using Compras.data.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Programacion_4_TP.Models;
using System.Security.Claims;
using static Compras.data.Repositorios.CarritoRepository;
using static Compras.data.Repositorios.UsuarioRepository;

namespace Programacion_4_TP.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoRepository _carritoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        
        private readonly IProductoRepository _productoRepository;

        public CarritoController(ICarritoRepository carritoRepository, IUsuarioRepository usuarioRepository, IProductoRepository ProductoRepository)
        {
            _carritoRepository = carritoRepository;
            _usuarioRepository = usuarioRepository;
            _productoRepository = ProductoRepository;
        }


        private Usuario ObtenerUsuarioPorEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Usuario  = _usuarioRepository.ObtenerUsuarioPorEmail(email);
            return Usuario ;
        }

        public IActionResult Index()
        {
            var Usuario = ObtenerUsuarioPorEmail();
            if (Usuario == null) return NotFound();

            var items = _carritoRepository.ObtenerCarrito(Usuario.IdUsuario)
                .Select(c => new CarritoVM
                {
                    IdCarrito = c.IdCarrito,
                    Nombre = c.Producto.Nombre,
                    Precio = c.Producto.Precio,
                    Cantidad = c.Cantidad
                }).ToList();

            return View(items);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Quitar(int idCarrito)
        {
            var Usuario = ObtenerUsuarioPorEmail();
            if (Usuario == null) return NotFound();

            _carritoRepository.QuitarDelCarrito(Usuario.IdUsuario, idCarrito);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Comprar(int idCarrito)
        {
            var Usuario = ObtenerUsuarioPorEmail();
            if (Usuario == null) return NotFound();

            _carritoRepository.Comprar(Usuario.IdUsuario, idCarrito);
            return RedirectToAction(nameof(Index));
        }


    }
}
