using Microsoft.EntityFrameworkCore;
using TorneioJJ_Usuarios.Models;

namespace TorneioJJ_Usuarios.Context
{
    public class MyDbContext : DbContext
    {
        private IConfiguration Configuration;
        public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        public DbSet<Usuario> Usuarios { get; set; }
        
    }
}
