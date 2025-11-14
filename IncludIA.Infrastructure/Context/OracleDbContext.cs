using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Context
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aqui podes configurar o mapeamento das tuas tabelas Oracle
            base.OnModelCreating(modelBuilder);
        }
    }
}