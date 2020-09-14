using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using GPUApp.Models;

namespace GPUApp.Controllers
{
    public class GPUController : Controller
    {

        private GPUDBEntities _db = new GPUDBEntities(); 
        
        
        public ActionResult Index()
        {
            return View(_db.GPUSet.ToList());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        

        public ActionResult Create()
        {
            return View();
        } 


 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude="Id")] GPU gpuToCreate)
        {
             if (!ModelState.IsValid)
                return View();

            _db.AddToGPUSet(gpuToCreate);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

       

        public ActionResult Edit(int id)
        {
            var gpuToEdit = (from m in _db.GPUSet
                               where m.Id == id
                               select m).First();

            return View(gpuToEdit);
        }

        

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(GPU gpuToEdit)
        {

            var originalGPU = (from m in _db.GPUSet
                                 where m.Id == gpuToEdit.Id
                                 select m).First();

            if (!ModelState.IsValid)
                return View(originalGPU);

                _db.ApplyPropertyChanges(originalGPU.EntityKey.EntitySetName, gpuToEdit);
                _db.SaveChanges();

                return RedirectToAction("Index");
        }

    
    }
}
