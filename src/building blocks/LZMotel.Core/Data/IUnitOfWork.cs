using System.Threading.Tasks;

namespace LZMotel.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}