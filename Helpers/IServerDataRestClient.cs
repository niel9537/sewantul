using PaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceWeb.Helpers
{
    public interface IServerDataRestClient
    {
        //Get Wisata Landing
        List<Wisata> GetWisata2();
        //Get List Users
        List<User> GetUsers(string token);
        //Get List Evakuasi
        List<Evakuasi> GetEvakuasi(string token);
        //Add Tempat Evakuasi
        string SaveEvakuasi(string token, string nama, string alamat, string longitude, string lattitude, string foto, string keterangan);
        //Update Tempat Evakuasi
        string UpdateEvakuasi(string token, string id, string nama, string alamat, string longitude, string lattitude, string foto, string keterangan);
        //Delete Evakuasi
        string DeleteEvakuasi(string token, string id);
        //Get List Wisata
        List<Wisata> GetWisata(string token);
        //Add Tempat Wisata
        string SaveWisata(string token, string nama, string alamat, string longitude, string lattitude, string foto, string keterangan, string jumlah_pengunjung);
        //Update Tempat Wisata
        string UpdateWisata(string token, string id, string nama, string alamat, string longitude, string lattitude, string foto, string keterangan, string jumlah_pengunjung);
        //Delete Wisata
        string DeleteWisata(string token, string id);
        
        //Sign In 
        string SignIn(string username, string password);
        //Save Users
        string SaveUsers(string token, string username, string password, string nama, string email, string alamat, string notelp, string jeniskelamin);
        //Update Users
        string UpdateUsers(string token, string id, string username, string password, string nama, string email, string alamat, string notelp, string jeniskelamin);
        //Delete Users 
        string DeleteUsers(string token,string id);
    }
}
