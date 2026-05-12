namespace Proxinex.Shared.SemanticKernel.Configuration;

public class ModelDefinition
{
    public string Name { get; set; } = default!;

    public string Provider { get; set; } = default!;

    public string Endpoint { get; set; } = default!;

    public string ApiKey { get; set; } = default!;

    public bool SupportsVision { get; set; }

    public bool SupportsCode { get; set; }

    public bool SupportsLongContext { get; set; }

    public bool IsCheap { get; set; }

    public bool IsEnterprise { get; set; }

    public int MaxTokens { get; set; }
}