using Dm.Config;
using Microsoft.AspNetCore.Mvc;
using Pizh.ChatRoom.Models;
using Pizh.ChatRoom.Service;
using System.Diagnostics;

namespace Pizh.ChatRoom.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public bool CodeFirst()
        {
            return _userService.CodeFirst();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("userName");
            if (string.IsNullOrEmpty(userName))
            {
                //说明用户信息不存在，未登录
                return Redirect("/Home/Login");
            }
            ViewBag.UserName = userName;
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        // pizh my controller
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Submit(string userName, string password)
        {
            int loginStatus = _userService.Login(userName, password);
            if (loginStatus == Constants.LoginConst.OK)
            {
                HttpContext.Session.SetString("userName", userName);
                Response.Cookies.Append("userName", userName);
                return Redirect("/");
            }
            else if (loginStatus == Constants.LoginConst.PasswordError)
            {
                return Json("\"errorMessage\":\"Password Error !\"");
            }
            else
            {
                return Json("\"errorMessage\":\"User Not Exist !\"");
            }
        }

        [HttpPost]
        public IActionResult Register(string userName, string password)
        {
            int registerStatus= _userService.Register(userName, password);
            if (registerStatus == Constants.RegisterConst.OK)
            {
                return Redirect("/Home/Login");
            }
            else if (registerStatus == Constants.RegisterConst.UserAlreadyExist)
            {
                return Json("\"errorMessage\":\"User Already Exist !\"");
            }
            else
            {
                return Json("\"errorMessage\":\"Register Error !\"");
            }
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("userName");
            Response.Cookies.Delete("userName");
            return Redirect("/Home/Login");
        }
    }
}
