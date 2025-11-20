using Asp.Versioning;
using IncludIA.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfileViewController : ControllerBase
    {
        private readonly ProfileViewService _service;

        public ProfileViewController(ProfileViewService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterView(Guid recruiterId, Guid candidateId)
        {
            await _service.RegisterViewAsync(recruiterId, candidateId);
            return Ok();
        }

        [HttpGet("count/{candidateId}")]
        public async Task<IActionResult> GetCount(Guid candidateId)
        {
            var count = await _service.GetProfileViewsCountAsync(candidateId);
            return Ok(new { CandidateId = candidateId, Views = count });
        }
    }
}