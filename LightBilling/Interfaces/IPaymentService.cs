using System.Threading.Tasks;
using Api.Payment;

namespace LightBilling.Interfaces
{
    public interface IPaymentService
    {
        Task<BalanceDto> AddPayment(double amount, int clientId);
    }
}