using Compras.data.Db;
using Compras.data.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Compras.data.Repositorios.CarritoRepository;

namespace Compras.data.Repositorios
{
    public class CarritoRepository : ICarritoRepository
    {
        public interface ICarritoRepository
        {
            List<Carrito> ObtenerCarrito(int idUsuario);
            void AgregarAlCarrito(int idUsuario, int idProducto);

            void QuitarDelCarrito(int idUsuario, int IdCarrito);
            public void Comprar(int idUsuario, int idCarrito);

        }

        private ComprasContext _db;

        public CarritoRepository(ComprasContext context)
        {
            _db = context;
        }
        public List<Carrito> ObtenerCarrito(int idUsuario)
        {
            return _db.Carritos
                .Include(c => c.Producto)
                .Where(c => c.IdUsuario == idUsuario)
                .ToList();
        }

        public void AgregarAlCarrito(int idProducto, int idUsuario)
        {
            if (idUsuario == 0 || idProducto == 0) return;

            var item = _db.Carritos
                       .FirstOrDefault(c => c.IdUsuario == idUsuario && c.IdProducto == idProducto);

            var producto = _db.Productos.FirstOrDefault(p => p.Id == idProducto && !p.Borrado);
            if (producto == null || producto.Cantidad < 1) {
                throw new InvalidOperationException("No hay stock disponible.");
                }
            if (item == null)
            {
                _db.Carritos.Add(new Carrito
                {
                    IdUsuario = idUsuario,
                    IdProducto = idProducto,
                    Cantidad = 1

                });
            }
            else
            {
                item.Cantidad += 1;
            }
            _db.SaveChanges();
        }

        public void QuitarDelCarrito(int idUsuario, int idCarrito)
        {
            var item = _db.Carritos
                        .FirstOrDefault(c => c.IdUsuario == idUsuario && c.IdCarrito == idCarrito);
            if (item == null) return;

            var producto = _db.Productos.FirstOrDefault(p => p.Id == item.IdProducto);
            if (producto != null)
                producto.Cantidad += item.Cantidad;

            _db.Carritos.Remove(item);
            _db.SaveChanges();
        }
        public void Comprar(int idUsuario, int idCarrito)
        {
            var item = _db.Carritos
                        .FirstOrDefault(c => c.IdUsuario == idUsuario && c.IdCarrito == idCarrito);
            if (item == null) return;

            _db.Carritos.Remove(item);
            _db.SaveChanges();
        }

    }
}
