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
        new ChatMessage(ChatRole.System, "You are Grok, a chatbot inspired by the Hitchhiker's Guide to the Galaxy."),
        new ChatMessage(ChatRole.User, "What is the meaning of life, the universe, and everything?"),
    ];

    [Params("oai-gpt-4o", "oai-gpt-4o-mini", "aai-gpt-4o", "aai-gpt-4o-mini", "xai-grok-beta")]
    public string? Client { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        configuration = new ConfigurationBuilder()
            .AddUserSecrets<ModelPerformance>()
            .Build();

        services = new ServiceCollection()
            .AddKeyedChatClient("oai-gpt-4o", builder => builder
                .Use(new OpenAI.OpenAIClient(configuration.ǃ("OpenAI:Key")).AsChatClient("gpt-4o")))
            .AddKeyedChatClient("oai-gpt-4o-mini", builder => builder
                .Use(new OpenAI.OpenAIClient(configuration.ǃ("OpenAI:Key")).AsChatClient("gpt-4o-mini")))
            .AddKeyedChatClient("aai-gpt-4o", builder => builder
                .Use(new AzureAIInferenceChatClient(
                    new Azure.AI.Inference.ChatCompletionsClient(
                        new Uri(configuration.ǃ("AzureAI:Endpoint")),
                        new AzureKeyCredential(configuration.ǃ("AzureAI:Key"))),
                    "gpt-4o")))
            .AddKeyedChatClient("aai-gpt-4o-mini", builder => builder
                .Use(new AzureAIInferenceChatClient(
                    new Azure.AI.Inference.ChatCompletionsClient(
                        new Uri(configuration.ǃ("AzureAI:Endpoint")),
                        new AzureKeyCredential(configuration.ǃ("AzureAI:Key"))),
                    "gpt-4o-mini")))
            .AddKeyedChatClient("xai-grok-beta", builder => builder
                .Use(new OpenAI.OpenAIClient(
                    new ApiKeyCredential(configuration.ǃ("xAI:Key")),
                    new OpenAI.OpenAIClientOptions
                    {
                        Endpoint = new(configuration.ǃ("xAI:Endpoint"))
                    }).AsChatClient("grok-beta")))
            .BuildServiceProvider();
    }

    [Benchmark]
    public async Task Chat()
    {
        var chat = services!.GetRequiredKeyedService<IChatClient>(Client!);

        var result = await chat.CompleteAsync(prompt, options);
        Debug.Assert(result != null);
    }
}
