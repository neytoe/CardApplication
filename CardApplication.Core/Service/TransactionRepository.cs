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

        public async Task<Transaction> GetById(string id)
        {
            return await _context.Transactions.Include(x => x.Payment).FirstOrDefaultAsync(x => x.PaymentId == id);
        }
    }
}
