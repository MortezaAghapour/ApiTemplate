using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Model.Users
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "فیلد {0} نمی تواند خالی باشد")]
        [DisplayName("نام")]
        public string Name { get; set; }
        [Required(ErrorMessage = "فیلد {0} نمی تواند خالی باشد")]
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "فیلد {0} نمی تواند خالی باشد")]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "فیلد {0} نمی تواند خالی باشد")]
        [DisplayName("رمز عبور")]
        public string Password { get; set; }
    }
}