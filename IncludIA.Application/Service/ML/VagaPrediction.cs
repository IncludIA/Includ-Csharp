using Microsoft.ML.Data;

namespace IncludIA.Application.Service;

public class VagaPrediction
{
    [ColumnName("PredictedLabel")] public string CategoriaPrevista { get; set; }
}