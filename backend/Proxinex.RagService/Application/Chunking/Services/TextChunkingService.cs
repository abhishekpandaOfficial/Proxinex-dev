using Proxinex.RagService.Application.Chunking.Interfaces;

namespace Proxinex.RagService.Application.Chunking.Services;

public class TextChunkingService
    : ITextChunkingService
{
    public List<string> Chunk(
        string text,
        int chunkSize = 500)
    {
        var words =
            text.Split(' ');

        var chunks =
            new List<string>();

        for (int i = 0;
             i < words.Length;
             i += chunkSize)
        {
            var chunk =
                string.Join(
                    " ",
                    words.Skip(i)
                         .Take(chunkSize));

            chunks.Add(chunk);
        }

        return chunks;
    }
}