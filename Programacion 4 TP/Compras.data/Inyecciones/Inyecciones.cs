using Compras.data.Db;
using Compras.data.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using static Compras.data.Repositorios.CarritoRepository;
using static Compras.data.Repositorios.UsuarioRepository;

namespace Compras.data.Inyecciones
{
    public static class DataAccessExtensions
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ComprasContext>(o =>
                o.UseSqlServer(connectionString));
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<ICarritoRepository, CarritoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }
    }
}
