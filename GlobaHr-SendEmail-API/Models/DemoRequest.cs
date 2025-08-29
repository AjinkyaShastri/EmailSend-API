using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobaHr_SendEmail_API.Models
{
    public class DemoRequest
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }
        public string NumberOfEmployees { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}