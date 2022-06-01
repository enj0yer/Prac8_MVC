using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prac8_MVC.Models;

namespace Prac8_MVC.Controllers
{
    public class AdminController : Controller
    {

        Dictionary<string, string> admins = new Dictionary<string, string>() { { "admin", "admin"} };


        StoreContext db = new StoreContext();
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Enter()
        {
            if (Session["IsAdmin"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Enter(string login, string password)
        {
            string adminPassword = "";
            if (admins.TryGetValue(login, out adminPassword) && adminPassword.Equals(password))
            {
                Session["IsAdmin"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Error", "Неверый логин или пароль");
                return View();
            }
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View(new Product());
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            Product sp = db.Products.FirstOrDefault(p => p.Name == product.Name);

            if (sp == null)
            {
                db.Products.Add(product);
                db.SaveChanges();

                return Redirect("/Home/CheckProducts");
            }
            else
            {

                db.Products.Find(sp.Id).Description = product.Description;
                db.Products.Find(sp.Id).Price = product.Price;
                db.Products.Find(sp.Id).Number = product.Number;

                db.SaveChanges();

                return Redirect("/Home/CheckProducts");
            }
        }

        [HttpGet]
        public ActionResult CheckOrders()
        {
            List<OrderWithCustomer> orders = new List<OrderWithCustomer>();

            List<Order> dbOrders = db.Orders.ToList();

            foreach (var order in dbOrders)
            {
                orders.Add(new OrderWithCustomer(order.Id, order.Products, order.Price, db.Customers.Find(order.CustomerId)));
            }
            return View(orders.ToArray());
        }

        public ActionResult Exit()
        {
            Session["IsAdmin"] = null;

            return Redirect("/Home/Index");
        }

    }
}