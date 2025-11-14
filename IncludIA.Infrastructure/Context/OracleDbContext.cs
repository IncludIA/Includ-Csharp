using IncludIA.Domain.Entities; 
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Context
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext() {} 
        
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

        public virtual DbSet<Candidato> Candidatos { get; set; }
        public virtual DbSet<Candidatura> Candidaturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidatura>()
                .HasOne(c => c.Candidato)
                .WithMany(c => c.Candidaturas)
                .HasForeignKey(c => c.CandidatoId);
                
            base.OnModelCreating(modelBuilder);
        }
    }
}