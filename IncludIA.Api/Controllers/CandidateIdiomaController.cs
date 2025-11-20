using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CandidateIdiomaController : ControllerBase
    {
        private readonly CandidateIdiomaService _service;
        public CandidateIdiomaController(CandidateIdiomaService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> AddIdioma([FromBody] CandidateIdioma idioma)
        {
            await _service.AddIdiomaToCandidateAsync(idioma);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveIdioma(Guid id)
        {
            await _service.RemoveIdiomaFromCandidateAsync(id);
            return NoContent();
        }
    }
}