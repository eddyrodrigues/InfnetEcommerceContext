using MailKit.Net.Smtp;
using MassTransit;
using MessagingContracts;
using MimeKit;

namespace InfnetEcommerceContext.Email.API.Services;

public class SendEmailService
{
    public string EmailLogin { get; }
    public string EmailPassword { get; }

    public SendEmailService(IConfiguration options)
    {
        EmailLogin = options.GetValue<string>("email.login");
        EmailPassword = options.GetValue<string>("email.password");
    }

    public void SendEmail(SendEmailTemplate messageTemplate)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("SendEmailService", EmailLogin));
        message.To.Add(new MailboxAddress("", messageTemplate.to));
        message.Subject = messageTemplate.subject;

        var builder = new BodyBuilder();

        // add the body text
        builder.TextBody = messageTemplate.body;

        // add the attachment, if provided
        if (!string.IsNullOrEmpty(messageTemplate.attachmentPath))
        {
            var attachment = new MimePart("application", "octet-stream")
            {
                Content = new MimeContent(File.OpenRead(messageTemplate.attachmentPath), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(messageTemplate.attachmentPath)
            };

            builder.Attachments.Add(attachment);
        }

        message.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(EmailLogin, EmailPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }

}
