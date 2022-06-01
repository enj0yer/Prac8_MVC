using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Prac8_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string Products { get; set; }
        public int Price { get; set; }
    }
}