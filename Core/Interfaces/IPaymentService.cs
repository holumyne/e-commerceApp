using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId); //for stripe webhook
        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);  //for stripe webhook
    }
}