using IncludIA.Domain.Entities; 
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Context
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext() {} 
        
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}