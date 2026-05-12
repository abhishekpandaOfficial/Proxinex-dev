using Proxinex.Shared.SemanticKernel.Configuration;

namespace Proxinex.Shared.SemanticKernel.Routing.Interfaces;

public interface IModelRouter
{
    ModelDefinition Route(
        string prompt,
        bool requiresVision = false,
        bool requiresCode = false,
        bool requiresLongContext = false,
        bool enterprise = false);
}