using Asp.Versioning;
using IncludIA.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/saved")]
    public class SavedContentController : ControllerBase
    {
        private readonly SavedJobService _jobService;
        private readonly SavedCandidateService _candidateService;

        public SavedContentController(SavedJobService jobService, SavedCandidateService candidateService)
        {
            _jobService = jobService;
            _candidateService = candidateService;
        }

        [HttpGet("jobs/{candidateId}")]
        public async Task<IActionResult> GetSavedJobs(Guid candidateId)
        {
            return Ok(await _jobService.GetSavedJobsAsync(candidateId));
        }

        [HttpPost("jobs")]
        public async Task<IActionResult> SaveJob(Guid candidateId, Guid vagaId)
        {
            await _jobService.SaveJobAsync(candidateId, vagaId);
            return Ok();
        }

        [HttpDelete("jobs/{id}")]
        public async Task<IActionResult> UnsaveJob(Guid id)
        {
            await _jobService.UnsaveJobAsync(id);
            return NoContent();
        }

        [HttpGet("candidates/{recruiterId}")]
        public async Task<IActionResult> GetSavedCandidates(Guid recruiterId)
        {
            return Ok(await _candidateService.GetSavedCandidatesAsync(recruiterId));
        }

        [HttpPost("candidates")]
        public async Task<IActionResult> SaveCandidate(Guid recruiterId, Guid candidateId)
        {
            await _candidateService.SaveCandidateAsync(recruiterId, candidateId);
            return Ok();
        }
    }
}