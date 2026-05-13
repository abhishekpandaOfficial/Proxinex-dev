using Proxinex.RagService.Application.Chunking.Interfaces;
using Proxinex.RagService.Application.Chunking.Services;
using Proxinex.RagService.Application.Embeddings.Interfaces;
using Proxinex.RagService.Application.Embeddings.Services;

using Proxinex.RagService.Application.Ingestion.Interfaces;
using Proxinex.RagService.Application.Ingestion.Services;

using Proxinex.RagService.Application.Retrieval.Interfaces;
using Proxinex.RagService.Application.Retrieval.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<
    ITextChunkingService,
    TextChunkingService>();

builder.Services.AddSingleton<
    IVectorStoreService,
    QdrantVectorStoreService>();

builder.Services.AddSingleton<
    IEmbeddingService,
    OllamaEmbeddingService>();

builder.Services.AddScoped<
    IDocumentIngestionService,
    DocumentIngestionService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();