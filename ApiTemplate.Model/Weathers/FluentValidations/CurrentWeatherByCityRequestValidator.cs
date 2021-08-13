using ApiTemplate.Common.Resources;
using FluentValidation;

namespace ApiTemplate.Model.Weathers.FluentValidations
{
    public class CurrentWeatherByCityRequestValidator  :AbstractValidator<CurrentWeatherByCityRequestModel>
    {
        public CurrentWeatherByCityRequestValidator()
        {
            RuleFor(c => c.City).NotNull().NotEmpty().WithMessage(Resource.NotNullOrEmptyCity);
        }
    }
}