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

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4751/23H2/2023Update/SunValley3)
Intel Core i9-10900T CPU 1.90GHz, 1 CPU, 20 logical and 10 physical cores
.NET SDK 9.0.200-preview.0.25057.12
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


```
| Method | Client            | Provider | Model         | Mean    | Error    | StdDev   | Median  |
|------- |------------------ |--------- |-------------- |--------:|---------:|---------:|--------:|
| **Chat**   | **aai-gpt-4o**        | **Azure AI** | **gpt-4o**        | **1.536 s** | **0.1220 s** | **0.3298 s** | **1.445 s** |
| **Chat**   | **aai-gpt-4o-mini**   | **Azure AI** | **gpt-4o-mini**   | **1.691 s** | **0.1988 s** | **0.5608 s** | **1.467 s** |
| **Chat**   | **oai-gpt-4o**        | **OpenAI**   | **gpt-4o**        | **2.299 s** | **0.1650 s** | **0.4544 s** | **2.287 s** |
| **Chat**   | **oai-gpt-4o-mini**   | **OpenAI**   | **gpt-4o-mini**   | **2.738 s** | **0.2487 s** | **0.7135 s** | **2.653 s** |
| **Chat**   | **xai-grok-2-latest** | **xAI**      | **grok-2-latest** | **1.614 s** | **0.1312 s** | **0.3849 s** | **1.565 s** |
| **Chat**   | **xai-grok-beta**     | **xAI**      | **grok-beta**     | **1.656 s** | **0.1114 s** | **0.3231 s** | **1.676 s** |

<!-- src/AI.Benchmarks/BenchmarkDotNet.Artifacts/results/AI.Benchmarks.ModelPerformance-report-github.md -->
