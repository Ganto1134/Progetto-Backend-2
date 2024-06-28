namespace Scarpe.WebApp.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public Article Article { get; set; }
    }
}
