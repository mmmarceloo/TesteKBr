using Microsoft.EntityFrameworkCore;
using TorneioJJ_Campeonatos.Models;

namespace TorneioJJ_Campeonatos.Context
{
    public class CampeonatoDbContext : DbContext
    {
        private IConfiguration Configuration;
        public CampeonatoDbContext(DbContextOptions<CampeonatoDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Campeonato> Campeonatos { get; set; }
    }
}
