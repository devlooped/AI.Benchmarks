# Microsoft.Extensions.AI Benchmarks

Uses the new [Microsoft.Extensions.AI](https://devblogs.microsoft.com/dotnet/introducing-microsoft-extensions-ai-preview/) preview 
to compare the relative performance of OpenAI, AzureAI and xAI, using the same client code.

Currently, comparison includes:

* OpenAI: gpt-4o, gpt-4o-mini
* AzureAI: gpt-4o, gpt-4o-mini
* xAI: grok-3-beta, grok-3-mini-beta

![xAI grok2](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fgithub.com%2Fdevlooped%2FAI.Benchmarks%2Fraw%2Frefs%2Fheads%2Fmain%2Fartifacts%2Fsummary.json&query=%24.xai.mean&logo=x&label=grok2)
![Azure gpt-4o](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fgithub.com%2Fdevlooped%2FAI.Benchmarks%2Fraw%2Frefs%2Fheads%2Fmain%2Fartifacts%2Fsummary.json&query=%24..aai.mean&label=gpt-4o&logo=data:image/svg+xml;base64,PHN2ZyBpZD0idXVpZC1hZGJkYWU4ZS01YTQxLTQ2ZDEtOGMxOC1hYTczY2RiZmVlMzIiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjE4IiBoZWlnaHQ9IjE4IiB2aWV3Qm94PSIwIDAgMTggMTgiPjxkZWZzPjxyYWRpYWxHcmFkaWVudCBpZD0idXVpZC0yYTc0MDdhYS1iNzg3LTQ4ZGQtYTk2YS0wZDgxYWI2ZTkzYmIiIGN4PSItNjcuOTgxIiBjeT0iNzkzLjE5OSIgcj0iLjQ1IiBncmFkaWVudFRyYW5zZm9ybT0idHJhbnNsYXRlKC0xNzkzOS4wMyAyMDM2OC4wMjkpIHJvdGF0ZSg0NSkgc2NhbGUoMjUuMDkxIC0zNC4xNDkpIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSI+PHN0b3Agb2Zmc2V0PSIwIiBzdG9wLWNvbG9yPSIjODNiOWY5IiAvPjxzdG9wIG9mZnNldD0iMSIgc3RvcC1jb2xvcj0iIzAwNzhkNCIgLz48L3JhZGlhbEdyYWRpZW50PjwvZGVmcz48cGF0aCBkPSJtMCwyLjd2MTIuNmMwLDEuNDkxLDEuMjA5LDIuNywyLjcsMi43aDEyLjZjMS40OTEsMCwyLjctMS4yMDksMi43LTIuN1YyLjdjMC0xLjQ5MS0xLjIwOS0yLjctMi43LTIuN0gyLjdDMS4yMDksMCwwLDEuMjA5LDAsMi43Wk0xMC44LDB2My42YzAsMy45NzYsMy4yMjQsNy4yLDcuMiw3LjJoLTMuNmMtMy45NzYsMC03LjE5OSwzLjIyMi03LjIsNy4xOTh2LTMuNTk4YzAtMy45NzYtMy4yMjQtNy4yLTcuMi03LjJoMy42YzMuOTc2LDAsNy4yLTMuMjI0LDcuMi03LjJaIiBmaWxsPSJ1cmwoI3V1aWQtMmE3NDA3YWEtYjc4Ny00OGRkLWE5NmEtMGQ4MWFiNmU5M2JiKSIgc3Ryb2tlLXdpZHRoPSIwIiAvPjwvc3ZnPg==)
![OpenAI gpt-4o](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fgithub.com%2Fdevlooped%2FAI.Benchmarks%2Fraw%2Frefs%2Fheads%2Fmain%2Fartifacts%2Fsummary.json&query=%24.oai.mean&logo=openai&label=gpt-4o)

Prompt:

```csharp
IList<ChatMessage> prompt =
[
    new ChatMessage(ChatRole.System, "You are a chatbot inspired by the Hitchhiker's Guide to the Galaxy."),
    new ChatMessage(ChatRole.User, "What is the meaning of life, the universe, and everything?"),
];
```

Results:

<!-- include artifacts/results/AI.Benchmarks.ModelPerformance-report-github.md -->
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3775)
Intel Core i9-10900T CPU 1.90GHz, 1 CPU, 20 logical and 10 physical cores
.NET SDK 9.0.201
  [Host]     : .NET 8.0.15 (8.0.1525.16413), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.15 (8.0.1525.16413), X64 RyuJIT AVX2


```
| Method | Client               | Provider | Model            | Mean     | Error    | StdDev    | Median  |
|------- |--------------------- |--------- |----------------- |---------:|---------:|----------:|--------:|
| **Chat**   | **aai-gpt-4o**           | **Azure AI** | **gpt-4o**           | **20.181 s** | **7.8701 s** | **23.2052 s** | **5.630 s** |
| **Chat**   | **aai-gpt-4o-mini**      | **Azure AI** | **gpt-4o-mini**      | **20.171 s** | **6.6035 s** | **19.4707 s** | **9.726 s** |
| **Chat**   | **oai-gpt-4o**           | **OpenAI**   | **gpt-4o**           |  **2.013 s** | **0.1394 s** |  **0.4000 s** | **1.937 s** |
| **Chat**   | **oai-gpt-4o-mini**      | **OpenAI**   | **gpt-4o-mini**      |  **2.930 s** | **0.1641 s** |  **0.4736 s** | **2.916 s** |
| **Chat**   | **xai-grok-3-beta**      | **xAI**      | **grok-3-beta**      |       **NA** |       **NA** |        **NA** |      **NA** |
| **Chat**   | **xai-grok-3-mini-beta** | **xAI**      | **grok-3-mini-beta** |  **5.666 s** | **0.2034 s** |  **0.5964 s** | **5.699 s** |

Benchmarks with issues:
  ModelPerformance.Chat: DefaultJob [Client=xai-grok-3-beta]

<!-- artifacts/results/AI.Benchmarks.ModelPerformance-report-github.md -->

## Run locally

You can trivially update (and optionally send a PR) the results by running the benchmarks locally.
Simply fork the repository, clone it locally and run the `.\update.ps1` script from a `pwsh` prompt.

The only requirement is to have `jq` installed and available in your `PATH`.
