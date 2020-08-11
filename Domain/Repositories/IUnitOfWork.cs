using System.Threading.Tasks;

namespace supermarketapi.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}