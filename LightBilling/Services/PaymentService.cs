using System;
using System.Threading.Tasks;
using Api.Payment;
using Domain.Payment;
using LightBilling.Interfaces;
using LightBilling.Repositories;

namespace LightBilling.Services
{
    /// <inheritdoc />
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _repository;
        private readonly IClientService _clientService;

        public PaymentService(PaymentRepository repository, IClientService clientService)
        {
            _repository = repository;
            _clientService = clientService;
        }

        /// <inheritdoc />
        public async Task<BalanceDto> AddPayment(PaymentAddDto request)
        {
            var payment = new Payment
            {
                ClientId = request.ClientId,
                Amount = request.Amount,
                Comment = request.Comment,
                DateTime = DateTime.Now,
                Type = PaymentType.User
            };

            await _repository.Add(payment);

            var payments = await _repository.GetByClientId(request.ClientId);

            var balance = 0.0;
            foreach (var pay in payments)
            {
                balance += pay.Amount;
            }

            return new BalanceDto
            {
                ClientId = request.ClientId,
                Balance = await _clientService.UpdateBalance(balance, request.ClientId)
            };
        }
    }
}