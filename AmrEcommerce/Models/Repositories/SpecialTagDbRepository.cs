using AmrEcommerce.Models.Repositories;
using AmrEcommerce.Data;
using AmrEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmrEcommerce.Models.Repositories
{
    public class SpecialTagDbRepository : IAmrEcommerceRepository<SpecialTag>
    {
        ApplicationDbContext db;

        public SpecialTagDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(SpecialTag entity)
        {
            db.SpecialTags.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var SpecialTag = Find(id);

            db.SpecialTags.Remove(SpecialTag);
            db.SaveChanges();
        }

        public SpecialTag Find(int id)
        {
            var SpecialTag = db.SpecialTags.SingleOrDefault(a => a.Id == id);

            return SpecialTag;
        }

        public IList<SpecialTag> List()
        {
            return db.SpecialTags.ToList();
        }

        //public List<ProductTypes> Search(string term)
        //{
        //    return db.ProductTypes.Where(a => a.FullName.Contains(term)).ToList();
        //}

        public void Update(int id, SpecialTag SpecialTag)
        {
            db.Update(SpecialTag);
            db.SaveChanges();
        }
    }
}