using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaceWeb.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class WisataModel
    {
        public List<Wisata> Wisata { get; set; }
        public string status { get; set; }
    }

    public class Wisata
    {
        public string id { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string longitude { get; set; }
        public string lattitude { get; set; }
        public string foto { get; set; }
        public string keterangan { get; set; }
        public string jumlah_pengunjung { get; set; }
        public string status { get; set; }
    }
}