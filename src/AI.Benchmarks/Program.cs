using AI.Benchmarks;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ModelPerformance>(ManualConfig
#if DEBUG
    .Create(new DebugInProcessConfig())
#else
    .Create(DefaultConfig.Instance)
    .AddExporter(BenchmarkDotNet.Exporters.Json.JsonExporter.Full)
#endif
    .AddColumn(new ProviderColumn())
    .AddColumn(new ModelColumn())
    .WithArtifactsPath(Path.Combine(ThisAssembly.Git.Root, "artifacts")),
#if DEBUG
    args
#else
    null
#endif
    );

// Run benchmark with debug in-process mode
//BenchmarkRunner.Run<ModelPerformance>(new DebugInProcessConfig(), args);

class ProviderColumn : IColumn
{
    public string Id => "Provider";

    public string ColumnName => "Provider";

    public bool AlwaysShow => true;

    public ColumnCategory Category => ColumnCategory.Params;

    public int PriorityInCategory => 0;

    public bool IsNumeric => false;

    public UnitType UnitType => UnitType.Dimensionless;

    public string Legend => "Provider";

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => benchmarkCase.Parameters[0].Value.ToString()![..3] switch
    {
        "oai" => "OpenAI",
        "aai" => "Azure AI",
        "xai" => "xAI",
        _ => "Unknown",
    };

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);
    public bool IsAvailable(Summary summary) => true;
    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
}

class ModelColumn : IColumn
{
    public string Id => "Model";

    public string ColumnName => "Model";

    public bool AlwaysShow => true;

    public ColumnCategory Category => ColumnCategory.Params;

    public int PriorityInCategory => 1;

    public bool IsNumeric => false;

    public UnitType UnitType => UnitType.Dimensionless;

    public string Legend => "Model";

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => benchmarkCase.Parameters[0].Value.ToString()![4..];
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);
    public bool IsAvailable(Summary summary) => true;
    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
}