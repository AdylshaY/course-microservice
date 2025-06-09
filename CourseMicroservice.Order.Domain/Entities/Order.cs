using MassTransit;
using System.Text;

namespace CourseMicroservice.Order.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string Code { get; set; } = null!;
        public DateTime Created { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid PaymentId { get; set; }
        public List<OrderItem> OrderItemList { get; set; } = [];
        public float? DiscountRate { get; set; }
        public Address Address { get; set; } = null!;

        public static string GenerateCode()
        {
            var random = new Random();
            var orderCode = new StringBuilder(10);

            for(int i = 0; i < 10; i++)
            {
                orderCode.Append(random.Next(0, 10));
            }

            return orderCode.ToString();
        }

        public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate, int addressId)
        {
            return new Order()
            {
                Id = NewId.NextSequentialGuid(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                Created = DateTime.Now,
                AddressId = addressId,
                Status = OrderStatus.WaitingForPayment,
                TotalPrice = 0,
                DiscountRate = discountRate,
            };
        }

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
        {
            var orderItem = new OrderItem();
            orderItem.SetItem(productId, productName, unitPrice);
            OrderItemList.Add(orderItem);
            CalculateTotalPrice();
        }

        public void ApplyDiscount(float discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 100.");
            }
            DiscountRate = discountPercentage;
            CalculateTotalPrice();
        }

        public void SetPaidStatus(Guid paymentId)
        {
            if (Status != OrderStatus.WaitingForPayment)
            {
                throw new InvalidOperationException("Order is not in a state that allows payment.");
            }
            Status = OrderStatus.Paid;
            PaymentId = paymentId;
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItemList.Sum(item => item.UnitPrice);
            if (DiscountRate.HasValue)
            {
                TotalPrice -= TotalPrice * (decimal)(DiscountRate.Value / 100);
            }
        }
    }

    public enum OrderStatus
    {
        WaitingForPayment = 1,
        Paid = 2,
        Cancel = 3
    }
}
