using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiTemplate.Model.Users
{
    public class LoginModel
    {
        [Required(ErrorMessage = "فیلد {0} نمی تواند خالی باشد")] 
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "فیلد {0} نمی تواند خالی باشد")]
        [DisplayName("کلمه عبور")]
        public string Password { get; set; }

        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
    }
}
