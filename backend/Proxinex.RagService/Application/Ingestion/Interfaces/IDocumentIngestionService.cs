namespace Proxinex.RagService.Application.Ingestion.Interfaces;

public interface IDocumentIngestionService
{
    Task IngestAsync(
        string documentName,
        string content);
}