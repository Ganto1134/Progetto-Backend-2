using Scarpe.WebApp.Entities;

namespace Scarpe.WebApp.Services
{
    public interface IArticleService : ICrudService<Article>
    {
        IEnumerable<Article> GetAll();
    }
}
