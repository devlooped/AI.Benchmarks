# Microsoft.Extensions.AI Benchmarks

Uses the new [Microsoft.Extensions.AI](https://devblogs.microsoft.com/dotnet/introducing-microsoft-extensions-ai-preview/) preview 
to compare the relative performance of OpenAI, AzureAI and xAI, using the same client code.

Currently, comparison includes:

* OpenAI: gpt-4o, gpt-4o-mini
* AzureAI: gpt-40, gpt-4o-mini
* xAI: grok-beta, grok-2-latest

Prompt:

```csharp
IList<ChatMessage> prompt =
[
    new ChatMessage(ChatRole.System, "You are a chatbot inspired by the Hitchhiker's Guide to the Galaxy."),
    new ChatMessage(ChatRole.User, "What is the meaning of life, the universe, and everything?"),
];
```

Results:

<!-- include src/AI.Benchmarks/BenchmarkDotNet.Artifacts/results/AI.Benchmarks.ModelPerformance-report-github.md -->
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4317/23H2/2023Update/SunValley3)
Intel Core i9-10900T CPU 1.90GHz, 1 CPU, 20 logical and 10 physical cores
.NET SDK 9.0.100-rc.2.24474.11
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method | Client          | Provider | Model       | Mean    | Error    | StdDev   | Median  |
|------- |---------------- |--------- |------------ |--------:|---------:|---------:|--------:|
| **Chat**   | **aai-gpt-4o**      | **Azure AI** | **gpt-4o**      | **1.145 s** | **0.0877 s** | **0.2385 s** | **1.090 s** |
| **Chat**   | **aai-gpt-4o-mini** | **Azure AI** | **gpt-4o-mini** | **1.525 s** | **0.2311 s** | **0.6592 s** | **1.232 s** |
| **Chat**   | **oai-gpt-4o**      | **OpenAI**   | **gpt-4o**      | **1.761 s** | **0.1907 s** | **0.5473 s** | **1.585 s** |
| **Chat**   | **oai-gpt-4o-mini** | **OpenAI**   | **gpt-4o-mini** | **2.009 s** | **0.1259 s** | **0.3632 s** | **2.008 s** |
| **Chat**   | **xai-grok-beta**   | **xAI**      | **grok-beta**   | **2.764 s** | **0.3631 s** | **1.0000 s** | **2.543 s** |

<!-- src/AI.Benchmarks/BenchmarkDotNet.Artifacts/results/AI.Benchmarks.ModelPerformance-report-github.md -->
