using System.Threading.Tasks;
using Api.Payment;

namespace LightBilling.Interfaces
{
    /// <summary>
    /// Service to add payment to clients
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Add payment to client
        /// </summary>
        /// <param name="request">PaymentAddDto</param>
        /// <returns>Current user balance</returns>
        Task<BalanceDto> AddPayment(PaymentAddDto request);
    }
}