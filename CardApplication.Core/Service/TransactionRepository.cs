using CardApplication.Core.Interface;
using CardApplication.Data;
using CardApplication.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CardApplication.Core.Service
{
    public class TransactionRepository : ITransactionRepository
    {
        private AppDbContext _context { get; set; }
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transactions> GetById(string id)
        {
            return await _context.Transactions.Include(x => x.Payment).FirstOrDefaultAsync(x => x.PaymentId == id);
        }

        public async Task<bool> Add(Transactions model)
        {
            await _context.Transactions.AddAsync(model);
            return _context.SaveChanges() > 0;
        }
    }
}
