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

    readonly IList<ChatMessage> prompt =
    [
        new ChatMessage(ChatRole.System, "You are a chatbot inspired by the Hitchhiker's Guide to the Galaxy."),
        new ChatMessage(ChatRole.User, "What is the meaning of life, the universe, and everything?"),
    ];

    [Params("oai-gpt-4o", "oai-gpt-4o-mini", "aai-gpt-4o", "aai-gpt-4o-mini", "xai-grok-3-beta", "xai-grok-3-mini-beta")]
    public string? Client { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        configuration = new ConfigurationBuilder()
            .AddUserSecrets<ModelPerformance>()
            .Build();

        // get the Params attribute on Client and use it to set the Client property
        var clients = GetType().GetProperty(nameof(Client))?.GetCustomAttributes(typeof(ParamsAttribute), false)
            .OfType<ParamsAttribute>()
            .FirstOrDefault()?.Values.OfType<string>();

        var collection = new ServiceCollection();
        Debug.Assert(clients != null, "Did not find expected list of clients");

        foreach (var client in clients)
        {
            // switch on the first 3 characters of the client name
            // then use the model name removing that prefix + '-'
            var model = client[4..];
            var provider = client[..3];
            switch (provider)
            {
                case "oai":
                    collection.AddKeyedChatClient(client, new OpenAI.OpenAIClient(configuration.ǃ("OpenAI:Key")).AsChatClient(model));
                    break;
                case "aai":
                    collection.AddKeyedChatClient(client,
                        new AzureAIInferenceChatClient(
                            new Azure.AI.Inference.ChatCompletionsClient(
                                new Uri(configuration.ǃ("AzureAI:Endpoint")),
                                new AzureKeyCredential(configuration.ǃ("AzureAI:Key"))),
                            model));
                    break;
                case "xai":
                    collection.AddKeyedChatClient(client,
                        new OpenAI.OpenAIClient(
                            new ApiKeyCredential(configuration.ǃ("xAI:Key")),
                            new OpenAI.OpenAIClientOptions
                            {
                                Endpoint = new(configuration.ǃ("xAI:Endpoint"))
                            }).AsChatClient(model));
                    break;
                default:
                    throw new NotSupportedException($"Unknown provider {provider}");
            }
        }

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
