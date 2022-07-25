using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TCNumberInquiry.Web.Model
{
    public class Data
    {
        [JsonProperty("data")]
        public List<UserModel> UserModel { get; set; }
        [JsonProperty("hasError")]
        public bool HasError { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("validationErrors")]
        public string ValidationErrors { get; set; }
    }

    public class UserModel
    {
        public UserModel()
        {
        }

        [JsonProperty("id")]
        public long Id { get; set; }
        [Required]
        [JsonProperty("identyNumber")]
        public long IdentyNumber { get; set; }
        [Required]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [Required]
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [Required]
        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }
    }
}
