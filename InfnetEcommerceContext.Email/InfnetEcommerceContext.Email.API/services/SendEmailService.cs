using MailKit.Net.Smtp;
using MimeKit;

namespace InfnetEcommerceContext.Notification.API.services
{
    public class SendEmailService
    {
        public void SendEmail(string to, string subject, string body, string attachmentPath)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Name", "youremail@gmail.com"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var builder = new BodyBuilder();

            // add the body text
            builder.TextBody = body;

            // add the attachment, if provided
            if (!string.IsNullOrEmpty(attachmentPath))
            {
                var attachment = new MimePart("application", "octet-stream")
                {
                    Content = new MimeContent(File.OpenRead(attachmentPath), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(attachmentPath)
                };

                builder.Attachments.Add(attachment);
            }

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("youremail@gmail.com", "yourpassword");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
