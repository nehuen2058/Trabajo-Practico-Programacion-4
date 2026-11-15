using Compras.data.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compras.data.Db
{
    public class ComprasContext : DbContext
    {
        public ComprasContext(DbContextOptions<ComprasContext> options) : base(options) { }

        public DbSet<Producto> Productos => Set<Producto>();

    }
}
