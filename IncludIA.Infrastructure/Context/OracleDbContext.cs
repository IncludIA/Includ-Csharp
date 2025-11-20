using IncludIA.Domain.Entities; 
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Context
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext() {} 
        
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

        // Entidades Principais
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Recruiter> Recruiters { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<JobVaga> JobVagas { get; set; }
        
        // Entidades de Suporte / Currículo
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Voluntariado> Voluntariados { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<CandidateIdioma> CandidateIdiomas { get; set; }

        // Entidades de Relacionamento / Ação
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<SavedJob> SavedJobs { get; set; }
        public virtual DbSet<SavedCandidate> SavedCandidates { get; set; }
        public virtual DbSet<ProfileView> ProfileViews { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => new { m.CandidateId, m.JobVagaId })
                .IsUnique();

            modelBuilder.Entity<SavedJob>()
                .HasIndex(s => new { s.CandidateId, s.JobVagaId })
                .IsUnique();

            modelBuilder.Entity<SavedCandidate>()
                .HasIndex(s => new { s.RecruiterId, s.CandidateId })
                .IsUnique();

            modelBuilder.Entity<CandidateIdioma>()
                .HasIndex(c => new { c.CandidateId, c.IdiomaId })
                .IsUnique();

            modelBuilder.Entity<Skill>()
                .HasIndex(s => s.Nome)
                .IsUnique();

            modelBuilder.Entity<Empresa>()
                .HasIndex(e => e.Cnpj)
                .IsUnique();

            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Recruiter>()
                .HasIndex(r => r.Email)
                .IsUnique();
                
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Skills)
                .WithMany(s => s.Candidates)
                .UsingEntity(j => j.ToTable("candidate_skills"));

            modelBuilder.Entity<JobVaga>()
                .HasMany(v => v.SkillsDesejadas)
                .WithMany(s => s.Vagas)
                .UsingEntity(j => j.ToTable("vaga_skills"));
        }
    }
}