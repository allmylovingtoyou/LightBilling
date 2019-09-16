using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Payment;
using Domain.Payment;

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
        /// <param name="type"></param>
        /// <returns>Current user balance</returns>
        Task<BalanceDto> AddPayment(PaymentAddDto request, PaymentType type = PaymentType.User);

        /// <summary>
        /// Списать месячную плату по периодическим услугам
        /// </summary>
        /// <returns></returns>
        Task<List<BalanceDto>> WithdrawMonthlyFee();
    }
}