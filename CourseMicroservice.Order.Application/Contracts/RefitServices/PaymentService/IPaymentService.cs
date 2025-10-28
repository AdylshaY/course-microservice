namespace CourseMicroservice.Order.Application.Contracts.RefitServices.PaymentService
{
    using Refit;
    using System.Threading.Tasks;

    public interface IPaymentService
    {
        [Post("/api/v1/payments")]
        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest paymentRequest);

        [Get("/api/v1/payments/status/{orderCode}")]
        Task<GetPaymentStatusResponse> GetStatusAsync(string orderCode);
    }
}
