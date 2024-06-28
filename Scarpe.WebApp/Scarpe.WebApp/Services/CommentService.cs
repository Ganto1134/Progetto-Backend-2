using Scarpe.WebApp.Entities;

namespace Scarpe.WebApp.Services
{
    public class CommentService : CrudService<Comment>, ICommentService
    {
        public IEnumerable<Comment> GetAll(int entityId) =>
            entities.Where(e => e.Article.Id == entityId);
    }
}
