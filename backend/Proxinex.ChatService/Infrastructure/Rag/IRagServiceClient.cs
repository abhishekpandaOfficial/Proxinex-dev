namespace Proxinex.ChatService.Infrastructure.Rag;

public interface IRagServiceClient
{
    Task<List<string>> SearchAsync(
        string query);
}