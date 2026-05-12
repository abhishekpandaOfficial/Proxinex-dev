namespace Proxinex.Shared.SemanticKernel.Configurations
{
    public class OpenAIOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string ChatModels { get; set; } = "gpt-4o";
        public string DeploymentName { get; set; } = "text-embedding-3-small";
    }
}