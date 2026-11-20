using Compras.data.Db;
using Compras.data.Modelos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compras.data.Repositorios
{
    public interface IProductoRepository
    {
        void ActualizarProducto(Producto Producto);
        bool AgregarProducto(Producto Producto);
        bool BorrarProducto(int id);
        Producto ObtenerProductoPorId(int id);
        List<Producto> ObtenerProductos();
        void RestarCantidad(int id);
        List<Producto> ObtenerProductosDisponibles();
    }
    public class ProductoRepository : IProductoRepository

    {
        private ComprasContext _db;

        public ProductoRepository(ComprasContext context) 
        { 
            _db = context; 
        }

        public bool AgregarProducto(Producto evento)
        {
            try
            {
                _db.Productos.Add(evento);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Producto> ObtenerProductos()
        {
            return _db.Productos.Where(x => !x.Borrado).ToList();
        }
        public Producto ObtenerProductoPorId(int idProducto)
        {

            Producto Producto = _db.Productos
                                .Where(x => x.Id == idProducto)
                                .FirstOrDefault();

            return Producto;
        }
        public void ActualizarProducto(Producto Producto)
        {
            _db.Productos.Update(Producto);
            _db.SaveChanges();
        }
        public bool BorrarProducto(int IdProducto)
        {
            try
            {
                Producto Producto = _db.Productos.Find(IdProducto);
                if (Producto != null)
                {
                    _db.Productos.Remove(Producto);
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }
        public void RestarCantidad(int id)
        {
            var item = ObtenerProductoPorId(id);

            item.Cantidad -= 1;
            _db.SaveChanges();
            return;
        }
        public List<Producto> ObtenerProductosDisponibles()
        {
            return _db.Productos.Where(x => !x.Borrado && x.Cantidad > 0).ToList();
        }
    }
}
