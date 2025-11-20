using Microsoft.ML.Data;

namespace IncludIA.Application.Service;

public class VagaInput
{
    [LoadColumn(0)] public string Descricao { get; set; }
    [LoadColumn(1)] public string Categoria { get; set; } 
}