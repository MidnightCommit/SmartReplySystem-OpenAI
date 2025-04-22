using Microsoft.AspNetCore.Mvc;
using SmartReplySystem.Interfaces;
using SmartReplySystem.Models;

namespace SmartReplySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiryController : ControllerBase
    {
        private readonly IAiService _aiService;
        private readonly IEmailService _emailService;

        public InquiryController(IAiService aiService, IEmailService emailService)
        {
            _aiService = aiService;
            _emailService = emailService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitInquiry([FromBody] InquiryDto inquiry)
        {
            Console.WriteLine("Hitting Post API  for Submition.");
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // 1. Generate smart reply
                var aiReply = await _aiService.GenerateReplyAsync(inquiry.Message);

                // 2. Send email with AI reply
                var success = await _emailService.SendEmailAsync(inquiry.Email, inquiry.Name, aiReply);

                if (!success)
                    return StatusCode(500, "Failed to send the email.");

                return Ok(new { message = "Inquiry processed and email sent successfully." });
            }
            catch (Exception ex)
            {
                // Log the error (you can add a logger later)
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }

        [HttpPost("generate-reply")]
        public async Task<IActionResult> GenerateReply([FromBody] MessageDto message)
        {
            try
            {
                var reply = await _aiService.GenerateReplyAsync(message.Content);
                return Ok(new { Reply = reply });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
