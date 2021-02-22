using CardApplication.Model;
using System.Threading.Tasks;

namespace CardApplication.Core.Interface
{
    public interface ITransactionRepository
    {
        Task<Transactions> GetById(string id);
        Task<bool> Add(Transactions model);
    }
}
