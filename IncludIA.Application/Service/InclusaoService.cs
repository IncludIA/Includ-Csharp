using System.Text.RegularExpressions;

namespace IncludIA.Application.Service
{
    public class InclusaoService
    {
        public async Task<string> TornarDescricaoInclusivaAsync(string descricaoOriginal)
        {
            var descricaoInclusiva = Regex.Replace(descricaoOriginal, 
                @"\b(ele/ela|ele|ela|o candidato|a candidata)\b", 
                "a pessoa candidata", 
                RegexOptions.IgnoreCase);

            descricaoInclusiva = Regex.Replace(descricaoInclusiva, 
                @"\b(agressivo|ninja|rockstar)\b", 
                "proativa", 
                RegexOptions.IgnoreCase);
            
            await Task.Delay(250); 
            return descricaoInclusiva;
        }
    }
}