using System;
using System.Threading.Tasks;
using Api.Client;
using Api.Payment;
using Domain.Payment;
using LightBilling.Interfaces;
using LightBilling.Repositories;

namespace LightBilling.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _repository;
        private readonly IClientService _clientService;

        public PaymentService(PaymentRepository repository, IClientService clientService)
        {
            _repository = repository;
            _clientService = clientService;
        }

        public async Task<BalanceDto> AddPayment(double amount, int clientId)
        {
            var payment = new Payment
            {
                ClientId = clientId,
                Amount = amount,
                DateTime = DateTime.Now
            };

            await _repository.Add(payment);

            var payments = await _repository.GetByClientId(clientId);

            var balance = 0.0;
            foreach (var pay in payments)
            {
                balance += pay.Amount;
            }

            return new BalanceDto
            {
                ClientId = clientId,
                Balance = await _clientService.UpdateBalance(balance, clientId)
            };
        }
    }
}