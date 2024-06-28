using Microsoft.AspNetCore.Mvc;
using Scarpe.WebApp.Entities;
using Scarpe.WebApp.Models;
using Scarpe.WebApp.Services;
using System.Diagnostics;
using System.IO;

namespace Scarpe.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, ICommentService commentService, IWebHostEnvironment env)
        {
            _logger = logger;
            _articleService = articleService;
            _commentService = commentService;
            _env = env;
        }

        public IActionResult Index()
        {
            ViewData["DataAttuale"] = DateTime.Now;
            var articles = _articleService.GetAll().OrderByDescending(a => a.Id);
            return View(articles);
        }

        public IActionResult Write()
        {
            return View(new Article()); // stavo seguendo il video di ieri e dopo mi sono riscordato che il write normale non lo avrei più usato
        }                               // ma l'ho tenuto per una pagina dove si possono mettere progetti furuti che non hanno ancora foto 

        public IActionResult WriteArticle()
        {
            return View(new ArticleInputModel());
        }

        [HttpPost]
        public IActionResult WriteArticle(ArticleInputModel article) // creazione della card con tutte le info richieste 
        {
            if (ModelState.IsValid)
            {
                var a = new Article
                {
                    Title = article.Title,
                    Description = article.Description,
                    Price = article.Price
                };
                _articleService.Create(a);

                string uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                if (article.Cover != null && article.Cover.Length > 0)
                {
                    string coverFileName = $"{a.Id}_cover.jpg";
                    string coverPath = Path.Combine(uploadsFolderPath, coverFileName);
                    using (var stream = new FileStream(coverPath, FileMode.Create))
                    {
                        article.Cover.CopyTo(stream);
                    }
                    a.CoverPath = Path.Combine("uploads", coverFileName);
                }

                if (article.Img1 != null && article.Img1.Length > 0)
                {
                    string img1FileName = $"{a.Id}_img1.jpg";
                    string img1Path = Path.Combine(uploadsFolderPath, img1FileName);
                    using (var stream = new FileStream(img1Path, FileMode.Create))
                    {
                        article.Img1.CopyTo(stream);
                    }
                    a.Img1Path = Path.Combine("uploads", img1FileName);
                }

                if (article.Img2 != null && article.Img2.Length > 0)
                {
                    string img2FileName = $"{a.Id}_img2.jpg";
                    string img2Path = Path.Combine(uploadsFolderPath, img2FileName);
                    using (var stream = new FileStream(img2Path, FileMode.Create))
                    {
                        article.Img2.CopyTo(stream);
                    }
                    a.Img2Path = Path.Combine("uploads", img2FileName);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        [HttpPost]
        public IActionResult Write(Article article)
        {
            _articleService.Create(article);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Read(int id) // pagina read dove si vedono tutte le info non come in index
        {
            var article = _articleService.GetById(id);
            var comments = _commentService.GetAll(id);
            ViewBag.Comments = comments;
            string uploads = Path.Combine(_env.WebRootPath, "uploads");
            var cover = Path.Combine(uploads, $"{article.Id}_cover.jpg");
            if (System.IO.File.Exists(cover))
                ViewBag.Cover = $"/uploads/{article.Id}_cover.jpg";
            var img1 = Path.Combine(uploads, $"{article.Id}_img1.jpg");
            if (System.IO.File.Exists(img1))
                ViewBag.Img1 = $"/uploads/{article.Id}_img1.jpg";
            var img2 = Path.Combine(uploads, $"{article.Id}_img2.jpg");
            if (System.IO.File.Exists(img2))
                ViewBag.Img2 = $"/uploads/{article.Id}_img2.jpg";

            return View(article);
        }

        public IActionResult Comment(int id) // commenti sempre perchè stavo seguendo il video
        {
            var article = _articleService.GetById(id);
            ViewBag.Article = article.Title;
            ViewBag.Id = article.Id;
            return View(new Comment());
        }

        [HttpPost]
        public IActionResult Comment(int id, Comment comment)
        {
            var article = _articleService.GetById(id);
            comment.Article = article;
            _commentService.Create(comment);
            return RedirectToAction(nameof(Read), new { id = id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}