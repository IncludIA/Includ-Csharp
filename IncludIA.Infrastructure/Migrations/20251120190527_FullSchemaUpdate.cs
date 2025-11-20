using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncludIA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FullSchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CANDIDATURA");

            migrationBuilder.DropTable(
                name: "TB_CANDIDATO");

            migrationBuilder.CreateTable(
                name: "candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    SenhaHash = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ResumoPerfil = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false),
                    ResumoInclusivoIA = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false),
                    FotoPerfilUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IsAtive = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NomeOficial = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NomeFantasia = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cnpj = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: false),
                    Cultura = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: false),
                    FotoCapaUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "idiomas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idiomas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    TipoSkill = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "educations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NomeInstituicao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Grau = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AreaEstudo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Descricao = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_educations_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "voluntariados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Organizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Funcao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voluntariados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_voluntariados_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TituloCargo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoEmprego = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_experiences_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_experiences_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "recruiters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    SenhaHash = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IsAtive = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    FotoPerfilUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recruiters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recruiters_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidate_idiomas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NivelProficiencia = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    IdiomaId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidate_idiomas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_candidate_idiomas_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_candidate_idiomas_idiomas_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "idiomas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidate_skills",
                columns: table => new
                {
                    CandidatesId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    SkillsId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidate_skills", x => new { x.CandidatesId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_candidate_skills_candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_candidate_skills_skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "job_vagas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Titulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DescricaoOriginal = table.Column<string>(type: "NCLOB", maxLength: 5000, nullable: false),
                    DescricaoInclusiva = table.Column<string>(type: "NCLOB", maxLength: 5000, nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoVaga = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ModeloTrabalho = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SalarioMin = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    SalarioMax = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    Beneficios = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ExperienciaRequerida = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IsAtiva = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RecruiterId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_vagas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_job_vagas_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_job_vagas_recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "recruiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Tipo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Mensagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IsRead = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RecruiterId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_notifications_recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "recruiters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "profile_views",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RecruiterId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile_views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_profile_views_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_profile_views_recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "recruiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "saved_candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RecruiterId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saved_candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_saved_candidates_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_saved_candidates_recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "recruiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    JobVagaId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    MatchScore = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IsLikedByCandidate = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsLikedByRecruiter = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matches_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matches_job_vagas_JobVagaId",
                        column: x => x.JobVagaId,
                        principalTable: "job_vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "saved_jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    JobVagaId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saved_jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_saved_jobs_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_saved_jobs_job_vagas_JobVagaId",
                        column: x => x.JobVagaId,
                        principalTable: "job_vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vaga_skills",
                columns: table => new
                {
                    SkillsDesejadasId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    VagasId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaga_skills", x => new { x.SkillsDesejadasId, x.VagasId });
                    table.ForeignKey(
                        name: "FK_vaga_skills_job_vagas_VagasId",
                        column: x => x.VagasId,
                        principalTable: "job_vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vaga_skills_skills_SkillsDesejadasId",
                        column: x => x.SkillsDesejadasId,
                        principalTable: "skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_candidate_idiomas_CandidateId_IdiomaId",
                table: "candidate_idiomas",
                columns: new[] { "CandidateId", "IdiomaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidate_idiomas_IdiomaId",
                table: "candidate_idiomas",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_candidate_skills_SkillsId",
                table: "candidate_skills",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_candidates_Email",
                table: "candidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_educations_CandidateId",
                table: "educations",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_Cnpj",
                table: "empresas",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_experiences_CandidateId",
                table: "experiences",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_experiences_EmpresaId",
                table: "experiences",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_job_vagas_EmpresaId",
                table: "job_vagas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_job_vagas_RecruiterId",
                table: "job_vagas",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_CandidateId_JobVagaId",
                table: "matches",
                columns: new[] { "CandidateId", "JobVagaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_matches_JobVagaId",
                table: "matches",
                column: "JobVagaId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_CandidateId",
                table: "notifications",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_RecruiterId",
                table: "notifications",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_profile_views_CandidateId",
                table: "profile_views",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_profile_views_RecruiterId",
                table: "profile_views",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_recruiters_Email",
                table: "recruiters",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recruiters_EmpresaId",
                table: "recruiters",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_saved_candidates_CandidateId",
                table: "saved_candidates",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_saved_candidates_RecruiterId_CandidateId",
                table: "saved_candidates",
                columns: new[] { "RecruiterId", "CandidateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_saved_jobs_CandidateId_JobVagaId",
                table: "saved_jobs",
                columns: new[] { "CandidateId", "JobVagaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_saved_jobs_JobVagaId",
                table: "saved_jobs",
                column: "JobVagaId");

            migrationBuilder.CreateIndex(
                name: "IX_skills_Nome",
                table: "skills",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vaga_skills_VagasId",
                table: "vaga_skills",
                column: "VagasId");

            migrationBuilder.CreateIndex(
                name: "IX_voluntariados_CandidateId",
                table: "voluntariados",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate_idiomas");

            migrationBuilder.DropTable(
                name: "candidate_skills");

            migrationBuilder.DropTable(
                name: "educations");

            migrationBuilder.DropTable(
                name: "experiences");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "profile_views");

            migrationBuilder.DropTable(
                name: "saved_candidates");

            migrationBuilder.DropTable(
                name: "saved_jobs");

            migrationBuilder.DropTable(
                name: "vaga_skills");

            migrationBuilder.DropTable(
                name: "voluntariados");

            migrationBuilder.DropTable(
                name: "idiomas");

            migrationBuilder.DropTable(
                name: "job_vagas");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "candidates");

            migrationBuilder.DropTable(
                name: "recruiters");

            migrationBuilder.DropTable(
                name: "empresas");

            migrationBuilder.CreateTable(
                name: "TB_CANDIDATO",
                columns: table => new
                {
                    ID_CANDIDATO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CANDIDATO", x => x.ID_CANDIDATO);
                });

            migrationBuilder.CreateTable(
                name: "TB_CANDIDATURA",
                columns: table => new
                {
                    ID_CANDIDATURA = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ID_CANDIDATO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    DT_APLICACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ID_VAGA_MONGO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CANDIDATURA", x => x.ID_CANDIDATURA);
                    table.ForeignKey(
                        name: "FK_TB_CANDIDATURA_TB_CANDIDATO_ID_CANDIDATO",
                        column: x => x.ID_CANDIDATO,
                        principalTable: "TB_CANDIDATO",
                        principalColumn: "ID_CANDIDATO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CANDIDATURA_ID_CANDIDATO",
                table: "TB_CANDIDATURA",
                column: "ID_CANDIDATO");
        }
    }
}
