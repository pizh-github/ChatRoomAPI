﻿using Dm.Config;
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

        /// <summary>
        /// ChatRoom
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("userName");
            if (string.IsNullOrEmpty(userName))
            {
                return Redirect("/Home/Login");
            }
            ViewBag.UserName = userName;
            return View();
        }

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
        [HttpGet]
        public IActionResult Submit(string userName, string password)
        {
            int loginStatus = _userService.Login(userName, password);
            if (loginStatus == Constants.LoginConst.OK)
            {
                HttpContext.Session.SetString("userName", userName);
                Response.Cookies.Append("userName", userName);
                return Redirect("/Home/Index");
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

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// LogOut
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("userName");
            Response.Cookies.Delete("userName");
            return Redirect("/Home/Login");
        }
    }
}
