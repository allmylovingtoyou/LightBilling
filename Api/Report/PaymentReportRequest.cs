using System;

namespace Api.Report
{
    public class PaymentReportRequest
    {
        public DateTime From { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime To { get; set; } = DateTime.Now;
        public int? ClientId { get; set; }
        public PaymentTypeDto? PaymentType { get; set; }
    }
}