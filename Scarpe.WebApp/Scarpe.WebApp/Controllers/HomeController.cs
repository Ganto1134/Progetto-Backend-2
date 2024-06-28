using Microsoft.AspNetCore.Mvc;
using Scarpe.WebApp.Entities;
using Scarpe.WebApp.Models;
using Scarpe.WebApp.Services;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Scarpe.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, ICommentService commentService)
        {
            _logger = logger;
            _articleService = articleService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            ViewData["DataAttuale"] = DateTime.Now;
            var articles = _articleService.GetAll().OrderByDescending(a => a.Id);
            return View(articles);
        }

        public IActionResult Write(){
            return View(new Article());
        }
        [HttpPost]
        public IActionResult Write(Article article){
            _articleService.Create(article);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Read(int id){
            var article = _articleService.GetById(id);
            var comments = _commentService.GetAll(id);
            ViewBag.Comments = comments;
            return View(article);
        }
        public IActionResult Comment(int id){
            var article = _articleService.GetById(id);
            ViewBag.Article = article.Title;
            ViewBag.Id = article.Id;
            return View(new Comment());
        }
        [HttpPost]
        public IActionResult Comment(int id, Comment comment){
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
