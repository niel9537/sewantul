using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaceWeb.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class UserModel
    {
        public List<User> users { get; set; }
        public string status { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string nama { get; set; }
        public string email { get; set; }
        public string alamat { get; set; }
        public string notelp { get; set; }
        public string jeniskelamin { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string status { get; set; }
    }
}