using Compras.data.Migrations;
using Compras.data.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Programacion_4_TP.Models;
using System.Security.Claims;
using static Compras.data.Repositorios.CarritoRepository;
using static Compras.data.Repositorios.UsuarioRepository;
using static Compras.data.Repositorios.ProductoRepository;

namespace Programacion_4_TP.Controllers
{
    public class CatalogoController : Controller
    {

        private IProductoRepository _ProductoRepository;
        private IUsuarioRepository _UsuarioRepository;

        private ICarritoRepository _CarritoRepository;

        public CatalogoController(IProductoRepository ProductoRepository, IUsuarioRepository UsuarioRepository, ICarritoRepository CarritoRepository)
        {
            _ProductoRepository = ProductoRepository;
            _UsuarioRepository = UsuarioRepository;
            _CarritoRepository = CarritoRepository;
        }

     
        public IActionResult Index()
        {
            List<ProductoVM> Productos = new List<ProductoVM>();
            var Productosdb = _ProductoRepository.ObtenerProductosDisponibles();
            Productos = Productosdb.Select(x => new ProductoVM { Id = x.Id, Nombre = x.Nombre, Precio = x.Precio, Cantidad = x.Cantidad }).ToList();
            return View(Productos);
        }

     
        [HttpPost]
        [Authorize]
        public IActionResult agregar(int idProducto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var ProductoDb = _ProductoRepository.ObtenerProductoPorId(idProducto);
            var UsuarioDb = _UsuarioRepository.ObtenerUsuarioPorEmail(email);
            if (ProductoDb == null || UsuarioDb == null)
            {
                return NotFound();
            }
            try
            {
                _CarritoRepository.AgregarAlCarrito(ProductoDb.Id, UsuarioDb.IdUsuario);
                _ProductoRepository.RestarCantidad(ProductoDb.Id);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Detalle(int id)
        {

            var ProductoDb = _ProductoRepository.ObtenerProductoPorId(id);
            if (ProductoDb == null)
            {
                return NotFound();
            }
            ProductoVM producto = new ProductoVM();
            producto.Nombre = ProductoDb.Nombre;
            producto.Precio = ProductoDb.Precio;
            producto.Cantidad = ProductoDb.Cantidad;
            producto.Id = ProductoDb.Id;
            return View(producto);
        }
    }
}
