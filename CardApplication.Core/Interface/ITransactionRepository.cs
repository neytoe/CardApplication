using CardApplication.Model;
using System.Threading.Tasks;

namespace CardApplication.Core.Interface
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetById(string id);
    }
}
