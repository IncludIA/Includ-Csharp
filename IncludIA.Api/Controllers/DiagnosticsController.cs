using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/diagnostics")]
    public class DiagnosticsController : ControllerBase
    {
        private readonly OracleDbContext _oracleContext;
        private readonly InclusaoIAService _iaService;

        public DiagnosticsController(OracleDbContext oracleContext, InclusaoIAService iaService)
        {
            _oracleContext = oracleContext;
            _iaService = iaService;
        }

        [HttpGet("ping")]
        [AllowAnonymous]
        public IActionResult Ping()
        {
            return Ok(new { message = "Pong! API IncludIA is running.", time = DateTime.UtcNow });
        }

        [HttpGet("check-dependencies")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckDependencies()
        {
            var status = new Dictionary<string, string>();

            try
            {
                bool canConnect = await _oracleContext.Database.CanConnectAsync();
                status.Add("Oracle Database", canConnect ? "Online 🟢" : "Offline 🔴");
            }
            catch (Exception ex)
            {
                status.Add("Oracle Database", $"Error: {ex.Message} 🔴");
            }

            try
            {
                
                var analise = await _iaService.ModerarConteudoAsync("Teste de conexão");
                status.Add("IncludIA External API", "Online 🟢");
            }
            catch (Exception ex)
            {
                status.Add("IncludIA External API", $"Unreachable 🔴 ({ex.Message})");
            }

            return Ok(status);
        }
    }
}