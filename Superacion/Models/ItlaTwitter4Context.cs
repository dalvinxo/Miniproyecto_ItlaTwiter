using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Superacion.ViewModels;

namespace Superacion.Models
{
    public partial class ItlaTwitter4Context : DbContext
    {
        public ItlaTwitter4Context()
        {
        }

        public ItlaTwitter4Context(DbContextOptions<ItlaTwitter4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Amigo> Amigo { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Publicacion> Publicacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public DbSet<PublicacionViewModel> PublicacionViewModels { get; set; }
        public DbSet<ComentarioViewModels> comentarioViewModels { get; set; }
        public DbSet<PublicacionAmigoViewModels> publicacionAmigoViewModels { get; set; }
        public DbSet<AmigoViewModels> amigoViewModels{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=DESKTOP-DK0MCFF;database=ItlaTwitter4;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmigoViewModels>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
            });

            modelBuilder.Entity<PublicacionAmigoViewModels>(entity =>
            {
                entity.HasKey(e => e.IdPublicacion);
            });

            modelBuilder.Entity<ComentarioViewModels>(entity =>
            {
                entity.HasKey(e => e.IdComentario);
            });

            modelBuilder.Entity<PublicacionViewModel>(entity =>
            {
                entity.HasKey(e => e.IdPublicacion);
            });

            modelBuilder.Entity<Amigo>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuarioA, e.IdAmigoB });

                entity.Property(e => e.IdUsuarioA).HasColumnName("IdUsuario_A");

                entity.Property(e => e.IdAmigoB).HasColumnName("IdAmigo_B");

                entity.HasOne(d => d.IdAmigoBNavigation)
                    .WithMany(p => p.AmigoIdAmigoBNavigation)
                    .HasForeignKey(d => d.IdAmigoB)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Amigo__IdAmigo_B__3A81B327");

                entity.HasOne(d => d.IdUsuarioANavigation)
                    .WithMany(p => p.AmigoIdUsuarioANavigation)
                    .HasForeignKey(d => d.IdUsuarioA)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Amigo__IdUsuario__398D8EEE");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario);

                entity.Property(e => e.Comentario1)
                    .HasColumnName("Comentario")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdPublicacionNavigation)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.IdPublicacion)
                    .HasConstraintName("FK__Comentari__IdPub__4316F928");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Comentari__IdUsu__4222D4EF");
            });

            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.HasKey(e => e.IdPublicacion);

                entity.Property(e => e.Cuerpo)
                    .HasMaxLength(1500)
                    .IsUnicode(false);


                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Publicacion)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Publicaci__IdUsu__3E52440B");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Clave)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(17)
                    .IsUnicode(false);
                
                entity.Property(e => e.Usuario1)
                    .HasColumnName("Usuario")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }

        public DbSet<Superacion.ViewModels.RegistroUsuarioViewModel> RegistroUsuarioViewModel { get; set; }
    }
}
