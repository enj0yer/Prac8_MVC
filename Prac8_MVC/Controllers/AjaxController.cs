using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prac8_MVC.Models;
using Prac8_MVC.Controllers;

namespace Prac8_MVC.Controllers
{
    public class AjaxController : Controller
    {
        StoreContext db = new StoreContext();

        private Product FindInBasket(int id)
        {
            List<Product> products;
            if (Session["Basket"] != null)
            {
                products = HomeController.ParseProducts(Session["Basket"].ToString(), db);
            }
            else
            {
                products = new List<Product>();
            }

            foreach(Product product in products)
            {
                if(product.Id == id)
                {
                    return product;
                }
            }
            return null;
        }

        [HttpPost]
        public JsonResult AddToBasket(int id, int number)
        {
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return Json(new AjaxResponse("FAIL", "Товар не найден"));

            }
            Product storedProduct = FindInBasket(id);
            if (product.Number < number + ((storedProduct != null) ? storedProduct.Number : 0))
            {
                return Json(new AjaxResponse("FAIL", "Выбрано слишком много товара"));
            }
            

            if (Session["Basket"] == null)
            {
                Session["Basket"] = product.Id + ";" + product.Description + ";" + product.Name + ";" + product.Price + ";" + number;
            }
            else
            {
                Session["Basket"] += "|" + product.Id + ";" + product.Description + ";" + product.Name + ";" + product.Price + ";" + number;
            }

            return Json(new AjaxResponse("SUCCESS", "Товар добавлен в корзину"));
        }

        [HttpPost]
        public JsonResult DeleteOrder(int id)
        {
            Order so = db.Orders.Find(id);
            if (so == null)
            {
                return Json(new AjaxResponse("FAIL", "Заказ не найден"));
            }
            else
            {
                db.Orders.Remove(so);
                db.SaveChanges();
                return Json(new AjaxResponse("SUCCESS", "Заказ удален"));
            }
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            Product so = db.Products.Find(id);
            if (so == null)
            {
                return Json(new AjaxResponse("FAIL", "Заказ не найден"));
            }
            else
            {
                db.Products.Remove(so);
                db.SaveChanges();
                return Json(new AjaxResponse("SUCCESS", "Заказ удален"));
            }
        }
    }
}