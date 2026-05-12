using Microsoft.Extensions.Options;
using Proxinex.Shared.SemanticKernel.Configuration;
using Proxinex.Shared.SemanticKernel.Routing.Interfaces;

namespace Proxinex.Shared.SemanticKernel.Routing;

public class ModelRouter : IModelRouter
{
    private readonly AIModelsOptions _options;

    public ModelRouter(
        IOptions<AIModelsOptions> options)
    {
        _options = options.Value;
    }

    public ModelDefinition Route(
        string prompt,
        bool requiresVision = false,
        bool requiresCode = false,
        bool requiresLongContext = false,
        bool enterprise = false)
    {
        var models = _options.Models;

        ModelDefinition selectedModel;

        // Vision
        if (requiresVision)
        {
            selectedModel =
                models.FirstOrDefault(
                    x => x.SupportsVision)
                ?? models.First(
                    x => x.Name == "phi3:mini");

            Console.WriteLine(
                $"[MODEL ROUTER] Vision → {selectedModel.Name}");

            return selectedModel;
        }

        // Coding
        if (requiresCode)
        {
            selectedModel =
                models.First(
                    x => x.Name == "phi3:mini");

            Console.WriteLine(
                $"[MODEL ROUTER] Coding → {selectedModel.Name}");

            return selectedModel;
        }

        // Long Context
        if (requiresLongContext)
        {
            selectedModel =
                models.First(
                    x => x.Name == "phi3:mini");

            Console.WriteLine(
                $"[MODEL ROUTER] Long Context → {selectedModel.Name}");

            return selectedModel;
        }

        // Enterprise
        if (enterprise)
        {
            selectedModel =
                models.First(
                    x => x.Name == "phi3:mini");

            Console.WriteLine(
                $"[MODEL ROUTER] Enterprise → {selectedModel.Name}");

            return selectedModel;
        }

        // Default Cheap Model
        selectedModel =
            models.First(
                x => x.Name == "tinyllama:latest");

        Console.WriteLine(
            $"[MODEL ROUTER] Cheap → {selectedModel.Name}");

        return selectedModel;
    }
}