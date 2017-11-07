using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUDsummerNote.Models;
using System.IO;

namespace CRUDsummerNote.Controllers
{
    public class NewsController : Controller
    {

        public static List<News> news = new List<News>(); 

        // GET: News
        public ActionResult Index()
        {
            return View(news.ToList());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(News ne)
        {

            if (ModelState.IsValid)
            {
                ne.Id =  news.Count()+1;
                news.Add(ne);
                return RedirectToAction("Index");
            }

            return View(ne);
        }

        public ActionResult Edit(int? id)
        {
            News ne = new News();
            
            ne.Id= news.ToList().Where(o => o.Id == id).FirstOrDefault().Id;
            ne.Tittle= news.ToList().Where(o => o.Id == id).FirstOrDefault().Tittle;
            ne.Content= news.ToList().Where(o => o.Id == id).FirstOrDefault().Content;

            return View(ne);
        }

        [HttpPost]
        public ActionResult Edit(News ne)
        {

            if (ModelState.IsValid)
            {
                news.Where(d => d.Id == ne.Id).First().Tittle = ne.Tittle;
                news.Where(d => d.Id == ne.Id).First().Content = ne.Content;
                return RedirectToAction("Index");
            }

            return View(ne);
        }


        public ActionResult Delete(int? Id)
        {
            News ne = new News();

            ne.Id = news.ToList().Where(o => o.Id == Id).FirstOrDefault().Id;
            ne.Tittle = news.ToList().Where(o => o.Id == Id).FirstOrDefault().Tittle;
            ne.Content = news.ToList().Where(o => o.Id == Id).FirstOrDefault().Content;

            return View(ne);
        }
        [HttpPost]
        public ActionResult Delete(News ne)
        {
            if (ModelState.IsValid)
            {
                var itemToRemove = news.Single(r => r.Id == ne.Id);
                news.Remove(itemToRemove);
                return RedirectToAction("Index");
            }
            return View(ne);
        }
    
        public ActionResult Detail(int? id)
        {
            News ne = new News();

            ne.Id = news.ToList().Where(o => o.Id == id).FirstOrDefault().Id;
            ne.Tittle = news.ToList().Where(o => o.Id == id).FirstOrDefault().Tittle;
            ne.Content = news.ToList().Where(o => o.Id == id).FirstOrDefault().Content;

            return View(ne);
        }

        [HttpPost]
        public ActionResult UpLoadImages(HttpPostedFileBase file)
        {
            if (file != null)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var files = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    string fileType = Path.GetExtension(file.FileName);

                    if (fileName != "")
                    {
                        string newName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() +
                                           DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
                                           DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + fileType;
                        //Ruta de Archivo
                        string Currentpath = Server.MapPath("/ImgUploads");

                        CreateDirectory ImgDr = new CreateDirectory();

                        ImgDr.CreatePath(Currentpath);
                        var path = Currentpath + "\\" + newName;
                        files.SaveAs(path);

                        string RelativePath = "ImgUploads" + "/" + newName;

                        Uri url = System.Web.HttpContext.Current.Request.Url;

                        int port = url.Port;


                        string retPath = "http://" + "localhost:" + port + "/" + RelativePath;

                        return Content(retPath);
                    }
                }
            }
            return View();
        }

    }
}


public class CreateDirectory
{
    public void CreatePath(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}