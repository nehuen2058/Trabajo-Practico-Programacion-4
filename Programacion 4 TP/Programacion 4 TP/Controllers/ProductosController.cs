
using Compras.data.Db;
using Compras.data.Modelos;
using Compras.data.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Programacion_4_TP.Models;
using System.Diagnostics;
public class ProductosController : Controller
{
    private readonly ComprasContext _context;
    private IProductoRepository _ProductoRepository;

    public ProductosController(IProductoRepository ProductoRepository)
    {
        _ProductoRepository = ProductoRepository;
    }

    // GET: PRODUCTOVMS
    public ActionResult Index()
    {

        List<ProductoVM> Productos = new List<ProductoVM>();
        var Productosdb = _ProductoRepository.ObtenerProductos();

        Productos = Productosdb.Select(x => new ProductoVM { Id = x.Id, Nombre = x.Nombre, Precio = x.Precio, Cantidad = x.Cantidad }).ToList();
        return View(Productos);
    }

    // GET: PRODUCTOVMS/Details/5
    public ActionResult Details(int id)
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


    // POST: PRODUCTOVMS/Create
    public ActionResult Create(ProductoVM ProductoNuevo)
    {
        if (!ModelState.IsValid)
        {

            ViewBag.Mensaje = "Error";
            return View(ProductoNuevo);
        }
        try
        {
            Producto Producto = new Producto();

            Producto.Nombre = ProductoNuevo.Nombre;
            Producto.Precio = ProductoNuevo.Precio;
            Producto.Cantidad = ProductoNuevo.Cantidad;

            _ProductoRepository.AgregarProducto(Producto);
            TempData["Mensaje"] = "Evento creado Exitoso";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: PRODUCTOVMS/Edit/5
    public ActionResult Edit(int id)
    {
        ProductoVM elemento = null;

        var ProductoDb = _ProductoRepository.ObtenerProductoPorId(id);
        if (ProductoDb == null)
        {
            return NotFound();
        }
        elemento = new ProductoVM();
        elemento.Nombre = ProductoDb.Nombre;
        elemento.Precio = ProductoDb.Precio;
        elemento.Cantidad = ProductoDb.Cantidad;

        return View(elemento);
    }

    // POST: PRODUCTOVMS/Edit/5

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, ProductoVM ProductoModificado)
    {
        try
        {
            Producto ProductoAModificar = new Producto();
            ProductoAModificar.Nombre = ProductoModificado.Nombre;
            ProductoAModificar.Precio = ProductoModificado.Precio;
            ProductoAModificar.Cantidad = ProductoModificado.Cantidad;
            ProductoAModificar.Id = ProductoModificado.Id;
            _ProductoRepository.ActualizarProducto(ProductoAModificar);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: PRODUCTOVMS/Delete/5
    public ActionResult Delete(int id)
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

    // POST: PRODUCTOVMS/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(ProductoVM ProductoABorrar)
    {
        try
        {
            var producto = _ProductoRepository.ObtenerProductoPorId(ProductoABorrar.Id);
            producto.Borrado = true;
            _ProductoRepository.ActualizarProducto(producto);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
