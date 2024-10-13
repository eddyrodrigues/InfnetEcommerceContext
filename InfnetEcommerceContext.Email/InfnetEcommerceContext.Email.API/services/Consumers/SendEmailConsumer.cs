using MassTransit;
using MessagingContracts;

namespace InfnetEcommerceContext.Email.API.Services.Consumers
{
    public class SendEmailConsumer: IConsumer<SendEmailTemplate>
    {
        private readonly SendEmailService sendEmailService;

        public SendEmailConsumer(SendEmailService sendEmailService)
        {
            this.sendEmailService = sendEmailService;
        }
        public Task Consume(ConsumeContext<SendEmailTemplate> context)
        {
            sendEmailService.SendEmail(context.Message);
            return Task.CompletedTask;
        }

    }
}
