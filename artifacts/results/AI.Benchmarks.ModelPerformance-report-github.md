```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4751/23H2/2023Update/SunValley3)
Intel Core i9-10900T CPU 1.90GHz, 1 CPU, 20 logical and 10 physical cores
.NET SDK 9.0.200-preview.0.25057.12
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


```
| Method | Client            | Provider | Model         | Mean    | Error    | StdDev   | Median  |
|------- |------------------ |--------- |-------------- |--------:|---------:|---------:|--------:|
| **Chat**   | **aai-gpt-4o**        | **Azure AI** | **gpt-4o**        | **1.675 s** | **0.1482 s** | **0.4082 s** | **1.557 s** |
| **Chat**   | **aai-gpt-4o-mini**   | **Azure AI** | **gpt-4o-mini**   | **1.726 s** | **0.2052 s** | **0.5687 s** | **1.535 s** |
| **Chat**   | **oai-gpt-4o**        | **OpenAI**   | **gpt-4o**        | **2.013 s** | **0.2078 s** | **0.5759 s** | **1.870 s** |
| **Chat**   | **oai-gpt-4o-mini**   | **OpenAI**   | **gpt-4o-mini**   | **2.757 s** | **0.2057 s** | **0.5666 s** | **2.671 s** |
| **Chat**   | **xai-grok-2-latest** | **xAI**      | **grok-2-latest** | **1.803 s** | **0.1928 s** | **0.5342 s** | **1.701 s** |
| **Chat**   | **xai-grok-beta**     | **xAI**      | **grok-beta**     | **2.028 s** | **0.1480 s** | **0.4100 s** | **1.994 s** |
