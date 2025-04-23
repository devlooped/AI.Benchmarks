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
