```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4751/23H2/2023Update/SunValley3)
Intel Core i9-10900T CPU 1.90GHz, 1 CPU, 20 logical and 10 physical cores
.NET SDK 9.0.200-preview.0.25057.12
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


```
| Method | Client            | Provider | Model         | Mean    | Error    | StdDev   | Median  |
|------- |------------------ |--------- |-------------- |--------:|---------:|---------:|--------:|
| **Chat**   | **aai-gpt-4o**        | **Azure AI** | **gpt-4o**        | **2.414 s** | **0.5845 s** | **1.6197 s** | **1.670 s** |
| **Chat**   | **aai-gpt-4o-mini**   | **Azure AI** | **gpt-4o-mini**   | **2.748 s** | **0.5885 s** | **1.6599 s** | **1.954 s** |
| **Chat**   | **oai-gpt-4o**        | **OpenAI**   | **gpt-4o**        | **2.646 s** | **0.2275 s** | **0.6563 s** | **2.493 s** |
| **Chat**   | **oai-gpt-4o-mini**   | **OpenAI**   | **gpt-4o-mini**   | **2.845 s** | **0.2176 s** | **0.6173 s** | **2.738 s** |
| **Chat**   | **xai-grok-2-latest** | **xAI**      | **grok-2-latest** | **1.629 s** | **0.1238 s** | **0.3651 s** | **1.618 s** |
| **Chat**   | **xai-grok-beta**     | **xAI**      | **grok-beta**     | **1.731 s** | **0.1186 s** | **0.3402 s** | **1.697 s** |
