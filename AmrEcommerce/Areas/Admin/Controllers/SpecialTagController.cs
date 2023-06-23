using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AmrEcommerce.Data;
using AmrEcommerce.Models;
using AmrEcommerce.Models.Repositories;

namespace AmrEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        //private ApplicationDbContext _db;
        private readonly IAmrEcommerceRepository<SpecialTag> SpecialTagDbRepository;

        public SpecialTagController(IAmrEcommerceRepository<SpecialTag> SpecialTagDbRepository)
        {
            this.SpecialTagDbRepository = SpecialTagDbRepository;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            var specialTags = SpecialTagDbRepository.List();
            return View(specialTags);
        }

        //GET Create Action Method

        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SpecialTagDbRepository.Add(specialTag);
                    TempData["save"] = "SpecialTag type has been saved";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        ////GET Edit Action Method

        public ActionResult Edit(int id)
        {

            var specialTag = SpecialTagDbRepository.Find(id);
            return View(specialTag);
        }

        ////POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SpecialTagDbRepository.Update(id, specialTag);
                    TempData["edit"] = "SpecialTag type has been updated";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            return View(specialTag);
        }

        //GET Details Action Method
        public ActionResult Details(int id)
        {
            var specialTag = SpecialTagDbRepository.Find(id);
            return View(specialTag);
        }

        ////POST Edit Action Method

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Details(ProductTypes productTypes)
        //{
        //    return RedirectToAction(nameof(Index));

        //}

        ////GET Delete Action Method

        public ActionResult Delete(int id)
        {
            var specialTag = SpecialTagDbRepository.Find(id);
            return View(specialTag);
        }

        ////POST Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SpecialTag specialTag)
        {

            try
            {
                SpecialTagDbRepository.Delete(id);
                TempData["delete"] = "SpecialTag type has been deleted";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

    }
}