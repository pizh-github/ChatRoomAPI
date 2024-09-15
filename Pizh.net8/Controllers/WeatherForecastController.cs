using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pizh.NET8.IService;
using Pizh.NET8.Model;
using Pizh.NET8.Service;

namespace Pizh.net8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBseService<Role, RoleVo> _roleService;

        //private readonly IMapper _mapper;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBseService<Role,RoleVo> roleService)
        {
            _logger = logger;
            _roleService = roleService;

        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public async IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<object> Get()
        {
            //var userService = new UserService();
            //var userList = await userService.Query();
            //return userList;


            //var roleService = new BaseService<Role, RoleVo>(_mapper);
            var roleList = await _roleService.Query();


            return roleList;
        }
    }
}
