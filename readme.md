# Microsoft.Extensions.AI Benchmarks

Uses the new [Microsoft.Extensions.AI](https://devblogs.microsoft.com/dotnet/introducing-microsoft-extensions-ai-preview/) preview 
to compare the relative performance of OpenAI, AzureAI and xAI, using the same client code.

Currently, comparison includes:

* OpenAI: gpt-4o, gpt-4o-mini
* AzureAI: gpt-40, gpt-4o-mini
* xAI: grok-beta

Prompt:

```csharp
IList<ChatMessage> prompt =
[
    new ChatMessage(ChatRole.System, "You are Grok, a chatbot inspired by the Hitchhiker's Guide to the Galaxy."),
    new ChatMessage(ChatRole.User, "What is the meaning of life, the universe, and everything?"),
];
```

Results:

<!-- include src/AI.Benchmarks/BenchmarkDotNet.Artifacts/results/AI.Benchmarks.ModelPerformance-report-github.md -->