using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Common.Enums.Exceptions
{
    public enum CustomStatusCodes
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success,
        [Display(Name = "خطایی در سرور رخ داده است")]
        ServerError,
        [Display(Name = "یافت نشد")]
        NotFound,
        [Display(Name = "پارامترهای ارسالی نامعتبر می باشد")]
        BadRequest,
        [Display(Name = "لیست خالی می باشد")]
        ListEmpty,
        [Display(Name = "خطایی در پردازش رخ داد")]
        LogicError = 5,
        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 6,
        [Display(Name = "پارامتر ورودی خالی می باشد")]
        ArgumentNull
    }
}