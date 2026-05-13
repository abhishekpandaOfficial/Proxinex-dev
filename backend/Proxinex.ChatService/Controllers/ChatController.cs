using Microsoft.AspNetCore.Mvc;
using Proxinex.ChatService.Application.Chat.Interfaces;
using Proxinex.Shared.Contracts.Chat;

namespace Proxinex.ChatService.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IChatOrchestrationService _chatService;
    
    public ChatController(
        IChatOrchestrationService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> Chat(
        [FromBody] ChatRequest request)
    {
        var response =
            await _chatService.ProcessChatAsync(
                request);

        return Ok(response);
    }

    [HttpPost("stream")]
    public async Task StreamChat(
        [FromBody] ChatRequest request)
    {
        Response.ContentType =
            "text/event-stream";

        await foreach (
            var chunk in _chatService.StreamChatAsync(
                request))
        {
            await Response.WriteAsync(
                $"data: {chunk}\n\n");

            await Response.Body.FlushAsync();
        }
    }
}