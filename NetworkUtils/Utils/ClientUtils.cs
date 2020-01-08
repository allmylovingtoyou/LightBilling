using System.Collections.Generic;
using System.Linq;
using Api.Client.Status;
using Db;
using Domain.Client;
using LightBilling.Mapping;
using Microsoft.EntityFrameworkCore;

namespace NetworkUtils.Utils
{
    public class ClientUtils
    {
        public static List<Client> GetActiveClients(ApplicationDbContext db)
        {
            var clients = db.Clients
                .Include(c => c.GreyAddress)
                .ThenInclude(g => g.White)
                .Include(x => x.JoinTariffs)
                .ThenInclude(jt => jt.Tariff)
                .Where(c => !c.IsDeleted)
                .Where(c => c.GreyAddress != null)
                .Where(c => c.GreyAddress.White != null)
                .ToList();

            var activeClients = clients
                .Where(client => ClientMapper.CalculateClientStatus(client) == ClientStatus.Active)
                .ToList();
            return activeClients;
        }
    }
}