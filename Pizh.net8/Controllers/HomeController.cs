using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Pizh.NET8;
using Pizh.NET8.IService;
using Pizh.NET8.Model;
using Pizh.NET8.Service;
using System.Linq.Expressions;

namespace Pizh.net8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBseService<Role, RoleVo> _roleService;

        //private readonly IMapper _mapper;
        public HomeController(ILogger<WeatherForecastController> logger, IBseService<Role, RoleVo> roleService)
        {
            _logger = logger;
            _roleService = roleService;

        }
    }
}
