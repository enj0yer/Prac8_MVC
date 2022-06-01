using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prac8_MVC.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Pat { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}