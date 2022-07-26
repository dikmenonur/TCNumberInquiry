using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCNumberInquiry.Web.Model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TCNumberInquiry.Web.Services
{
    public class UserServices
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;

        public UserServices(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ApiResponse<List<UserModel>>> GetAllUsersAsync()
        {
            var httpClient = this._httpClientFactory.CreateClient("TCNumberInquiryClient");
            using var response = await httpClient.GetAsync("users/GetAllUser", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<List<UserModel>>>(stream);

        }

        public async Task<ApiResponse<UserModel>> GetUserByIdAsync(long Id)
        {
            var httpClient = this._httpClientFactory.CreateClient("TCNumberInquiryClient");
            using var response = await httpClient.GetAsync("users/GetUserById?userId=" + Id, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<UserModel>>(stream);

        }

        public async Task<ApiResponse> InsertUserAsync(UserModel user)
        {
            var httpClient = this._httpClientFactory.CreateClient("TCNumberInquiryClient");
            StringContent content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync("users/InsertUser", content);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(stream);
        }

        public async Task<ApiResponse> UpdateUserAsync(UserModel user)
        {
            var httpClient = this._httpClientFactory.CreateClient("TCNumberInquiryClient");
            StringContent content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            ApiResponse apiResponse = null;
            using var response = await httpClient.PostAsync("users/UpdateUser", content);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(stream);

            return apiResponse;
        }

        public async Task<ApiResponse> DeleteUserAsync(long id)
        {
            var httpClient = this._httpClientFactory.CreateClient("TCNumberInquiryClient");
            StringContent content = new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json");
            ApiResponse apiResponse = null;

            using var response = await httpClient.PostAsync("users/DeleteUser", content);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(stream);

            return apiResponse;
        }
    }
}
