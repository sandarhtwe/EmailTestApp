using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace EmailTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(string body)
        {
            var email=new MimeMessage();
            email.From.Add(MailboxAddress.Parse("sandarhtwe.sdh16@gmail.com"));
            email.To.Add(MailboxAddress.Parse("sandarhtwe@acedatasystems.com"));
            email.Subject="Test Email Service";
            email.Body=new TextPart(TextFormat.Html) { Text=body};

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("sandarhtwe.sdh16@gmail.com", "twgantpdeortakmi");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();

        }
    }
}
