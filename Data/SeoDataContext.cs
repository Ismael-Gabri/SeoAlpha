using Microsoft.EntityFrameworkCore;
using SeoAlpha.Data.Mappings;
using SeoAlpha.Models;

namespace SeoAlpha.Data
{
    public class SeoDataContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=SEO;User ID=sa;Password=1q2w3e4r@#$");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsuarioMapping());
            builder.ApplyConfiguration(new CursoMapping());
            builder.ApplyConfiguration(new CargoMapping());
        }
    }
}
