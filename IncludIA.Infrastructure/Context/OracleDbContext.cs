using IncludIA.Domain.Entities; // <--- NOVO
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Context
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }

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