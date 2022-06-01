using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prac8_MVC.Models
{
    public class OrderWithCustomer
    {
        public int Id { get; set; }
        public string Products { get; set; }
        public int Price { get; set; }
        public Customer Customer { get; set; }

        public OrderWithCustomer(int id, string Products, int price, Customer customer)
        {
            this.Id = id;
            this.Products = Products;
            this.Price = price;
            this.Customer = customer;
        }
    }
}