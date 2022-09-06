using KnowledgeHub.Web.Models.Data;
using KnowledgeHub.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Humanizer;

namespace KnowledgeHub.Web.Controllers
{
    public class ArticlesController : Controller
    {
        KnowledgeHubDbContext db = new KnowledgeHubDbContext();
        public IActionResult Index()
        {
            List<Article> articles = null;
           
            if (User.IsInRole("admin"))
            {
                articles = (from c in db.Articles
                            where c.IsApproved == false
                            select c).ToList();
                return View(articles);
            }
             articles = db.Articles.ToList();
            return View(articles);
        }

        public IActionResult UserIndex(Article article)
        {
            List<Article> articles = null; 
            articles = (from c in db.Articles
                       where c.PostedBy == article.PostedBy
                       select c).ToList();
                       
            return View(articles);
            //return View();
        }
        [HttpGet]
        public IActionResult Approve(int id)
        {
            var article = db.Articles.Find(id);
            return View(article);
        }
        [HttpPost]
        public IActionResult Approve(Article article)
        {
            var res = db.Articles.Find(article.ArticleId);
            res.IsApproved = true;
            //res.IsApproved = article.IsApproved;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CatagoryId = from c in db.Catagories
                                 select new SelectListItem { Text = c.Name, Value = c.CatagoryId.ToString() };
            return View();
           // return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            article.DateSubmitted = DateTime.Now;
            article.PostedBy = User.Identity.Name;
            article.IsApproved = false;
            db.Articles.Add(article);
            db.SaveChanges();
            TempData["Message"] = $"Article {article.Title} Submited";
            return RedirectToAction("UserIndex");
        }
    }
}
