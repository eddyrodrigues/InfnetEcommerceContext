using InfnetEcommerceContext.Notification.API.Models;
using InfnetEcommerceContext.Notification.API.services;

namespace InfnetEcommerceContext.Notification.API.BackgroundServices
{

    public class NotifyUserEmailListener : NotificationMqListener<NotifyUserEmail>
    {
        public NotifyUserEmailListener(IServiceProvider services, IConfiguration configuration)
            : base("NotifyUserEmailListener", services, configuration) { }

        public override void ProcessQueue(NotifyUserEmail notifyUser)
        {
            using (var scope = _services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<SendEmailService>();

                scopedProcessingService.SendEmail(notifyUser.EmailTo, notifyUser.Subject, notifyUser.Body, null);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
