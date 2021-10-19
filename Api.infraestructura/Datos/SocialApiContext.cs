using Api.Core.Entidades;
using Api.infraestructura.Datos.Configuracions;
using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Api.infraestructura.Datos
{
    public partial class SocialApiContext : DbContext
    {
        public SocialApiContext()
        {
        }

        public SocialApiContext(DbContextOptions<SocialApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Publicacion> Publicacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComentariosConfiguracion());
            modelBuilder.ApplyConfiguration(new PostConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
        }
    }

}
