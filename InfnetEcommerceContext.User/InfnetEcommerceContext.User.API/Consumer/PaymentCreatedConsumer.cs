using InfnetEcommerceContext.User.API.Repository.Repositories;
using MassTransit;
using MessagingContracts;

namespace InfnetEcommerceContext.Email.API.Services.Consumers;

public class PaymentCreatedConsumer: IConsumer<PaymentCreated>
{
    private readonly IUserRepository userRepository;

    public PaymentCreatedConsumer(IUserRepository userRepository, IBus bus)
    {
        this.userRepository = userRepository;
        Bus = bus;
    }

    public IBus Bus { get; }

    public Task Consume(ConsumeContext<PaymentCreated> context)
    {
        
        var user = userRepository.GetById(context.Message.UserId);

        if (user == null)
        {
            return Task.CompletedTask;
        }

        var messageForUser = $"Payment created on our system. As soon as possible we'll return with Updates. Order Id: " + context.Message.OrderId;

        Bus.Publish(new SendEmailTemplate()
        {
            to = user.Email,
            body = messageForUser,
            subject = "Payment created inside our system"
        });
        
        return Task.CompletedTask;
    }
}
