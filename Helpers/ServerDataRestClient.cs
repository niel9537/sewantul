using Newtonsoft.Json;
using PaceWeb.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PaceWeb.Helpers
{
    public class ServerDataRestClient : IServerDataRestClient
    {
        public readonly RestClient _client;
        private readonly string _url = ConfigurationManager.AppSettings["baseURL"];
        string baseURL = ConfigurationManager.AppSettings["baseURL"];
        public ServerDataRestClient()
        {
            _client = new RestClient(_url);
        }
        public string SignIn(string username, string password)
        {
            string result = "";
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("auth/login", Method.POST);
            var config = new 
            {
                username = username,
                password = password
            };
            request.AddParameter("username", config.username, ParameterType.GetOrPost);
            request.AddParameter("password", config.password, ParameterType.GetOrPost);

            var response = client.Execute<LoginModel>(request);
            var data = response.Data.token;
            if (response.Data.status.ToLower().Equals("berhasil"))
            {
                 result = data;
            }
            else
            {
                result = "9";
            }
            return result;
        }
        public List<User> GetUsers(string token)
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest("user/getusers", Method.GET);
            request.AddHeader("Authorization", "Bearer "+token);
            var response = client.Execute<UserModel>(request);
            Console.WriteLine(response.Content);
            var data = response.Data.users.ToList();
            return data;
        }
        public List<Wisata> GetWisata(string token)
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest("wisata/getWisata", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.Execute<WisataModel>(request);
            Console.WriteLine(response.Content);
            var data = response.Data.Wisata.ToList();
            return data;
        }
        public List<Wisata> GetWisata2()
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest("landing/getWisata", Method.GET);
            var response = client.Execute<WisataModel>(request);
            Console.WriteLine(response.Content);
            var data = response.Data.Wisata.ToList();
            return data;
        }
        public List<Evakuasi> GetEvakuasi2()
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest("landing/getEvakuasi", Method.GET);
            var response = client.Execute<EvakuasiModel>(request);
            Console.WriteLine(response.Content);
            var data = response.Data.Evakuasi.ToList();
            return data;
        }
        public string SaveWisata(string token, string nama, string alamat, string longitude, string lattitude, string foto, string keterangan, string jumlah_pengunjung)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("wisata/addWisata", Method.POST);
            var config = new
            {
                nama = nama,
                alamat = alamat,
                longitude = longitude,
                lattitude = lattitude,
                foto = foto,
                keterangan = keterangan,
                jumlah_pengunjung = jumlah_pengunjung
            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("nama", config.nama, ParameterType.GetOrPost);
            request.AddParameter("alamat", config.alamat, ParameterType.GetOrPost);
            request.AddParameter("longitude", config.longitude, ParameterType.GetOrPost);
            request.AddParameter("lattitude", config.lattitude, ParameterType.GetOrPost);
            request.AddParameter("foto", config.foto, ParameterType.GetOrPost);
            request.AddParameter("keterangan", config.keterangan, ParameterType.GetOrPost);
            request.AddParameter("jumlah_pengunjung", config.jumlah_pengunjung, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }
        public string UpdateWisata(string token, string id, string nama, string alamat, string longitude, string lattitude, string foto, string keterangan, string jumlah_pengunjung)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("wisata/updateWisata", Method.POST);
            var config = new
            {
                id = id,
                nama = nama,
                alamat = alamat,
                longitude = longitude,
                lattitude = lattitude,
                foto = foto,
                keterangan = keterangan,
                jumlah_pengunjung = jumlah_pengunjung
            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("id", config.id, ParameterType.GetOrPost);
            request.AddParameter("nama", config.nama, ParameterType.GetOrPost);
            request.AddParameter("alamat", config.alamat, ParameterType.GetOrPost);
            request.AddParameter("longitude", config.longitude, ParameterType.GetOrPost);
            request.AddParameter("lattitude", config.lattitude, ParameterType.GetOrPost);
            request.AddParameter("foto", config.foto, ParameterType.GetOrPost);
            request.AddParameter("keterangan", config.keterangan, ParameterType.GetOrPost);
            request.AddParameter("jumlah_pengunjung", config.jumlah_pengunjung, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }
        public string DeleteWisata(string token, string id)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("wisata/deleteWisata", Method.POST);
            var config = new
            {
                id = id

            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("id", config.id, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }
        public List<Evakuasi> GetEvakuasi(string token)
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest("evakuasi/getevakuasi", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.Execute<EvakuasiModel>(request);
            Console.WriteLine(response.Content);
            var data = response.Data.Evakuasi.ToList();
            return data;
        }
        public string SaveEvakuasi(string token, string nama, string alamat, string longitude, string lattitude, string foto, string keterangan)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("evakuasi/addevakuasi", Method.POST);
            var config = new
            {
                nama = nama,
                alamat = alamat,
                longitude = longitude,
                lattitude = lattitude,
                foto = foto,
                keterangan = keterangan
            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("nama", config.nama, ParameterType.GetOrPost);
            request.AddParameter("alamat", config.alamat, ParameterType.GetOrPost);
            request.AddParameter("longitude", config.longitude, ParameterType.GetOrPost);
            request.AddParameter("lattitude", config.lattitude, ParameterType.GetOrPost);
            request.AddParameter("foto", config.foto, ParameterType.GetOrPost);
            request.AddParameter("keterangan", config.keterangan, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }
        public string UpdateEvakuasi(string token, string id, string nama, string alamat, string longitude, string lattitude, string foto, string keterangan)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("evakuasi/updateevakuasi", Method.POST);
            var config = new
            {
                id = id,
                nama = nama,
                alamat = alamat,
                longitude = longitude,
                lattitude = lattitude,
                foto = foto,
                keterangan = keterangan
            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("id", config.id, ParameterType.GetOrPost);
            request.AddParameter("nama", config.nama, ParameterType.GetOrPost);
            request.AddParameter("alamat", config.alamat, ParameterType.GetOrPost);
            request.AddParameter("longitude", config.longitude, ParameterType.GetOrPost);
            request.AddParameter("lattitude", config.lattitude, ParameterType.GetOrPost);
            request.AddParameter("foto", config.foto, ParameterType.GetOrPost);
            request.AddParameter("keterangan", config.keterangan, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }
        public string DeleteEvakuasi(string token, string id)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("evakuasi/deleteevakuasi", Method.POST);
            var config = new
            {
                id = id

            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("id", config.id, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }
        public string SaveUsers(string token, string foto, string username, string password, string nama, string email, string alamat, string notelp, string jeniskelamin)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("user/addUsers", Method.POST);
            var config = new
            {
                foto = foto,
                username = username,
                password = password,
                nama = nama,
                email = email,
                alamat = alamat,
                notelp = notelp,
                jeniskelamin = jeniskelamin
            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("foto", config.foto, ParameterType.GetOrPost);
            request.AddParameter("username", config.username, ParameterType.GetOrPost);
            request.AddParameter("password", config.password, ParameterType.GetOrPost);
            request.AddParameter("nama", config.nama, ParameterType.GetOrPost);
            request.AddParameter("email", config.email, ParameterType.GetOrPost);
            request.AddParameter("alamat", config.alamat, ParameterType.GetOrPost);
            request.AddParameter("notelp", config.notelp, ParameterType.GetOrPost);
            request.AddParameter("jeniskelamin", config.jeniskelamin, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }
        public string UpdateUsers(string token,string id,string username, string password, string nama, string email, string alamat, string notelp, string jeniskelamin)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("user/updateUsers", Method.POST);
            var config = new
            {
                id = id,
                username = username,
                password = password,
                nama = nama,
                email = email,
                alamat = alamat,
                notelp = notelp,
                jeniskelamin = jeniskelamin
            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("id", config.id, ParameterType.GetOrPost);
            request.AddParameter("username", config.username, ParameterType.GetOrPost);
            request.AddParameter("password", config.password, ParameterType.GetOrPost);
            request.AddParameter("nama", config.nama, ParameterType.GetOrPost);
            request.AddParameter("email", config.email, ParameterType.GetOrPost);
            request.AddParameter("alamat", config.alamat, ParameterType.GetOrPost);
            request.AddParameter("notelp", config.notelp, ParameterType.GetOrPost);
            request.AddParameter("jeniskelamin", config.jeniskelamin, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }

        public string DeleteUsers(string token, string id)
        {
            string res;
            string baseURL = ConfigurationManager.AppSettings["baseURL"];
            var client = new RestClient(baseURL);
            var request = new RestRequest("user/deleteUsers", Method.POST);
            var config = new
            {
                id = id
            
            };
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("id", config.id, ParameterType.GetOrPost);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject(response.Content);
            string mes = "";
            if (response.IsSuccessful)
            {
                mes = result.ToString();
            }
            else
            {
                mes = result.ToString();
            }
            res = mes;
            return res;

        }
    }
}