using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prac8_MVC.Models
{
    public class AjaxResponse
    {
        public string Status;
        public string Result;

        public AjaxResponse(string status, string result)
        {
            this.Status = status;
            this.Result = result;
        }
    }
}