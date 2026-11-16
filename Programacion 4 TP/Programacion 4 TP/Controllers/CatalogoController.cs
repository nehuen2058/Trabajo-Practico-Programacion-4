using Compras.data.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Programacion_4_TP.Models;

namespace Programacion_4_TP.Controllers
{
    public class CatalogoController : Controller
    {

        private IProductoRepository _ProductoRepository;

        public CatalogoController(IProductoRepository ProductoRepository)
        {
            _ProductoRepository = ProductoRepository;
        }

        public IActionResult Index()
        {
             List<ProductoVM> Productos = new List<ProductoVM>();
        var Productosdb = _ProductoRepository.ObtenerProductos();
        Productos = Productosdb.Select(x => new ProductoVM {Nombre = x.Nombre, Precio = x.Precio, Cantidad = x.Cantidad }).ToList();
        return View(Productos);
        }
    }
}
