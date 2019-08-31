using System.Collections.Generic;
using System.Linq;
using Api.Report;
using Db;
using Domain.Payment;
using LightBilling.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LightBilling.Services
{
    /// <inheritdoc />
    public class ReportService : IReportService
    {
        /// <inheritdoc />
        public List<PaymentReportDto> PaymentReport(PaymentReportRequest request)
        {
            using (var db = new ApplicationDbContext())
            {
                var dbResult = db.Payments.Include(x => x.Client)
                    .Where(x => x.DateTime >= request.From)
                    .Where(x => x.DateTime <= request.To);

                if (request.ClientId != null)
                {
                    dbResult = dbResult.Where(x => x.ClientId == request.ClientId);
                }

                if (request.PaymentType != null)
                {
                    dbResult = dbResult.Where(x => x.Type == (PaymentType) request.PaymentType);
                }

                return dbResult.Select(x => new PaymentReportDto
                    {
                        ClientId = x.ClientId,
                        FullName = x.Client.FullName,
                        DateTime = x.DateTime,
                        Amount = x.Amount,
                        Comment = x.Comment,
                        PaymentType = x.Type.ToString()
                    })
                    .ToList();
            }
        }
    }
}