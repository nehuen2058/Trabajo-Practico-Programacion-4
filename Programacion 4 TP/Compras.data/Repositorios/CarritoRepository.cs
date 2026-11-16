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
        public void AgregarAlCarrito(int idUsuario, int idProducto)
        {
            var item = _db.Carritos
                .FirstOrDefault(c => c.IdUsuario == idUsuario && c.IdProducto == idProducto);

            if (item == null)
                _db.Carritos.Add(new Carrito
                {
                    IdUsuario = idUsuario,
                    IdProducto = idProducto
                });

            _db.SaveChanges();
        }
        public void QuitarDelCarrito(int idUsuario, int idCarrito)
        {
            var item = _db.Carritos
                .FirstOrDefault(c => c.IdCarrito == idCarrito && c.IdUsuario == idUsuario);
            if (item == null) return;

            _db.Carritos.Remove(item);
            _db.SaveChanges();
        }
    }
}
