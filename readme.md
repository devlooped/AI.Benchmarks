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

BenchmarkDotNet v0.14.0, Ubuntu 24.04.1 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.112
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


```
| Method | Client            | Provider | Model         | Mean | Error |
|------- |------------------ |--------- |-------------- |-----:|------:|
| **Chat**   | **aai-gpt-4o**        | **Azure AI** | **gpt-4o**        |   **NA** |    **NA** |
| **Chat**   | **aai-gpt-4o-mini**   | **Azure AI** | **gpt-4o-mini**   |   **NA** |    **NA** |
| **Chat**   | **oai-gpt-4o**        | **OpenAI**   | **gpt-4o**        |   **NA** |    **NA** |
| **Chat**   | **oai-gpt-4o-mini**   | **OpenAI**   | **gpt-4o-mini**   |   **NA** |    **NA** |
| **Chat**   | **xai-grok-2-latest** | **xAI**      | **grok-2-latest** |   **NA** |    **NA** |
| **Chat**   | **xai-grok-beta**     | **xAI**      | **grok-beta**     |   **NA** |    **NA** |

Benchmarks with issues:
  ModelPerformance.Chat: DefaultJob [Client=aai-gpt-4o]
  ModelPerformance.Chat: DefaultJob [Client=aai-gpt-4o-mini]
  ModelPerformance.Chat: DefaultJob [Client=oai-gpt-4o]
  ModelPerformance.Chat: DefaultJob [Client=oai-gpt-4o-mini]
  ModelPerformance.Chat: DefaultJob [Client=xai-grok-2-latest]
  ModelPerformance.Chat: DefaultJob [Client=xai-grok-beta]

<!-- src/AI.Benchmarks/BenchmarkDotNet.Artifacts/results/AI.Benchmarks.ModelPerformance-report-github.md -->
