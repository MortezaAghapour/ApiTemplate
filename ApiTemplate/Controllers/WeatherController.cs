using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Factory.Weathers;
using ApiTemplate.Infrastructure.Extensions.ModelStates;
using ApiTemplate.Model.Weathers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiTemplate.Controllers
{

    public class WeatherController : BaseController
    {
        #region Fields

        private readonly IWeatherFactory _weatherFactory;



        #endregion
        #region Constructors
        public WeatherController(IWeatherFactory weatherFactory)
        {
            _weatherFactory = weatherFactory;
        }
        #endregion
        #region Actions
        [HttpPost("CurrentWeatherByCity")]
        public async Task<IActionResult> CurrentWeatherByCityName(CurrentWeatherByCityRequestModel model,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetModelStateErrors());
            }

            await _weatherFactory.GetCurrentWeatherInfoByCityName(model,cancellationToken);
            return Ok();
        }
        [HttpPost("CurrentWeatherByLatLon")]
        public async Task<IActionResult> CurrentWeatherByLatLonName(CurrentWeatherByLatLonRequestModel model,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetModelStateErrors());
            }

            await _weatherFactory.GetCurrentWeatherInfoByLatLonName(model,cancellationToken);
            return Ok();
        }
        #endregion
    }
}
