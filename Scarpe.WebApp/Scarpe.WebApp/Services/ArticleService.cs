using Scarpe.WebApp.Entities;

namespace Scarpe.WebApp.Services
{
    public class ArticleService : CrudService<Article>, IArticleService
    {
        public IEnumerable<Article> GetAll() => entities;
    }
}
