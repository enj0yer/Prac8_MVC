using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prac8_MVC.Models;

namespace Prac8_MVC.Controllers
{
    public class HomeController : Controller
    {
        StoreContext db = new StoreContext();

        [NonAction]
        public static List<Product> ParseProducts(string strProducts, StoreContext db)
        {
            string[] parsedProducts = strProducts.Split('|');
            List<Product> products = new List<Product>();

            

            foreach (string p in parsedProducts)
            {
                Product product = new Product();
                string[] parsedProduct = p.Split(';');

                if (db.Products.Find(Int32.Parse(parsedProduct[0])) == null){
                    continue;
                }

                product = products.Find(x => x.Name == parsedProduct[2]);

                if (product != null)
                {
                    product.Number += Int32.Parse(parsedProduct[4]);
                    product.Price += Int32.Parse(parsedProduct[3]) * Int32.Parse(parsedProduct[4]);
                    continue;
                }

                product = new Product();

                product.Id = Int32.Parse(parsedProduct[0]);
                product.Description = parsedProduct[1];
                product.Name = parsedProduct[2];
                product.Price = Int32.Parse(parsedProduct[3]) * Int32.Parse(parsedProduct[4]);
                product.Number = Int32.Parse(parsedProduct[4]);

                products.Add(product);
            }

            return products;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CheckProducts()
        {
            return View(db.Products);
        }

        [HttpGet]
        public ActionResult CheckBasket()
        {
            List<Product> products = new List<Product>();
            
            if (Session["Basket"] != null)
            {
                products = ParseProducts(Session["Basket"].ToString(), db);

                int price = 0;

                foreach (var product in products)
                {
                    price += product.Price;
                }

                Session["Price"] = price;
            }

            return View(products);
        }

        [HttpGet]
        public ActionResult ClearBasket()
        {
            Session["Basket"] = null;

            return Redirect("/Home/CheckProducts");
        }

        [HttpGet]
        public ActionResult Purchase()
        {
            return View(new Customer());
        }

        [HttpPost]
        public ActionResult Purchase(Customer customer)
        {
            if (Session["Basket"] == null)
            {
                return Redirect("/Home/CheckProducts");
            }
            else
            {
                Customer sc = db.Customers.FirstOrDefault(c => c.Surname == customer.Surname &&
                                                           c.Name == customer.Name &&
                                                           c.Pat == customer.Pat &&
                                                           c.Email == customer.Email);

                if (sc == null)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    sc = customer;
                }

                Order order = new Order();

                order.CustomerId = sc.Id;
                order.Price = (int)Session["Price"];
                List<Product> products = ParseProducts(Session["Basket"].ToString(), db);

                foreach(var product in products)
                {
                    if (product.Number > db.Products.Find(product.Id).Number)
                    {
                        return Redirect("/Home/OrderError");
                    }
                }

                foreach (var product in products)
                {
                    order.Products += product.Name + " - " + product.Number + " шт." + '\\';
                }
                db.Orders.Add(order);

                foreach (var product in products)
                {
                    db.Products.Find(product.Id).Number -= product.Number;
                }

                db.SaveChanges();

                Session["Basket"] = null;
                Session["Price"] = null;

                return Redirect("/Home/Thanks");
            }
            
        }

        [HttpGet]
        public ActionResult Thanks()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OrderError()
        {
            return View();
        }
    }
}