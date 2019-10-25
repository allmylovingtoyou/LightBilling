using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Payment;
using Db;
using Domain.Client;
using Domain.Payment;
using LightBilling.Interfaces;
using LightBilling.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LightBilling.Services
{
    /// <inheritdoc />
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly PaymentRepository _repository;
        private readonly IClientService _clientService;

        public PaymentService(ILogger<PaymentService> logger, PaymentRepository repository, IClientService clientService)
        {
            _logger = logger;
            _repository = repository;
            _clientService = clientService;
        }

        /// <inheritdoc />
        public async Task<BalanceDto> AddPayment(PaymentAddDto request, PaymentType type = PaymentType.User)
        {
            var payment = new Payment
            {
                ClientId = request.ClientId,
                Amount = request.Amount,
                Comment = request.Comment,
                DateTime = DateTime.Now,
                Type = type
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

        public async Task<List<BalanceDto>> WithdrawMonthlyFee()
        {
            using (var db = new ApplicationDbContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var balances = await WithdrawMonthlyFee(db);
                    transaction.Commit();
                    return balances;
                }
                catch (Exception exception)
                {
                    _logger.LogError($"{nameof(WithdrawMonthlyFee)}", exception);
                    throw;
                }
            }
        }

        private async Task<List<BalanceDto>> WithdrawMonthlyFee(ApplicationDbContext db)
        {
            var dbResult = db.Clients
                .Include(x => x.JoinTariffs)
                .Where(x => !x.IsDeleted)
                .Where(x => x.IsActive);

            var balances = new List<BalanceDto>();
            foreach (var client in dbResult)
            {
                balances.AddRange(await Task.WhenAll(client.JoinTariffs
                    .Where(x => x.Tariff.IsPeriodic)
                    .Select(x => x.Tariff)
                    .Select(x => x.Cost)
                    .Select(async cost => await AddPayment(client, cost))));
            }

            return balances;
        }

        private async Task<BalanceDto> AddPayment(Client client, double cost)
        {
            var payment = new PaymentAddDto
            {
                ClientId = client.Id,
                Amount = -cost,
                Comment = "mountly fee"
            };
            return await AddPayment(payment, PaymentType.System);
        }
    }
}