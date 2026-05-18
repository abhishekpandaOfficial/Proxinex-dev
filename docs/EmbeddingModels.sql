INSERT INTO "LLMModels"
(
    "Id",
    "Name",
    "DisplayName",
    "Provider",
    "ModelCategory",
    "SupportsEmbeddings",
    "IsOpenSource",
    "HostingType",
    "ModelUrl",
    "DocumentationUrl",
    "Description",
    "CreatedAtUtc",
    "UpdatedAtUtc"
)
VALUES

(
    gen_random_uuid(),
    'nomic-embed-text',
    'Nomic Embed Text',
    'Nomic',
    'Embedding',
    true,
    true,
    'Ollama',
    'https://ollama.com/library/nomic-embed-text',
    'https://huggingface.co/nomic-ai',
    'Lightweight local embedding model.',
    NOW(),
    NOW()
),

(
    gen_random_uuid(),
    'bge-large-en-v1.5',
    'BGE Large',
    'BAAI',
    'Embedding',
    true,
    true,
    'vLLM',
    'https://huggingface.co/BAAI/bge-large-en-v1.5',
    'https://huggingface.co/BAAI',
    'Enterprise semantic embedding model.',
    NOW(),
    NOW()
);