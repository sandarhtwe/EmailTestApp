using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;

namespace EmailTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfGeneratorController : ControllerBase
    {
        [HttpPost]
        public IActionResult GeneratePdfAndSendEmail([FromBody] string email)
        {
            try
            {
                
                SendEmailWithAttachment(email, "Product List Report");

                return Ok("PDF generated and sent to the email.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred: " + ex.Message);
            }
        }


        private void SendEmailWithAttachment(string recipientEmail, string subject)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sandar Htwe", "sandarhtwe.sdh16@gmail.com"));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = subject;

            // Adding the mail body using a TextPart
            var textPart = new TextPart("plain")
            {
                Text = "Dear Sir,\r\n\r\nThis is the report of Product List. Please kindly check it. Thanks Sir.\r\n\r\nBest Regards,\r\n\r\nSandar"
            };

            // Adding the attachment using a BodyBuilder
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.Attachments.Add("ProductList.pdf");

            // Set the text body and attachment body to the MimeMessage
            var multipart = new Multipart("mixed");
            multipart.Add(textPart);
            multipart.Add(bodyBuilder.ToMessageBody());
            message.Body = multipart;

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // Replace with your SMTP server address and port number
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Provide your authentication credentials 
                client.Authenticate("sandarhtwe.sdh16@gmail.com", "twgantpdeortakmi");

                // Send the email
                client.Send(message);

                // Disconnect from the SMTP server
                client.Disconnect(true);
            }
        }
    }
}
