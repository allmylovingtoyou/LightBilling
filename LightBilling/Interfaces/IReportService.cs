using System.Collections.Generic;
using Api.Report;

namespace LightBilling.Interfaces
{
    /// <summary>
    /// Service to create reports
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Payment report for time range with client and payment type filtering.
        /// </summary>
        /// <param name="request">PaymentReportRequest</param>
        /// <returns>List PaymentReportDto</returns>
        List<PaymentReportDto> PaymentReport(PaymentReportRequest request);
    }
}