using System.Threading.Tasks;

namespace CardApplication.Core.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Add(T entity);
        //Task<bool> Update(T entity);
    }
}
