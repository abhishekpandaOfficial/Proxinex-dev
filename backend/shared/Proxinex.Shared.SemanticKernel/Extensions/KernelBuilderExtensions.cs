using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Proxinex.Shared.SemanticKernel.Configuration;

namespace Proxinex.Shared.SemanticKernel.Extensions;

public static class KernelBuilderExtensions
{
    public static IServiceCollection AddProxinexKernel(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bind Ollama settings
        services.Configure<OllamaOptions>(
            configuration.GetSection("Ollama"));

        // Register Semantic Kernel
        services.AddSingleton<Kernel>(sp =>
        {
            var ollamaOptions =
                sp.GetRequiredService<IOptions<OllamaOptions>>().Value;

            var builder = Kernel.CreateBuilder();

            builder.AddOllamaChatCompletion(
                modelId: ollamaOptions.ModelId,
                endpoint: new Uri(ollamaOptions.Endpoint));

            return builder.Build();
        });

        return services;
    }
}