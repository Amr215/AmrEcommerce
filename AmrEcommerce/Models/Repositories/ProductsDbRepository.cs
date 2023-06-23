using AmrEcommerce.Models.Repositories;
using AmrEcommerce.Data;
using AmrEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AmrEcommerce.Models.Repositories
{
    public class ProductsDbRepository : IAmrEcommerceRepository<Products>
    {
        ApplicationDbContext db;

        public ProductsDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(Products entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var Products = Find(id);

            db.Products.Remove(Products);
            db.SaveChanges();
        }

        public Products Find(int id)
        {
            //var Products = db.Products.SingleOrDefault(a => a.Id == id);
            var Products = db.Products.Include(a=>a.SpecialTag).Include(a=>a.ProductTypes).SingleOrDefault(a => a.Id == id);
            return Products;
        }

        public IList<Products> List()
        {
            return db.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTag).ToList();
        }

        //public List<ProductTypes> Search(string term)
        //{
        //    return db.ProductTypes.Where(a => a.FullName.Contains(term)).ToList();
        //}

        public void Update(int id, Products Products)
        {
            db.Update(Products);
            db.SaveChanges();
        }
    }
}