using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeoAlpha.Models;

namespace SeoAlpha.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x => x.Senha)
                .HasColumnName("Senha")
                .HasColumnType("NVARCHAR")
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x => x.DataNascimento)
                .HasColumnName("DataNascimento")
                .HasColumnType("DATETIME");

            builder.Property(x => x.DataCriacao)
                .HasColumnName("DataCriacao")
                .HasColumnType("DATETIME");
        }
    }
}
