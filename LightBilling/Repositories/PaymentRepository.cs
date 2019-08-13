using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db;
using Domain.Client;
using Domain.Payment;
using LightBilling.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace LightBilling.Repositories
{
    public class PaymentRepository : AbstractRepository<Payment>
    {

        public async Task<List<Payment>> GetByClientId(int clientId)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Payments.AsQueryable()
                    .Where(x => x.ClientId == clientId)
                    .ToListAsync();
            }
        }
    }
}