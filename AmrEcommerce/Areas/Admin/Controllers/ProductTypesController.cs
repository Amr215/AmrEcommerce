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
    //[Authorize(Roles = "Super user")]
    public class ProductTypesController : Controller
    {
        //private ApplicationDbContext _db;
        private readonly IAmrEcommerceRepository<ProductTypes> ProductTypesRepository;

        public ProductTypesController(IAmrEcommerceRepository<ProductTypes> ProductTypesRepository)
        {
            this.ProductTypesRepository = ProductTypesRepository;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            var ProductTypes = ProductTypesRepository.List();
            return View(ProductTypes);
        }

        //GET Create Action Method

        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductTypesRepository.Add(productTypes);
                    TempData["save"] = "Product type has been saved";
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

            var productType = ProductTypesRepository.Find(id);
            return View(productType);
        }

        ////POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductTypesRepository.Update(id , productTypes);
                    TempData["edit"] = "Product type has been updated";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            return View(productTypes);
        }

        //GET Details Action Method
        public ActionResult Details(int id)
        {
            var productType = ProductTypesRepository.Find(id);
            return View(productType);
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
            var productType = ProductTypesRepository.Find(id);
            return View(productType);
        }

        ////POST Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductTypes productTypes)
        {
           
                try
                {
                    ProductTypesRepository.Delete(id);
                    TempData["delete"] = "Product type has been deleted";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            
        }

    }
}