using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TCNumberInquiry.Web.Model
{
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
