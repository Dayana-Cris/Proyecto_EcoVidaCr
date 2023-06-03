using Microsoft.EntityFrameworkCore;

namespace EcoVidaCR.Data
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        //Se construye el contexto para el objeto Autor

        public DbSet<Models.Usuarios> Usuarios { get; set; }
        public DbSet<Models.Voluntariados> Voluntariados { get; set; }

        public DbSet<Models.Destinos> Destinos { get; set; }

    }//Cierre class
}
