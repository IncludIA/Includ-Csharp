using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using BCrypt.Net;

namespace IncludIA.Application.Service
{
    public class CandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly InclusaoIAService _iaService;

        public CandidateService(ICandidateRepository repository, InclusaoIAService iaService)
        {
            _repository = repository;
            _iaService = iaService;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync(int page, int size)
        {
            return await _repository.GetAllAsync(page, size);
        }

        public async Task<Candidate?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Candidate candidate)
        {
            if (!string.IsNullOrEmpty(candidate.SenhaHash))
            {
                candidate.SenhaHash = BCrypt.Net.BCrypt.HashPassword(candidate.SenhaHash);
            }

            if (!string.IsNullOrEmpty(candidate.ResumoPerfil))
            { 
                var analiseSeguranca = await _iaService.ModerarConteudoAsync(candidate.ResumoPerfil, "perfil_candidato");
                if (analiseSeguranca != null && !analiseSeguranca.Aprovado)
                {
                    throw new InvalidOperationException($"O conteúdo do perfil foi reprovado pela nossa IA de segurança. Motivo: {analiseSeguranca.Motivo}");
                }
                candidate.ResumoInclusivoIA = await _iaService.AnonimizarCurriculoAsync(candidate.ResumoPerfil);
            }

            await _repository.AddAsync(candidate);
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            if (!string.IsNullOrEmpty(candidate.SenhaHash) && !candidate.SenhaHash.StartsWith("$2"))
            {
                candidate.SenhaHash = BCrypt.Net.BCrypt.HashPassword(candidate.SenhaHash);
            }

            if (!string.IsNullOrEmpty(candidate.ResumoPerfil))
            {
                var analiseSeguranca = await _iaService.ModerarConteudoAsync(candidate.ResumoPerfil, "perfil_candidato");
                if (analiseSeguranca != null && !analiseSeguranca.Aprovado)
                {
                    throw new InvalidOperationException($"A atualização do perfil foi reprovada. Motivo: {analiseSeguranca.Motivo}");
                }
                candidate.ResumoInclusivoIA = await _iaService.AnonimizarCurriculoAsync(candidate.ResumoPerfil);
            }

            await _repository.UpdateAsync(candidate);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}