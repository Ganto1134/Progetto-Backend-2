namespace Scarpe.WebApp.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CoverPath { get; set; }
        public string Img1Path { get; set; }
        public string Img2Path { get; set; }
    }
}
