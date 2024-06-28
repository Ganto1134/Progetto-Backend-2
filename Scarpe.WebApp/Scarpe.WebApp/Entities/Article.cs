namespace Scarpe.WebApp.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Cover { get; set; }
        public IFormFile Img1 { get; set; }
        public IFormFile Img2 { get; set; }
    }
}
