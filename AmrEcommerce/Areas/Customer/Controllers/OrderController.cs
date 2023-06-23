using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AmrEcommerce.Data;
using AmrEcommerce.Models;
using AmrEcommerce.Utility;

namespace AmrEcommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
      
        //GET Checkout actioin method

        public ActionResult Checkout()
        {
            return View();
        }

        //POST Checkout action method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Checkout(Order anOrder)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products!=null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails=new OrderDetails();
                    orderDetails.PorductId = product.Id;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }

            anOrder.OrderNo = GetOrderNo();
            _db.Orders.Add(anOrder);
             await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }


        public string GetOrderNo()
        {
            int rowCount = _db.Orders.ToList().Count()+1;
            return rowCount.ToString("000");
        }
    }
}
