using Scarpe.WebApp.Entities;

namespace Scarpe.WebApp.Services
{
    public interface ICommentService : ICrudService<Comment>
    {
        IEnumerable<Comment> GetAll(int articleId);
    }
}
