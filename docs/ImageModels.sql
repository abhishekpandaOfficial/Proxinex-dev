INSERT INTO "LLMModels"
(
    "Id",
    "Name",
    "DisplayName",
    "Provider",
    "ModelCategory",
    "SupportsImageGeneration",
    "HostingType",
    "IsOpenSource",
    "IsEnabled",
    "ModelUrl",
    "Description",
    "CreatedAtUtc",
    "UpdatedAtUtc"
)
VALUES

(
    gen_random_uuid(),
    'flux-dev',
    'Flux Dev',
    'Black Forest Labs',
    'Image',
    true,
    'ComfyUI',
    true,
    true,
    'https://huggingface.co/black-forest-labs',
    'High quality open-source image generation.',
    NOW(),
    NOW()
),

(
    gen_random_uuid(),
    'sdxl',
    'Stable Diffusion XL',
    'Stability AI',
    'Image',
    true,
    'ComfyUI',
    true,
    true,
    'https://huggingface.co/stabilityai',
    'Stable production image generation.',
    NOW(),
    NOW()
);