using CourseMicroservice.Bus.Events;
using CourseMicroservice.Order.Application.Contracts.RefitServices.PaymentService;
using CourseMicroservice.Order.Application.Contracts.Repositories;
using CourseMicroservice.Order.Application.Contracts.UnitOfWork;
using CourseMicroservice.Order.Domain.Entities;
using CourseMicroservice.Shared;
using CourseMicroservice.Shared.Services;
using MassTransit;
using MediatR;
using System.Net;

namespace CourseMicroservice.Order.Application.Features.Orders.Create;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IIdentityService identityService, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint, IPaymentService paymentService) : IRequestHandler<CreateOrderCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (request.Items.Count == 0) return ServiceResult.Error("Order items not found.", "Order items cannot be empty. Please add at least one item to the order.", HttpStatusCode.BadRequest);

        var address = new Address()
        {
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };

        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.UserId, request.DiscountRate, address.Id);

        foreach (var orderItem in request.Items)
        {
            order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
        }

        order.Address = address;

        orderRepository.Add(order);
        await unitOfWork.CommitAsync(cancellationToken);

        var createPaymentRequest = new CreatePaymentRequest(order.Code, request.Payment.CardNumber, request.Payment.CardHolderName, request.Payment.Expiration, request.Payment.Cvc, request.Payment.Amount);
        var paymentResponse = await paymentService.CreateAsync(createPaymentRequest);

        if (!paymentResponse.Status) return ServiceResult.Error(paymentResponse.ErrorMessage!, HttpStatusCode.InternalServerError);

        order.SetPaidStatus(paymentResponse.PaymentId!.Value);

        orderRepository.Update(order);
        await unitOfWork.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(new OrderCreatedEvent(order.Id, identityService.UserId), cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
