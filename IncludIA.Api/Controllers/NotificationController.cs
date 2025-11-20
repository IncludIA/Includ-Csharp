using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _service;

        public NotificationController(NotificationService service)
        {
            _service = service;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMyNotifications(Guid userId)
        {
            var notifs = await _service.GetUserNotificationsAsync(userId);
            return Ok(notifs);
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] Notification notification)
        {
            await _service.SendNotificationAsync(notification);
            return Ok();
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            await _service.MarkAsReadAsync(id);
            return NoContent();
        }
    }
}