using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobaHr_SendEmail_API.Models
{
    public class CompanyDetails
    {
        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string Website { get; set; }
        public string CompanyMailAddress { get; set; }
        public List<string> BCCMailAddress { get; set; }
    }
}