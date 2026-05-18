INSERT INTO "LLMModels"
(
    "Id",
    "Name",
    "DisplayName",
    "Provider",
    "ModelCategory",
    "SupportsSpeechToText",
    "SupportsTextToSpeech",
    "HostingType",
    "IsOpenSource",
    "IsEnabled",
    "Description",
    "CreatedAtUtc",
    "UpdatedAtUtc"
)
VALUES

(
    gen_random_uuid(),
    'whisper-large-v3',
    'Whisper Large V3',
    'OpenAI',
    'Audio',
    true,
    false,
    'LocalGPU',
    true,
    true,
    'Enterprise speech-to-text model.',
    NOW(),
    NOW()
),

(
    gen_random_uuid(),
    'piper',
    'Piper TTS',
    'Piper',
    'Audio',
    false,
    true,
    'LocalCPU',
    true,
    true,
    'Fast local text-to-speech.',
    NOW(),
    NOW()
);