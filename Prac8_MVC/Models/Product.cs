using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prac8_MVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Number { get; set; }
    }
}