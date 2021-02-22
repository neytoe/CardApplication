using CardApplication.Core.Interface;
using CardApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CardApplication.Core.Service
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _ctx;
        private readonly DbSet<T> entities;
        public GenericRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            entities = ctx.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            await entities.AddAsync(entity);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
