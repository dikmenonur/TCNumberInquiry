using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TCNumberInquiry.Web.Services
{
    public class ApiResponse
    {
        private bool hasError;
        public ApiResponse()
        {
            this.ValidationErrors = new List<ValidationErrorMessage>();
        }
        /// <summary>
        /// Hata varsa true döner
        /// </summary>
        [JsonPropertyName("hasError")]
        public bool HasError
        {
            get
            {

                if (!string.IsNullOrEmpty(this.Message) || (this.ValidationErrors != null && this.ValidationErrors.Count > 0))
                    this.hasError = true;
                else
                    this.hasError = false;
                return this.hasError;
            }
            private set
            {
                this.hasError = value;
            }
        }
        /// <summary>
        /// Hata mesajı bilgisini verir
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; private set; }

        /// <summary>
        /// Validasyon hataları varsa listesini verir, yoksa boş döner
        /// </summary>
        [JsonPropertyName("validationErrors")]
        public List<ValidationErrorMessage> ValidationErrors { get; set; }
        public ApiResponse(string message)
        {
            //this.Data = data;
            this.Message = message;

        }

        public void AddValidationError(string field, string error)
        {
            var validationError = this.ValidationErrors.Where(k => k.Key == field).FirstOrDefault();
            if (validationError == null)
                validationError = new ValidationErrorMessage { Key = field };
            validationError.Errors.Add(error);

        }
        public void AddValidationError(string field, List<string> errors)
        {
            var validationError = this.ValidationErrors.Where(k => k.Key == field).FirstOrDefault();
            if (validationError == null)
                validationError = new ValidationErrorMessage { Key = field };
            validationError.Errors.AddRange(errors);
        }

    }

    /// <summary>
    /// Validasyon bilgilerini taşır
    /// </summary>
    public class ValidationErrorMessage
    {
        public ValidationErrorMessage()
        {
            this.Errors = new List<string>();
        }
        /// <summary>
        /// Validasyon hatası olan özelliği verir.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
        /// <summary>
        /// Oluşan validasyon hatalarını verir.
        /// </summary>
        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }
    }
    /// <summary>
    /// Response bilgilerini taşır
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse()
        {

        }
        public ApiResponse(T data, string message) : base(message)
        {
            this.Data = data;
        }
        /// <summary>
        /// Response datayı taşır.
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }


}
