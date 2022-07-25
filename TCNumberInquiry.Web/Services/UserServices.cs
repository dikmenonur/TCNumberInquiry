using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCNumberInquiry.Web.Model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TCNumberInquiry.Web.Services
{
    public class UserServices
    {
        private readonly string URLbase = "https://localhost:5001/api/public/v1/users";
        public async Task<Data> GetAllUsersAsync()
        {
            Data usersList = new Data();
            using (var httpClient = new HttpClient())
            {
                string URL = URLbase + "/GetAllUser";
                HttpResponseMessage response = await httpClient.GetAsync(URL);
                string apiResponse = await response.Content.ReadAsStringAsync();
                usersList = JsonConvert.DeserializeObject<Data>(apiResponse);
            }
            return usersList;
        }

        public async Task<UserModel> GetUserByIdAsync(long Id)
        {
            UserModel users = new UserModel();
            using (var httpClient = new HttpClient())
            {
                string URL = URLbase + "/GetUserByIdModel?userId=" + Id;
                HttpResponseMessage response = await httpClient.GetAsync(URL);
                string apiResponse = await response.Content.ReadAsStringAsync();

                users = JsonSerializer.Deserialize<UserModel>(apiResponse);
            }
            return users;
        }

        public async Task InsertUserAsync(UserModel user)
        {
            string URL = URLbase + "/InsertUser";

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(URL, content);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
