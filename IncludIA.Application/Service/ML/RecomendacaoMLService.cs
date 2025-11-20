using Microsoft.ML;

namespace IncludIA.Application.Service;

public class RecomendacaoMLService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public RecomendacaoMLService()
        {
            _mlContext = new MLContext();
            TreinarModeloEmMemoria();
        }

        private void TreinarModeloEmMemoria()
        {
            var dadosTreino = new List<VagaInput>
            {
                new VagaInput { Descricao = "Desenvolver APIs em C# e .NET Core com SQL Server", Categoria = "Backend" },
                new VagaInput { Descricao = "Criar interfaces com React e CSS", Categoria = "Frontend" },
                new VagaInput { Descricao = "Gerenciar servidores Azure e pipelines CI/CD", Categoria = "DevOps" },
                new VagaInput { Descricao = "Análise de dados e criação de dashboards PowerBI", Categoria = "Dados" },
                new VagaInput { Descricao = "Manutenção de computadores e redes", Categoria = "Suporte" }
            };

            var trainingData = _mlContext.Data.LoadFromEnumerable(dadosTreino);

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(VagaInput.Descricao))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var dataProcessPipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(VagaInput.Categoria))
                .Append(pipeline);

            _model = dataProcessPipeline.Fit(trainingData);
        }

        public string PreverCategoria(string descricaoVaga)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<VagaInput, VagaPrediction>(_model);
            var resultado = predictionEngine.Predict(new VagaInput { Descricao = descricaoVaga });
            return resultado.CategoriaPrevista;
        }
    }