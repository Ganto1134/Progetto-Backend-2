using Scarpe.WebApp.Entities;

namespace Scarpe.WebApp.Services
{
    public interface ICrudService<T> where T : BaseEntity
    {
        void Create(T entity);
        void Delete(int entityId);
        T GetById(int entityId);
    }
}
