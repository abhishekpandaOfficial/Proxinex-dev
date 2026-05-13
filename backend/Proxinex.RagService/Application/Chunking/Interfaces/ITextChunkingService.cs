namespace Proxinex.RagService.Application.Chunking.Interfaces;

public interface ITextChunkingService
{
    List<string> Chunk(
        string text,
        int chunkSize = 500);
}