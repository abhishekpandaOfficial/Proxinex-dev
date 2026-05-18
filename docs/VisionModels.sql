INSERT INTO "LLMModels"
(
    "Id",
    "Name",
    "DisplayName",
    "Provider",
    "ModelCategory",
    "SupportsVision",
    "HostingType",
    "IsOpenSource",
    "IsEnterprise",
    "IsEnabled",
    "ModelUrl",
    "DocumentationUrl",
    "Description",
    "CreatedAtUtc",
    "UpdatedAtUtc"
)
VALUES

(
    gen_random_uuid(),
    'qwen2.5-vl',
    'Qwen 2.5 Vision',
    'Alibaba',
    'Vision',
    true,
    'vLLM',
    true,
    true,
    true,
    'https://ollama.com/library/qwen2.5vl',
    'https://huggingface.co/Qwen',
    'Enterprise multimodal vision model.',
    NOW(),
    NOW()
),

(
    gen_random_uuid(),
    'llava',
    'LLaVA',
    'Microsoft',
    'Vision',
    true,
    'Ollama',
    true,
    false,
    true,
    'https://ollama.com/library/llava',
    'https://huggingface.co/llava-hf',
    'Lightweight local vision model.',
    NOW(),
    NOW()
);