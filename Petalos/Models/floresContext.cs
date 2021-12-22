using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Petalos.Models
{
    public partial class floresContext : DbContext
    {
        public floresContext()
        {
        }

        public floresContext(DbContextOptions<floresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Datosflore> Datosflores { get; set; }
        public virtual DbSet<Imagenesflore> Imagenesflores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=localhost;user=root;password=manzana123;database=flores", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<Datosflore>(entity =>
            {
                entity.HasKey(e => e.Idflor)
                    .HasName("PRIMARY");

                entity.ToTable("datosflores");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Idflor).HasColumnName("idflor");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.Nombrecientifico)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombrecientifico");

                entity.Property(e => e.Nombrecomun)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("nombrecomun");

                entity.Property(e => e.Origen)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("origen");
            });

            modelBuilder.Entity<Imagenesflore>(entity =>
            {
                entity.HasKey(e => e.Idimagen)
                    .HasName("PRIMARY");

                entity.ToTable("imagenesflores");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.Idflor, "FK_imagenesflores_1");

                entity.Property(e => e.Idimagen).HasColumnName("idimagen");

                entity.Property(e => e.Idflor).HasColumnName("idflor");

                entity.Property(e => e.Nombreimagen)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombreimagen");

                entity.HasOne(d => d.IdflorNavigation)
                    .WithMany(p => p.Imagenesflores)
                    .HasForeignKey(d => d.Idflor)
                    .HasConstraintName("FK_imagenesflores_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
