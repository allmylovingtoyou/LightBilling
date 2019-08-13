using System.Threading.Tasks;
using Db;
using Domain.Base;
using LightBilling.Extensions;

namespace LightBilling.Repositories.Base
{
    public abstract class AbstractRepository<T> where T : class, IBaseEntity
    {

        public virtual async Task<T> Add(T entity)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = await db.Set<T>().AddAsync(entity);
                await db.SaveChangesAsync();

                return result.Entity;
            }
        }
        
        public async Task<T> ById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = await db.Set<T>().FindAsync(id);
                if (result == null)
                {
                    throw new InternalExceptions.NotFoundException(id.ToString());
                }

                return result;
            }
        }
    }
}