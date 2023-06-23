using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AmrEcommerce.Data;
using AmrEcommerce.Models;
using AmrEcommerce.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AmrEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        //private ApplicationDbContext _db;
        private readonly IAmrEcommerceRepository<Products> ProductsRepository;
        private IHostingEnvironment _he;
        private ApplicationDbContext _db;
        public ProductsController(IAmrEcommerceRepository<Products> ProductsRepository, IHostingEnvironment he, ApplicationDbContext db)
        {
            this.ProductsRepository = ProductsRepository;
            _he = he;
            _db = db;
        }
        public ActionResult Index()
        {
            var Products = ProductsRepository.List();
            return View(Products);
        }

        //POST Index action method
        [HttpPost]
        public ActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag)
                 .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                products = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList();
            }
            return View(products);
        }
        //GET Create Action Method

        public ActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.Products.FirstOrDefault(c => c.Name == product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return View(product);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                 ProductsRepository.Add(product);
                TempData["save"] = "Product has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        ////GET Edit Action Method

        public ActionResult Edit(int? id)
        {

            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        ////POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "Images/noimage.PNG";
                }
                ProductsRepository.Update(id, products);
                TempData["edit"] = "Product has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(products);
        }

        //GET Details Action Method
        public ActionResult Details(int id)
        {
            var productType = ProductsRepository.Find(id);
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.SpecialTag).Include(c => c.ProductTypes).Where(c => c.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        ////POST Delete Action Method

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ProductsRepository.Delete(product.Id);
            TempData["edit"] = "Product  has been deleted";
            return RedirectToAction(nameof(Index));

        }

    }
}