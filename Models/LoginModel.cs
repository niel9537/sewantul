using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaceWeb.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class LoginModel
    {
        public string token { get; set; }
        public string status { get; set; }
    }

  
}