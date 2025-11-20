using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using IncludIA.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Trace;
using Serilog;
using System.Text;

// Configuração Inicial do Serilog (Logs)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/includia_log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração de Logs
builder.Host.UseSerilog();

// 2. Configuração de Controllers e JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();

// 3. Configuração do Banco de Dados (Oracle e Mongo)
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb"))
);

builder.Services.AddSingleton<MongoDbContext>();

// 4. Injeção de Dependência - Repositories (Camada de Infraestrutura)
// Core
builder.Services.AddScoped<ICandidatoRepository, ICandidatoRepository>();
builder.Services.AddScoped<IRecruiterRepository, RecruiterRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IJobVagaRepository, JobVagaRepository>();

// Currículo & Perfil
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IIdiomaRepository, IdiomaRepository>();
builder.Services.AddScoped<ICandidateIdiomaRepository, CandidateIdiomaRepository>();
builder.Services.AddScoped<IVoluntariadoRepository, VoluntariadoRepository>();

// Ações & Relacionamentos
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IProfileViewRepository, ProfileViewRepository>();
builder.Services.AddScoped<ISavedJobRepository, SavedJobRepository>();
builder.Services.AddScoped<ISavedCandidateRepository, SavedCandidateRepository>();


// 5. Injeção de Dependência - Services (Camada de Aplicação)
// Serviços Externos (IA)
builder.Services.AddHttpClient<InclusaoIAService>();

// Domain Services
builder.Services.AddScoped<CandidateService>();
builder.Services.AddScoped<RecruiterService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<JobVagaService>();
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddScoped<EducationService>();
builder.Services.AddScoped<ExperienceService>();
builder.Services.AddScoped<SkillService>();
builder.Services.AddScoped<CandidateIdiomaService>();
builder.Services.AddScoped<VoluntariadoService>();
builder.Services.AddScoped<SavedJobService>();
builder.Services.AddScoped<SavedCandidateService>();
builder.Services.AddScoped<ProfileViewService>();


// 6. Configuração de Versionamento da API
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// 7. Configuração do Swagger (com Suporte a JWT e Versionamento)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IncludIA API", Version = "v1", Description = "Global Solution 2025 - Future of Work" });

    // Configuração para Autenticação no Swagger (Cadeado)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT assim: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// 8. Configuração de Autenticação e Segurança (JWT)
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "super_secret_key_default_change_me_please_123"); 
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// 9. Monitoramento (Health Checks e OpenTelemetry)
builder.Services.AddHealthChecks();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter());


var app = builder.Build();

// === Pipeline de Requisições HTTP ===

app.UseSerilogRequestLogging(); 

// Swagger habilitado apenas em desenvolvimento (ou mude conforme necessidade)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IncludIA API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();