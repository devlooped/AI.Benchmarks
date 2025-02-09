using System.ClientModel;
using System.Diagnostics;
using Azure;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.Extensions.Configuration.RequiredConfigurationExtensions;

namespace AI.Benchmarks;

public class ModelPerformance
{
    IServiceProvider? services;
    IConfiguration? configuration;

    ChatOptions options = new ChatOptions
    {
        MaxOutputTokens = 512,
    };

    IList<ChatMessage> prompt =
    [
        new ChatMessage(ChatRole.System, "You are a chatbot inspired by the Hitchhiker's Guide to the Galaxy."),
        new ChatMessage(ChatRole.User, "What is the meaning of life, the universe, and everything?"),
    ];

    [Params("oai-gpt-4o", "oai-gpt-4o-mini", "aai-gpt-4o", "aai-gpt-4o-mini", "xai-grok-beta", "xai-grok-2-latest")]
    public string? Client { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        configuration = new ConfigurationBuilder()
            .AddUserSecrets<ModelPerformance>()
            .Build();

        var collection = new ServiceCollection();
        collection.AddKeyedChatClient("oai-gpt-4o", 
            new OpenAI.OpenAIClient(configuration.ǃ("OpenAI:Key")).AsChatClient("gpt-4o"));
        collection.AddKeyedChatClient("oai-gpt-4o-mini", 
            new OpenAI.OpenAIClient(configuration.ǃ("OpenAI:Key")).AsChatClient("gpt-4o-mini"));

        collection.AddKeyedChatClient("aai-gpt-4o",
            new AzureAIInferenceChatClient(
                new Azure.AI.Inference.ChatCompletionsClient(
                    new Uri(configuration.ǃ("AzureAI:Endpoint")),
                    new AzureKeyCredential(configuration.ǃ("AzureAI:Key"))),
                "gpt-4o"));
        collection.AddKeyedChatClient("aai-gpt-4o-mini",
            new AzureAIInferenceChatClient(
                new Azure.AI.Inference.ChatCompletionsClient(
                    new Uri(configuration.ǃ("AzureAI:Endpoint")),
                    new AzureKeyCredential(configuration.ǃ("AzureAI:Key"))),
                "gpt-4o-mini"));

        collection.AddKeyedChatClient("xai-grok-beta",
            new OpenAI.OpenAIClient(
                new ApiKeyCredential(configuration.ǃ("xAI:Key")),
                new OpenAI.OpenAIClientOptions
                {
                    Endpoint = new(configuration.ǃ("xAI:Endpoint"))
                }).AsChatClient("grok-beta"));
        collection.AddKeyedChatClient("xai-grok-2-latest",
            new OpenAI.OpenAIClient(
                new ApiKeyCredential(configuration.ǃ("xAI:Key")),
                new OpenAI.OpenAIClientOptions
                {
                    Endpoint = new(configuration.ǃ("xAI:Endpoint"))
                }).AsChatClient("grok-2-latest"));

        services = collection.BuildServiceProvider();
    }

    [Benchmark]
    public async Task Chat()
    {
        var chat = services!.GetRequiredKeyedService<IChatClient>(Client!);

        var result = await chat.CompleteAsync(prompt, options);
        Debug.Assert(result != null);
    }
}
