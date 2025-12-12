using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<CategoriaProducto> CategoriasProducto => Set<CategoriaProducto>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Mascota> Mascotas => Set<Mascota>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<DetalleVenta> DetallesVenta => Set<DetalleVenta>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(e => e.IdCliente);
            });

            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.ToTable("CategoriasProducto");
                entity.HasKey(e => e.IdCategoria);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(e => e.IdProducto);

                entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");

                entity.HasOne<CategoriaProducto>()
                      .WithMany()
                      .HasForeignKey(e => e.IdCategoria);
            });

            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.ToTable("Mascotas");
                entity.HasKey(e => e.IdMascota);

                entity.HasOne<Cliente>()
                      .WithMany()
                      .HasForeignKey(e => e.IdCliente);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Ventas");
                entity.HasKey(e => e.IdVenta);

                entity.Property(e => e.Fecha).HasColumnType("datetime");
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");

                entity.HasOne<Cliente>()
                      .WithMany()
                      .HasForeignKey(e => e.IdCliente);
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.ToTable("DetalleVenta");
                entity.HasKey(e => e.IdDetalle);

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Subtotal)
                      .HasColumnType("decimal(10,2)")
                      .ValueGeneratedOnAddOrUpdate(); 

                entity.HasOne<Venta>()
                      .WithMany()
                      .HasForeignKey(e => e.IdVenta);

                entity.HasOne<Producto>()
                      .WithMany()
                      .HasForeignKey(e => e.IdProducto);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
