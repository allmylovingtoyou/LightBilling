using System;

namespace Api.Report
{
    public class PaymentReportDto
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public DateTime DateTime { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }

        public string PaymentType { get; set; }
    }
}