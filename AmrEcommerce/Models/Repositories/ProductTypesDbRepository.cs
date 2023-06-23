using AmrEcommerce.Models.Repositories;
using AmrEcommerce.Data;
using AmrEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmrEcommerce.Models.Repositories
{
    public class ProductTypesDbRepository : IAmrEcommerceRepository<ProductTypes>
    {
        ApplicationDbContext db;

        public ProductTypesDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(ProductTypes entity)
        {
            db.ProductTypes.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var ProductTypes = Find(id);

            db.ProductTypes.Remove(ProductTypes);
            db.SaveChanges();
        }

        public ProductTypes Find(int id)
        {
            var ProductTypes = db.ProductTypes.SingleOrDefault(a => a.Id == id);

            return ProductTypes;
        }

        public IList<ProductTypes> List()
        {
            return db.ProductTypes.ToList();
        }

        //public List<ProductTypes> Search(string term)
        //{
        //    return db.ProductTypes.Where(a => a.FullName.Contains(term)).ToList();
        //}

        public void Update(int id, ProductTypes ProductTypes)
        {
            db.Update(ProductTypes);
            db.SaveChanges();
        }
    }
}