using KnowledgeHub.Web.Models.Data;
using KnowledgeHub.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace KnowledgeHub.Web.Controllers
{
    public class CatagoriesController : Controller
    {
        KnowledgeHubDbContext db = new KnowledgeHubDbContext();
        public IActionResult Index()
        {
            return View();
        }

        //for  create
        //Catagories/create
       
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Save(Catagory catagory)
        {
            
            //do Validation   
            if(!ModelState.IsValid)
                return View("Create");
 
            db.Add(catagory);
            db.SaveChanges();
            TempData["Message"] = $"Catagory {catagory.Name} Has created";
            
            //return View("Display", db.Catagories.ToList());
            return RedirectToAction("Display");
        }

        public IActionResult Display(String search)
        {

            //DataSet data = new DataSet();
            //var result = from c in db.Catagories
            //             select c;

            List<Catagory> catagories = null;

            if (search != null && search.Length != 0)
            {
                catagories = (from s in db.Catagories
                              where s.Name.Contains(search) || s.Description.Contains(search)
                              select s).ToList();
                TempData["Message"] = $"we have {catagories.Count()} catagory whic contains keyword {search}";
            }
            else
            {
                catagories = db.Catagories.ToList();
                TempData["Message"] = $"we have {catagories.Count()} catagory";
            }
            //ViewBag.CatagoryList = from c  in db.Catagories
            //                       select c;

            return View(catagories);

        }

        public IActionResult Delete(int id)
        {
            var res = db.Catagories.Find(id);
            return View(res);
        }
        public IActionResult Del(int id)
        {
            if (!ModelState.IsValid)
                return View("Create");

            var res = db.Catagories.Find(id);
            db.Catagories.Remove(res);
            db.SaveChanges();

            return RedirectToAction("Display");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Catagory result = db.Catagories.Find(id);
            return View(result) ;
        }

        [HttpPost]
        public IActionResult Edit(Catagory catagory)
        {
            if (!ModelState.IsValid)
                return View("Edit");

            //var UpdateCatagory = db.Catagories.Find(catagory.CatagoryId);
            //UpdateCatagory.Name = catagory.Name;
            //UpdateCatagory.Description = catagory.Description;   //wrong aproach

            db.Entry(catagory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            
            db.SaveChanges();
            TempData["Message"] = $"Catagory {catagory.Name} Has Updated";

            return RedirectToAction("Display");
        }

        public IActionResult Search()
        {
            
            return View();
        }


    }
}
