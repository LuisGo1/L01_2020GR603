using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Identity.Client;
using System.Data;
using System.Threading.Tasks.Dataflow;

namespace L01_2020GR603.models
{
    public class infocontext : DbContext
    {
        public infocontext(DbContextOptions<infocontext> options): base(options)
        {
            
         }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<calificaciones> calificaciones { get; set; }
        public DbSet<comentarios> comentarios { get; set; }
     
        

    }
}
